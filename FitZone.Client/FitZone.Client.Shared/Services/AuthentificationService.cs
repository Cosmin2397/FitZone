using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FitZone.Client.Shared.Services.Interfaces;
using FitZone.Client.Shared.Utilities;
using FitZone.Client.Shared.DTOs;

namespace FitZone.Client.Shared.Services
{
    public class AuthentificationService : IAuthentificationService
    {
        private readonly HttpClient _httpClient;

        public AuthentificationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> Logout(string email)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, $"api/User/logout/{email}");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", UserState.Instance.GetJwtToken);

            var response = await _httpClient.SendAsync(requestMessage);
            return response.IsSuccessStatusCode;
        }


        public async Task<LoginResponseDto> Login(LoginRequestDto user)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/authService/Auth/sign-in", user);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<LoginResponseDto>();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

           return null;
        }

        public async Task<LoginResponseDto> RefreshToken(RefreshTokenDto model)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "api/User/RefreshToken")
            {
                Content = JsonContent.Create(model)
            };
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", UserState.Instance.GetJwtToken);

            var response = await _httpClient.SendAsync(requestMessage);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<LoginResponseDto>();
            }
            return null;
        }

        public async Task<List<UserDto>> GetUsers()
        {
            // Creează un mesaj de cerere HTTP
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "api/User/Users");

            // Adaugă token-ul JWT la cerere
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", UserState.Instance.GetJwtToken);

            // Trimite cererea și obține răspunsul
            var response = await _httpClient.SendAsync(requestMessage);

            // Verifică dacă răspunsul este de succes și deserializăm conținutul
            if (response.IsSuccessStatusCode)
            {
                var users = await response.Content.ReadFromJsonAsync<List<UserDto>>();
                return users;
            }

            return null;
        }


        public async Task<UserDto> GetUserByEmail(string email)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"api/User/{email}");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", UserState.Instance.GetJwtToken);

            var response = await _httpClient.SendAsync(requestMessage);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<UserDto>();
            }
            return null;
        }


        public async Task<bool> DeleteUser(string email)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, $"api/User/{email}");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", UserState.Instance.GetJwtToken);

            var response = await _httpClient.SendAsync(requestMessage);
            return response.IsSuccessStatusCode;
        }


        public async Task<bool> UpdateUser(UserDto userUpdated)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Put, $"api/User")
            {
                Content = JsonContent.Create(userUpdated)
            };
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", UserState.Instance.GetJwtToken);

            var response = await _httpClient.SendAsync(requestMessage);
            return response.IsSuccessStatusCode;
        }

        public async Task<string> GetUserName(Guid clientId)
        {
            try
            {
                string url = $"/authService/Auth/userid/{clientId}";

                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var user = await response.Content.ReadFromJsonAsync<UserDto>();
                    return user.FirstName + " " + user.LastName;
                }
                else
                {
                    return String.Empty;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return String.Empty;
        }
    }
}