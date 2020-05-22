using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Authentication_And_Authorization_In_Dot_Net_Core.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Collections;

namespace Authentication_And_Authorization_In_Dot_Net_Core.Controllers
{
    
    public class EmployeesController : Controller
    {
        private readonly DatabaseContext _context;
       

        public EmployeesController(DatabaseContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Boss" , AuthenticationSchemes = "Boss")]

        // GET: Employees
        public async Task<IActionResult> Index()
        {

            return View(await _context.employee.ToListAsync());
        }
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login([Bind("Email", "Password")]Employee employee)
        {
            var data = await _context.employee.Where((x => x.Email == employee.Email && x.Password == employee.Password)).FirstOrDefaultAsync<Employee>();
            ClaimsIdentity identity = null;

            if (data != null)
            {
                await HttpContext.SignOutAsync("Employee");
                await HttpContext.SignOutAsync("Boss");

                identity = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.Email,employee.Email),
                new Claim(ClaimTypes.Role,"Employee")
                

            }, CookieAuthenticationDefaults.AuthenticationScheme);

                

                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync("Employee",principal);

                HttpContext.Session.SetString("EmployEmail", employee.Email);

                return Redirect("~/Employees/Details/" + employee.Email);
            }

            else
            {
                ModelState.AddModelError(string.Empty, "Invalid Login");
            }


            return View(employee);
        }


        // GET: Employees/Details/5
        [Authorize(Roles = "Employee", AuthenticationSchemes = "Employee")]
        public async Task<IActionResult> Details(string id)
        {
            if (HttpContext.Session.GetString("EmployEmail") != null)
            {
                id = HttpContext.Session.GetString("EmployEmail");
            }

            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.employee.Where(x => x.Email == id).FirstOrDefaultAsync<Employee>();
              
            if (employee == null)
            {
               
                return NotFound();
            }

            return View(employee);
        }

        [Authorize(Roles = "Boss" , AuthenticationSchemes = "Boss" )]
        // GET: Employees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Boss", AuthenticationSchemes = "Boss")]
        public async Task<IActionResult> Create([Bind("Name,Email,Address,Phone,Password,ConfrimPassword")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }
        [Authorize(Roles = "Employee", AuthenticationSchemes = "Employee")]
        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.employee.Where(x => x.Email == id).FirstOrDefaultAsync<Employee>();
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Employee", AuthenticationSchemes = "Employee")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name,Email,Address,Phone,Password,ConfrimPassword")] Employee employee)
        {
            if (id != employee.Email)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Email))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Redirect("~/Employees/Details/" + employee.Email);
            }
            return View(employee);
        }
        [Authorize(Roles = "Employee", AuthenticationSchemes = "Employee")]
        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.employee
                .FirstOrDefaultAsync(m => m.Email == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [Authorize(Roles = "Employee", AuthenticationSchemes = "Employee")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var employee = await _context.employee.FindAsync(id);
            _context.employee.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(string id)
        {
            return _context.employee.Any(e => e.Email == id);
        }
        [Authorize(Roles = "Employee", AuthenticationSchemes = "Employee")]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync("Employee");
            return Redirect("~/Home/Index");
        }
    }
}
