using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Authentication_And_Authorization_In_Dot_Net_Core.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Authentication_And_Authorization_In_Dot_Net_Core
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllersWithViews();
            services.AddDbContext<DatabaseContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DevConnection")));


            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Boss";
               
            })
            .AddCookie("Employee", options =>
            {
               
                options.Cookie.Name = "Employee.Says";
                options.LoginPath = "/Employees/Login";

            })
            .AddCookie("Boss", options =>
             {
                 options.Cookie.Name = "Boss.Says";
                ///// options.Cookie.Name = "Employee.Says";
                 options.LoginPath = "/Boss/Login";

             });






            /*services.AddAuthentication("Employee")
                .AddCookie("Employee", config =>
                {
                    config.Cookie.Name = "Employees";
                    config.LoginPath = "/Employees/Login";
                  
                });
           
             services.AddAuthentication("Boss")
               .AddCookie("Boss", config =>
               {
                   config.Cookie.Name = "Boss";
                   config.LoginPath = "/Boss/Login";
               });

            */

   
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.Cookie.Name = ".AdventureWorks.Session";
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.IsEssential = true;
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
