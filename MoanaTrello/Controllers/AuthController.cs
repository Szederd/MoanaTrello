using Microsoft.AspNetCore.Mvc;
using MoanaTrello.Models.Helpers;
using MoanaTrello.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoanaTrello.Controllers
{
    public class AuthController : Controller
    {
        private readonly ILoginService _loginService;

        public AuthController(ILoginService loginService) => _loginService = loginService;

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
            {
                return View(loginRequest);
            }

            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterRequest registerRequest)
        {
            if (!ModelState.IsValid)
            {
                return View(registerRequest);
            }

            return RedirectToAction("Login");
        }

        public ActionResult Logout()
        {
            return View();
        }
    }
}
