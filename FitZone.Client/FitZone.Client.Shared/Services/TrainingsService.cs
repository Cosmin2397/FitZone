using FitZone.Client.Shared.DTOs;
using FitZone.Client.Shared.Services.Interfaces;
using FitZone.Client.Shared.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace FitZone.Client.Shared.Services
{
    public class TrainingsService : ITrainingsService
    {
        private readonly HttpClient _httpClient;

        public TrainingsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<List<TrainingDto>> GetPeriodTrainings(DateTime startDate, DateTime endDate, string type)
        {
            try
            {
                Guid gymId = UserState.Instance.GetSubscription.GymDetails.GymId;
                //test
                gymId = Guid.Parse("3FA85F64-5717-4562-B3FC-2C963F66AFA6");
                // Trimiți cererea API pentru a obține informațiile despre antrenamente
                string url = ($"/scheduleService/Trainings?startDate={startDate}&endDate={endDate}&gymId={gymId}&type={type}");
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var trainings = await response.Content.ReadFromJsonAsync<List<TrainingDto>>();
                    return trainings;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<TrainingScheduleDto> GetScheduleById(Guid id)
        {
            try
            {
                // Trimiți cererea API pentru a obține informațiile despre programare
                string url = ($"/scheduleService/Trainings/schedule/{id}");
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var schedule = await response.Content.ReadFromJsonAsync<TrainingScheduleDto>();
                    return schedule;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<TrainingDto> GetTrainingById(Guid id)
        {
            try
            {
                // Trimiți cererea API pentru a obține informațiile despre antrenament
                string url = ($"/scheduleService/Trainings/{id}");
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var training = await response.Content.ReadFromJsonAsync<TrainingDto>();
                    return training;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<List<TrainingDto>> GetTrainingsByClientId(Guid clientId)
        {
            try
            {
                // Trimiți cererea API pentru a obține informațiile despre antrenamente
                string url = ($"/scheduleService/Trainings/list/{clientId}");
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var trainings = await response.Content.ReadFromJsonAsync<List<TrainingDto>>();
                    return trainings;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<List<TrainingDto>> GetTrainingsByTrainerId(Guid trainerId)
        {
            try
            {
                // Trimiți cererea API pentru a obține informațiile despre antrenamente
                string url = ($"/scheduleService/Trainings/trainer/{trainerId}");
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var trainings = await response.Content.ReadFromJsonAsync<List<TrainingDto>>();
                    return trainings;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
    }
}
