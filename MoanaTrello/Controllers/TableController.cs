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
    public class TableController : Controller
    {

        private readonly ICardService _cardService;

        public TableController(ICardService cardService) => _cardService = cardService;

        public async Task<ActionResult> Index()
        {
            var token = Request.Cookies.FirstOrDefault(x => x.Key == "accessToken").Value;
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Auth");
            }

            var model = await _cardService.GetCards(token);


            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCard([FromBody]CardRequest card)
        {
            var token = Request.Cookies.FirstOrDefault(x => x.Key == "accessToken").Value;

            if (await _cardService.CreateCard(token, card))
            {
               return Json(new { success = true });
            }

            return Json(new { success = false });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCard(string id)
        {
            var token = Request.Cookies.FirstOrDefault(x => x.Key == "accessToken").Value;

            if (await _cardService.DeleteCard(token, id))
            {
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

        public async Task<PartialViewResult> GetCardByStatus(int status){
            var token = Request.Cookies.FirstOrDefault(x => x.Key == "accessToken").Value;

            var model = await _cardService.GetCardsByStatus(status, token);

            return PartialView("_Cards", model);
        }


        public PartialViewResult AddNewCard()
        {
            return PartialView("_AddNewCard");
        }
        public async Task<PartialViewResult> CardDetail(string id)
        {
            var token = Request.Cookies.FirstOrDefault(x => x.Key == "accessToken").Value;

            var model = await _cardService.GetCardById(token, id);
            return PartialView("_CardDetail", model);
        }

        public async Task<PartialViewResult> EditCard(string id)
        {
            var token = Request.Cookies.FirstOrDefault(x => x.Key == "accessToken").Value;

            var model = await _cardService.GetCardById(token, id);
            var users = await _cardService.GetUsers(token);
            ViewData["users"] = users;

            return PartialView("_EditCard", model);
        }
        [HttpPut]
        public async Task<IActionResult> EditCard(Card card)
        {
            var token = Request.Cookies.FirstOrDefault(x => x.Key == "accessToken").Value;

            if (await _cardService.EditCard(token, card))
            {
                return Json(new { success = true });
            }

            return Json(new { success = false });


        }
    }
}
