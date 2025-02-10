using FitZone.GymsManagement.Entities.Enums;

namespace FitZone.GymsManagement.Entities
{
    public class Gym
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public string Description { get; set; }

        public Guid ManagerId { get; set; }

        public GymStatus Status { get; set; }

    }
}
