﻿using MoanaTrello.Models.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MoanaTrello.Services
{
    public class CardService : ICardService
    {
        public async Task<IEnumerable<Card>> GetCards(string token)
        {
            using (var client = new HttpClient())
            {
                var uri = "http://193.201.187.29:84/Cards/GetAll";

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var res = await client.GetAsync(uri);

                var json = await res.Content.ReadAsStringAsync();

                var response = JsonConvert.DeserializeObject<List<Card>>(json);

                return response;
            }
        }

        public async Task<IEnumerable<Card>> GetCardsByStatus(int status, string token)
        {
            using (var client = new HttpClient())
            {
                var uri = "http://193.201.187.29:84/Cards/GetAll";

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var res = await client.GetAsync(uri);

                var json = await res.Content.ReadAsStringAsync();

                var response = JsonConvert.DeserializeObject<List<Card>>(json);

                return response.Where(x => x.Status == status).OrderBy(x => x.Status);
            }
        }

        public async Task<bool> CreateCard(string token, CardRequest card)
        {
            using (var client = new HttpClient())
            {
                var uri = "http://193.201.187.29:84/Cards/Add";

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var res = await client.PostAsync(uri, new StringContent(JsonConvert.SerializeObject(card), Encoding.UTF8, "application/json"));

                if (res.IsSuccessStatusCode)
                {
                    return true;
                }

                return false;
            }
        }

        public async Task<bool> DeleteCard(string token, string id)
        {
            using (var client = new HttpClient())
            {
                var uri = "http://193.201.187.29:84/Cards/Delete";

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var httpMessage = new HttpRequestMessage(HttpMethod.Delete, uri)
                {
                    Content = new StringContent("{\"id\":\"" + id + "\"}", Encoding.UTF8, "application/json")
                };

                var res = await client.SendAsync(httpMessage);

                if (res.IsSuccessStatusCode)
                {
                    return true;
                }

                return false;
            }
        }

        public async Task<Card> GetCardById(string token, string id)
        {
            using (var client = new HttpClient())
            {
                var uri = "http://193.201.187.29:84/Cards/GetById?id=" + id;

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var res = await client.GetAsync(uri);

                var json = await res.Content.ReadAsStringAsync();

                var response = JsonConvert.DeserializeObject<Card>(json);

                return response;
            }
        }

        public async Task<List<User>> GetUsers(string token)
        {
            using (var client = new HttpClient())
            {
                var uri = "http://193.201.187.29:84/Users/GetAll";

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var res = await client.GetAsync(uri);

                var json = await res.Content.ReadAsStringAsync();

                var response = JsonConvert.DeserializeObject<List<User>>(json);

                return response;
            }
        }

        public async Task<bool> EditCard(string token, Card card)
        {
            using (var client = new HttpClient())
            {
                var uri = "http://193.201.187.29:84/Cards/Update";

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var res = await client.PutAsync(uri, new StringContent(JsonConvert.SerializeObject(card), Encoding.UTF8, "application/json"));

                if (res.IsSuccessStatusCode)
                {
                    return true;
                }

                return false;
            }
        }
    }
}
