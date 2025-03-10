using FitZone.AuthService.Entities;

namespace FitZone.AuthService.Dtos
{
    public class UserDto
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }


        public string LastName { get; set; }

        public string? PhoneNumber { get; set; }

        public List<string> Roles { get; set; } = new();

        public void ConvertFromApplicationUser(ApplicationUser user)
        {
            Id = user.Id;
            Email = user.Email;
            FirstName = user.FirstName;
            LastName = user.LastName;
            PhoneNumber = user.PhoneNumber;
        }
    }

}
