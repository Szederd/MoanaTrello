using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
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
                    Response.Cookies.Append("accessToken", token.Token);
          //          var claims = new List<Claim>
          //{
          //  new Claim(ClaimTypes.NameIdentifier, token.UserId),
          //  new Claim("accessToken", token.Token)
          //};

          //          var userIdentity = new ClaimsIdentity(claims, "login");

          //          ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
          //          await HttpContext.SignInAsync(principal);

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
