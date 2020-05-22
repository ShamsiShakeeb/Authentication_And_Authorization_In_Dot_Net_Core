using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Authentication_And_Authorization_In_Dot_Net_Core.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Authentication_And_Authorization_In_Dot_Net_Core.Controllers
{
    [Authorize(Roles ="Boss" , AuthenticationSchemes = "Boss")]
    public class BossController : Controller
    {
        private readonly DatabaseContext _context;

        public BossController(DatabaseContext context)
        {
            _context = context;
        }

       

        public async Task<IActionResult> Index(String id)
        {
            if (HttpContext.Session.GetString("email")!=null)
            {
                id = HttpContext.Session.GetString("email");
            }

            if (id == null)
            {
                return NotFound();
            }

            var boss = await _context.boss
                .FirstOrDefaultAsync(m => m.Email == id);
            if (boss == null)
            {
                return NotFound();
            }

            return View(boss);

           
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([Bind("Email","Password")]Boss boss)
        {
            ClaimsIdentity identity = null;
            var data = await _context.boss.Where((x => x.Email == boss.Email && x.Password == boss.Password)).FirstOrDefaultAsync<Boss>();
            if (data == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }
            else
            {
                await HttpContext.SignOutAsync("Employee");
                await HttpContext.SignOutAsync("Boss");

                identity = new ClaimsIdentity( new[]
                {
                    new Claim(ClaimTypes.Email,boss.Email),
                    new Claim(ClaimTypes.Role,"Boss"),
                },
                    
               CookieAuthenticationDefaults.AuthenticationScheme);

               var principal = new ClaimsPrincipal(identity);
               await HttpContext.SignInAsync("Boss", principal);
               HttpContext.Session.SetString("email", boss.Email);
               return Redirect("~/Boss/Index/" + HttpContext.Session.GetString("email")); 
            }
            return View(boss);
        }
        [AllowAnonymous]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create(Boss data)
        {
            if (ModelState.IsValid)
            {
                _context.boss.Add(data);
                await _context.SaveChangesAsync();
                return Redirect("~/Boss/Login");
            }

            return View(data);
        }

        public async Task<IActionResult> Edit(String id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boss = await _context.boss.Where(x => x.Email == id).SingleOrDefaultAsync<Boss>();

            if (boss == null)
            {
                return NotFound();
            }

            return View(boss);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(String id,Boss boss)
        {
            if (id != boss.Email)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(boss);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BossExists(boss.Email))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return Redirect("~/Boss/Index/" + id);
        }
        private bool BossExists(string id)
        {
            return _context.boss.Any(e => e.Email == id);
        }
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync("Boss");
            return Redirect("~/Home/Index");
        }
    }
   
}