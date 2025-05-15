using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitZone.Client.Shared.DTOs.Auth
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Email Address is required!")]
        [EmailAddress(ErrorMessage = "Email Address is invalid!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password confirmation is required.")]
        [Compare("Password", ErrorMessage = "Password and password confirmation doesn't match.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression(@"^(\+4|0)[0-9]{9}$", ErrorMessage = "Invalid phone number.")]
        public string PhoneNumber { get; set; }

        public bool IsEmployee { get; set; } = false;

        public Role RoleName { get; set; }

        public Guid? GymId { get; set; }

        public EmployeeInfoModel? EmployeeData { get; set; } = null;
    }

    public class EmployeeInfoModel
    {
        public DateTime Birthday { get; set; }

        public DateTime StartDate { get; set; }

        public decimal MonthlyPayment { get; set; }
    }

    public enum Role
    {
        Client,
        Employee,
        PersonalTrainer,
        GymManager,
        AppManager
    }
}
