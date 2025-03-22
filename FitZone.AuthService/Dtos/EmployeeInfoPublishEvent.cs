using FitZone.AuthService.Dtos;

namespace FitZone.AuthentificationService.Dtos
{
    public class EmployeeInfoPublishEvent
    {
        public Guid Id { get; set; }

        public Guid? GymId { get; set; }

        public Guid RoleId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime Birthday { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; } = DateTime.MaxValue;

        public decimal MonthlyPayment { get; set; }

        public void AddEmployee(EmployeeInfoModel model, Guid employeeId, Guid roleId, Guid? gymId, string firstName, string lastName, string phoneNumber)
        {
            Id = employeeId;
            RoleId = roleId;
            GymId = gymId;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber.Substring(2,9);
            Birthday = model.Birthday;
            StartDate = model.StartDate;
            MonthlyPayment = model.MonthlyPayment;
        }
    }
}
