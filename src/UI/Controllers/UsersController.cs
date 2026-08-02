using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UI.Contracts;
using UI.Models;

namespace UI.Controllers
{

    public class UsersController : Controller
    {
        private readonly IAuthenticateService _authenticateService;

        public UsersController(IAuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }


        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM, string? returnUrl)
        {
            returnUrl ??= Url.Content("~/");
            var loggedin = await _authenticateService.Authenticate(loginVM.Email, loginVM.Password);
            if (loggedin)
            {
                return LocalRedirect(returnUrl);
            }
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(loginVM);
        }


        [HttpGet]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Failed to register.");
                return View(registerVM);
            }
            var registered = await _authenticateService.Register(registerVM.FirstName, registerVM.LastName, registerVM.Email, registerVM.Password, registerVM.UserName);
            if (registered)
            {
                return LocalRedirect("/Users/Login");
            }
            ModelState.AddModelError(string.Empty, "Failed to register.");
            return View(registerVM);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _authenticateService.Logout();
            return LocalRedirect("/Users/Login");
        }
    }
}