using Microsoft.Extensions.Configuration;
using MoanaTrello.Models.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MoanaTrello.Services
{
    public class LoginService : ILoginService
    {
        public async Task<LoginResponse> Login(LoginRequest loginRequest)
        {
            using(var client = new HttpClient()){
                var loginEndpoint = "http://193.201.187.29:84/Authentication/SignIn";

                var request = new HttpRequestMessage(HttpMethod.Post, loginEndpoint);
                request.Content = new StringContent(JsonConvert.SerializeObject(loginRequest), Encoding.UTF8, "application/json");

                var response = await client.PostAsync(loginEndpoint, new StringContent(JsonConvert.SerializeObject(loginRequest), Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();

                    var tokenReponse = JsonConvert.DeserializeObject<LoginResponse>(json);

                    return tokenReponse;
                }
                return new LoginResponse();
            }

        }

        public async Task<bool> Register(RegisterRequest registerRequest)
        {
            using (var client = new HttpClient())
            {
                var loginEndpoint = "http://193.201.187.29:84/Authentication/SignUp";

                var request = new HttpRequestMessage(HttpMethod.Post, loginEndpoint);
                request.Content = new StringContent(JsonConvert.SerializeObject(registerRequest), Encoding.UTF8, "application/json");

                var response = await client.PostAsync(loginEndpoint, new StringContent(JsonConvert.SerializeObject(registerRequest), Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }

        }
    }
}
