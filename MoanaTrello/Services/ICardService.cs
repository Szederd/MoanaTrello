using MoanaTrello.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoanaTrello.Services
{
    public interface ICardService
    {
        Task<IEnumerable<Card>> GetCards(string token);
        Task<IEnumerable<Card>> GetCardsByStatus(int status, string token);
        Task<bool> CreateCard(string token, CardRequest card);
        Task<bool> DeleteCard(string token, string id);
        Task<Card> GetCardById(string token, string id);
        Task<List<User>> GetUsers(string token);
        Task<bool> EditCard(string token, Card card);
        Task<bool> ChangeCardStatusAndPosition(string token, CardChangeRequest card);
    }
}
