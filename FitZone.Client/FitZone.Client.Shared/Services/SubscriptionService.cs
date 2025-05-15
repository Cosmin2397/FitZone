using FitZone.Client.Shared.DTOs.Subscription;
using FitZone.Client.Shared.Services.Interfaces;
using FitZone.Client.Shared.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FitZone.Client.Shared.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly HttpClient _httpClient;

        public SubscriptionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<SubscriptionDto?> GetSubscriptionByClientId(string stringId)
        {
            //test
            if (Guid.TryParse(stringId, out Guid id))
            {
                // Trimiți cererea API pentru a obține informațiile despre abonamentele clientului
                var response = await _httpClient.GetStringAsync($"/subscriptionService/clientSubs/{stringId}");

                // Deserializăm răspunsul JSON folosind JsonDocument
                using (var document = JsonDocument.Parse(response))
                {
                    // Căutăm abonamentele active care sunt valabile (endDate > data curentă)
                    var subscriptions = document.RootElement.EnumerateArray()
                        .Where(sub => sub.GetProperty("status").GetString() == "Active" &&
                                      sub.GetProperty("endDate").GetDateTime() > DateTime.Now) // Filtrăm doar abonamentele valabile
                        .OrderByDescending(sub => sub.GetProperty("endDate").GetDateTime()) // Le ordonăm descrescător după data de final
                        .FirstOrDefault();

                    if (subscriptions.ValueKind == JsonValueKind.Undefined)
                    {
                        return null; // Nu există abonamente active
                    }

                    // Extragem informațiile din abonament
                    var gymId = subscriptions.GetProperty("gymId").GetString();
                    var subscriptionType = subscriptions.GetProperty("subscriptionType").GetString();
                    var endDate = subscriptions.GetProperty("endDate").GetDateTime();
                    // Apoi trimiți o cerere pentru detaliile sălii pe baza gymId
                    var gymResponse = await _httpClient.GetStringAsync($"/gymsService/Gym/{gymId}");


                    // Creăm DTO-ul pentru abonament
                    var subscriptionDto = new SubscriptionDto
                    {
                        ValidUntil = endDate,
                        SubscriptionType = subscriptionType,
                        GymDetails = GetGymDetails(gymResponse)
                    };
                    if (Guid.TryParse(gymId, out Guid gymGuidId))
                    {
                        subscriptionDto.GymDetails.GymId = gymGuidId;
                    }
                    UserState.Instance.SetUserSubscription(subscriptionDto);
                    return subscriptionDto;
                }
            }
            else
            {
                return null;
            }
        }

        public async Task<List<GymDetailDto>> GetGyms()
        {
            var gyms = new List<GymDetailDto>();

            try
            {
                var gymResponse = await _httpClient.GetStringAsync("/gymsService/Gym");
                var gymList = JsonSerializer.Deserialize<JsonElement>(gymResponse);

                if (gymList.ValueKind != JsonValueKind.Array)
                    return gyms; // return listă goală dacă nu e array

                foreach (var gymItem in gymList.EnumerateArray())
                {
                    if (!gymItem.TryGetProperty("gymData", out var gymData))
                        continue;

                    var gymName = gymData.GetProperty("name").GetString();
                    var gymAddress = gymData.GetProperty("address").GetString();
                    var phoneNumber = gymData.GetProperty("phoneNumber").GetString();

                    var workingHours = new List<string>();

                    if (gymItem.TryGetProperty("weekBusinessHours", out var hours))
                    {
                        foreach (var bh in hours.EnumerateArray())
                        {
                            var day = Enum.GetName(typeof(DayOfWeek), bh.GetProperty("dayOfWeek").GetInt32());

                            if (bh.GetProperty("isClosed").GetBoolean())
                            {
                                workingHours.Add($"{day}: Closed");
                            }
                            else
                            {
                                var opening = bh.GetProperty("openingHour").GetString();
                                var closing = bh.GetProperty("closingTime").GetString();
                                workingHours.Add($"{day}: {opening} - {closing}");
                            }
                        }
                    }

                    gyms.Add(new GymDetailDto
                    {
                        GymName = gymName,
                        GymAddress = gymAddress,
                        PhoneNumber = phoneNumber,
                        WorkingHours = workingHours
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return gyms;
        }

        public async Task<Guid> AddSubscription(AddSubscriptionRequest subscription)
        {
            try
            {
                var json = JsonSerializer.Serialize(subscription);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                // Trimitem cererea POST pentru a adăuga un abonament
                var response = await _httpClient.PostAsync("/subscriptionService/subscriptions", content);
                if (response.IsSuccessStatusCode)
                {
                    // Dacă cererea a fost reușită, returnăm ID-ul abonamentului creat
                    var responseBody = await response.Content.ReadAsStringAsync();
                    return Guid.Parse(responseBody);
                }
                else
                {
                    // Dacă cererea a eșuat, aruncăm o excepție sau gestionăm eroarea
                    Console.WriteLine("Failed to add subscription");
                }
            }
            catch (Exception ex)
            {
                // Gestionăm excepțiile
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return Guid.Empty; // Returnăm Guid.Empty în caz de eroare
        }

        private GymDetailDto GetGymDetails(string gymResponse)
        {
            // Deserializăm răspunsul de la API-ul salii
            var gymResponseJson = JsonSerializer.Deserialize<JsonElement>(gymResponse);

            // Verificăm dacă gymData este null sau nu există
            if (!gymResponseJson.TryGetProperty("gymData", out var gymData))
            {
                // Dacă nu există gymData, returnăm un obiect cu valori implicite
                return new GymDetailDto();
            }

            // Extragem datele de la gymData
            var gymName = gymData.GetProperty("name").GetString();
            var gymAddress = gymData.GetProperty("address").GetString();
            var phoneNumber = gymData.GetProperty("phoneNumber").GetString();

            // Extragem weekBusinessHours
            var weekBusinessHours = gymResponseJson.GetProperty("weekBusinessHours").EnumerateArray();

            // Creăm lista de ore de lucru
            var workingHours = weekBusinessHours
                .Where(bh => bh.GetProperty("isClosed").GetBoolean() == false) // Luăm doar zilele deschise
                .Select(bh =>
                {
                    var day = Enum.GetName(typeof(DayOfWeek), bh.GetProperty("dayOfWeek").GetInt32());
                    var openingHour = bh.GetProperty("openingHour").GetString();
                    var closingTime = bh.GetProperty("closingTime").GetString();
                    return $"{day}: {openingHour} - {closingTime}";
                })
                .ToList();

            // Adăugăm zilele închise în lista de ore de lucru
            var closedDays = weekBusinessHours
                .Where(bh => bh.GetProperty("isClosed").GetBoolean())
                .Select(bh => $"{Enum.GetName(typeof(DayOfWeek), bh.GetProperty("dayOfWeek").GetInt32())}: Closed")
                .ToList();

            workingHours.AddRange(closedDays);

            // Creăm DTO-ul pentru sală
            return new GymDetailDto
            {
                GymName = gymName,
                GymAddress = gymAddress,
                WorkingHours = workingHours, 
                PhoneNumber = phoneNumber
            };
        }

    }
}
