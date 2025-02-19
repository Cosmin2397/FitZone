using FitZone.EmployeeManagement.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitZone.EmployeeManagement.Domain.ValueObjects
{
    public class EmployeeId
    {
        public Guid Value { get; }
        private EmployeeId(Guid value) => Value = value;
        public static EmployeeId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);
            if (value == Guid.Empty)
            {
                throw new DomainException("EmployeeId cannot be empty.");
            }

            return new EmployeeId(value);
        }
    }
}
