namespace FitZone.AuthService.Dtos
{
    public class EmployeeInfoModel
    {
        public Guid GymId { get; set; }
            
        public Guid RoleId { get; set; } 
        
        public string FullName { get; set; }
            
        public string PhoneNumber { get; set; }
            
        public DateTime Birthday { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; } = DateTime.MaxValue;

        public decimal MonthlyPayment { get; set; }
    }
}
