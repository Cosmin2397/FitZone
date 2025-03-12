using FitZone.AuthService.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace FitZone.AuthService.Dtos
{
    public class UpdateUser
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression(@"^(\+4|0)[0-9]{9}$", ErrorMessage = "Invalid phone number.")]
        public string PhoneNumber { get; set; }
    }
}
