using System.ComponentModel.DataAnnotations;

namespace FitZone.AuthService.Dtos
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email Address is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }

}
