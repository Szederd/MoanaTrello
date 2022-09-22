using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoanaTrello.Controllers
{
    public class TableController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult AddNewCard()
        {
            ViewBag.Target = "addNewCard";
            return PartialView("_AddNewCard");
        }
    }
}
