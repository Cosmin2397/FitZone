using FitZone.GymsManagement.Entities;

namespace FitZone.GymsManagement.Dtos
{
    public class GymDTO
    {
        public Gym GymData { get; set; }

        public List<BusinessHours> WeekBusinessHours { get; set; }  
    }
}
