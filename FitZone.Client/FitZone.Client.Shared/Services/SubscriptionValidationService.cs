using FitZone.Client.Shared.DTOs.Access;
using FitZone.Client.Shared.DTOs.Auth;
using FitZone.Client.Shared.Services.Interfaces;
using FitZone.Client.Shared.Utilities;
using Microsoft.AspNet.Identity;
using System.Collections.Specialized;
using System.Net.Http.Json;
using System.Text;

namespace FitZone.Client.Shared.Services
{
    public class SubscriptionValidationService: ISubscriptionValidationService
    {
        private readonly HttpClient _httpClient;

        public SubscriptionValidationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ClientAccessDto>> GetClientsAccesses(DateTime startDate, DateTime endDate)
        {
            try
            {
                Guid gymId = UserState.Instance.GetSubscription.GymDetails.GymId;
                //test
                gymId = Guid.Parse("F2A0176F-AAAA-48ED-AD3E-38F4A99F477D");
                string url = $"/validationService/Validations/clientacceses?gymId={gymId}&startDate={startDate}&endDate={endDate}";

                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var accesses = await response.Content.ReadFromJsonAsync<List<ClientAccessDto>>();
                    if (accesses != null && accesses.Count > 0)
                    {
                        foreach (var acces in accesses)
                        {
                            acces.ClientName = await GetUserName(acces.ClientId);
                        }
                    }
                    return accesses;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return new List<ClientAccessDto>();
        }


        public async Task<List<ClientAccessDto>> GetEmployeesAccesses(DateTime startDate, DateTime endDate)
        {
            try
            {
                Guid gymId = UserState.Instance.GetSubscription.GymDetails.GymId;
                gymId = Guid.Parse("8ECBFE26-5329-4E0A-AA29-F0D6B908681D");
                string url = $"/validationService/Validations/employeesacceses?gymId={gymId}&startDate={startDate:O}&endDate={endDate:O}";

                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var accesses = await  response.Content.ReadFromJsonAsync<List<ClientAccessDto>>();
                    if(accesses != null && accesses.Count > 0)
                    {
                        foreach(var acces in accesses)
                        {
                            acces.ClientName = await GetUserName(acces.ClientId);
                        }
                    }
                    return accesses;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return new List<ClientAccessDto>();
        }

        private async Task<string> GetUserName(Guid clientId)
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
