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
    public class TableController : Controller
    {

        private readonly ICardService _cardService;
        private readonly string _token;

        public TableController(ICardService cardService) => _cardService = cardService;

        [SessionFilter]
        public async Task<ActionResult> Index()
        {
            var model = await _cardService.GetCards(HttpContext.Session.GetString("token"));

            return View(model);
        }

        [HttpPost]
        [SessionFilter]
        public async Task<IActionResult> CreateCard([FromBody]CardRequest card)
        {
            if (await _cardService.CreateCard(HttpContext.Session.GetString("token"), card))
            {
               return Json(new { success = true });
            }

            return Json(new { success = false });
        }

        [HttpDelete]
        [SessionFilter]
        public async Task<IActionResult> DeleteCard(string id)
        {
            if (await _cardService.DeleteCard(HttpContext.Session.GetString("token"), id))
            {
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

        [SessionFilter]
        public async Task<PartialViewResult> GetCardByStatus(int status)
        {
            var model = await _cardService.GetCardsByStatus(status, HttpContext.Session.GetString("token"));

            return PartialView("_Cards", model);
        }

        [SessionFilter]
        public PartialViewResult AddNewCard()
        {
            return PartialView("_AddNewCard");
        }

        [SessionFilter]
        public async Task<PartialViewResult> CardDetail(string id)
        {
            var model = await _cardService.GetCardById(HttpContext.Session.GetString("token"), id);
            return PartialView("_CardDetail", model);
        }

        [SessionFilter]
        public async Task<PartialViewResult> EditCard(string id)
        {
            var model = await _cardService.GetCardById(HttpContext.Session.GetString("token"), id);
            ViewData["users"] = await _cardService.GetUsers(HttpContext.Session.GetString("token"));

            return PartialView("_EditCard", model);
        }

        [HttpPut]
        [SessionFilter]
        public async Task<IActionResult> EditCard([FromBody]Card card)
        {
            if (await _cardService.EditCard(HttpContext.Session.GetString("token"), card))
            {
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpPut]
        [SessionFilter]
        public async Task<IActionResult> ReorderCards([FromBody] List<ReorderCardRequest> reorderCardRequest)
        {
            if (await _cardService.EditCard(HttpContext.Session.GetString("token"), reorderCardRequest))
            {
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}
