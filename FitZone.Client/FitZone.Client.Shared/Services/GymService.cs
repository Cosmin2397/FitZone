using FitZone.Client.Shared.DTOs.Access;
using FitZone.Client.Shared.DTOs.Gym;
using FitZone.Client.Shared.Services.Interfaces;
using FitZone.Client.Shared.Utilities;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;

namespace FitZone.Client.Shared.Services
{
    public class GymService : IGymService
    {
        private readonly HttpClient _httpClient;

        public GymService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GymDTO> AddGym(GymDTO gym)
        {
            try
            {
                var json = JsonSerializer.Serialize(gym);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                // Trimitem cererea POST pentru a adăuga o sală
                var response = await _httpClient.PostAsync("/gymsService/Gym", content);
                if (response.IsSuccessStatusCode)
                {
                    // Dacă cererea a fost reușită, returnăm sala creată
                    var addedGym = await response.Content.ReadFromJsonAsync<GymDTO>();
                    return addedGym;
                }
                else
                {
                    // Dacă cererea a eșuat, aruncăm o excepție sau gestionăm eroarea
                    Console.WriteLine("Failed to add gym");
                }
            }
            catch (Exception ex)
            {
                // Gestionăm excepțiile
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return null; // Returnăm null în cazul în care adăugarea a eșuat
        }

        public async Task<GymDTO> GetGymById(Guid id)
        {
            try
            {
                string url = $"/gymsService/Gym/{id}";
                var response = await _httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var gym = await response.Content.ReadFromJsonAsync<GymDTO>();
                    return gym;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<GymDTO> UpdateGym(GymDTO gymUpdated, Guid id)
        {
            try
            {
                var json = JsonSerializer.Serialize(gymUpdated);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                string url = $"/gymsService/Gym/{id}";
                var response = await _httpClient.PutAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    var updatedGym = await response.Content.ReadFromJsonAsync<GymDTO>();
                    return updatedGym;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<List<GymDTO>> GetGyms()
        {
            try
            {
                string url = $"/gymsService/Gym";

                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var gyms = await response.Content.ReadFromJsonAsync<List<GymDTO>>();
                    return gyms;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<bool> DeleteGym(Guid id)
        {
            try
            {
                string url = $"/gymsService/Gym/{id}";
                var response = await _httpClient.DeleteAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }
    }
}
