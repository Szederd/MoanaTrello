using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoanaTrello.Services
{
    public class SessionFilter : Attribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var result = context.HttpContext.Session.Keys.FirstOrDefault(x => x == "token");

            if (result == null)
            {
                context.Result = new RedirectToActionResult("Login", "Auth", null);
            }

        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}
