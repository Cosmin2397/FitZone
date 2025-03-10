using FitZone.AuthService.Dtos;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FitZone.AuthService.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "First Name is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        public string LastName { get; set; }

        [RegularExpression(@"^(\+4|0)[0-9]{9}$", ErrorMessage = "Phone number is required.")]
        public string? PhoneNumber { get; set; }


        public string? RefreshToken { get; set; } = string.Empty;

        public Guid? GymId { get; set; }

        public DateTime? RefreshTokenExpiry { get; set; } = DateTime.MinValue;
    }

}
