using AppoinmentApp.Data;
using AppoinmentApp.Models;
using AppoinmentApp.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AppoinmentApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        RoleManager<IdentityRole> _roleManager;
        #region Fildes
        public AccountController(AppDbContext db, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            this._db = db;
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._roleManager = roleManager;
        }
        #endregion
        #region Actions
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult LogIn() => View();
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn(LoginViewModel mdl)
        {
            if (!ModelState.IsValid) { return View(mdl); }
            //var user = await _userManager.FindByEmailAsync(mdl.Email);
            //if(user!=null)
            //{
            
                var PasswordCheck = await _signInManager.PasswordSignInAsync(mdl.Email,mdl.Password, mdl.RememberMe, false);
                if(PasswordCheck.Succeeded)
                {
                    return RedirectToAction("Index", "Appointment");
                }
                ModelState.AddModelError("", "Invliad Login");
            //}
            return View(mdl);
        }
        public async Task<IActionResult> Register()
        {
            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole(Helpers.Hepler.Admin));
                await _roleManager.CreateAsync(new IdentityRole(Helpers.Hepler.Doctor));
                await _roleManager.CreateAsync(new IdentityRole(Helpers.Hepler.Patinet));
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegsiterViewModle mdl)
        {
            if (!ModelState.IsValid) { return View(mdl); }
            var user = new ApplicationUser()
            {
                FullName = mdl.FullName,
                Email = mdl.Email,
                UserName=mdl.Email
            };
            var resulte = await _userManager.CreateAsync(user,mdl.Password);
            if (resulte.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, mdl.RoleName);
                await _signInManager.SignInAsync(user,isPersistent:false);

                return RedirectToAction("index", "Appointment");
            }
            return View(nameof(Register));



        }

        public async Task<IActionResult> Logoff()
        {
            await _signInManager.SignOutAsync();
            return View(nameof(LogIn));

        }
        #endregion

    }
}
