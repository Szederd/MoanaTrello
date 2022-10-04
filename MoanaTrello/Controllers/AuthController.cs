using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoanaTrello.Models.Helpers;
using MoanaTrello.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        public async Task<ActionResult> Login(LoginRequest loginRequest)
        {
            if (ModelState.IsValid)
            {
                var token = await _loginService.Login(loginRequest);

                if (!String.IsNullOrEmpty(token.Token))
                {
                    HttpContext.Session.SetString("token", token.Token);

                    return RedirectToAction("Index", "Table");
                }
                

            }
                    return View(loginRequest);
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

        [Authorize]
        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return new SignOutResult("Auth0", new AuthenticationProperties
            {
                RedirectUri = Url.Action("Index", "Home")
            });
        }
    }
}
