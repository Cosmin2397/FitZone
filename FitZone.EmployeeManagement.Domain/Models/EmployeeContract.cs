using FitZone.EmployeeManagement.Domain.Abstractions;
using FitZone.EmployeeManagement.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitZone.EmployeeManagement.Domain.Models
{
    public class EmployeeContract: Entity<EmployeeContractId>
    {
        public EmployeeId EmployeeId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal MonthlyPayment { get; set; }

        public EmployeeContract(EmployeeId employeeId, DateTime startDate, DateTime endDate, decimal monthlyPayment)
        {
            EmployeeId = employeeId;
            StartDate = startDate;
            EndDate = endDate;
            MonthlyPayment = monthlyPayment;
        }
    }
}
