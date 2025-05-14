using FitZone.Client.Shared.DTOs;
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
