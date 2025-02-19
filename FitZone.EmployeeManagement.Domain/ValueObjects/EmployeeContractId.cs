using FitZone.EmployeeManagement.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitZone.EmployeeManagement.Domain.ValueObjects
{
    public class EmployeeContractId
    {
        public Guid Value { get; }
        private EmployeeContractId(Guid value) => Value = value;
        public static EmployeeContractId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);
            if (value == Guid.Empty)
            {
                throw new DomainException("EmployeeContractId cannot be empty.");
            }

            return new EmployeeContractId(value);
        }
    }
}
