using FitZone.AuthService.Entities;

namespace FitZone.AuthService.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }


        public string LastName { get; set; }

        public Guid? GymId { get; set; }

        public string? PhoneNumber { get; set; }

        public List<string> Roles { get; set; } = new();

        public void ConvertFromApplicationUser(ApplicationUser user)
        {
            if (user != null)
            {
                Id = user.Id;
                Email = user.Email;
                FirstName = user.FirstName;
                LastName = user.LastName;
                PhoneNumber = user.PhoneNumber;
                GymId = user.GymId;
            }
        }
    }

}
