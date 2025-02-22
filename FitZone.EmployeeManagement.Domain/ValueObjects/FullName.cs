using FitZone.EmployeeManagement.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitZone.EmployeeManagement.Domain.ValueObjects
{
    public record FullName
    {
        public string FirstName { get; } = default!;
        public string MiddleName { get; } = default!;
        public string LastName { get; } = default!;

        public FullName()
        {
            
        }

        public FullName(string firstName, string middleName, string lastName)
        {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
        }

        public static FullName Of(string firstName, string middleName, string lastName)
        {
            ArgumentNullException.ThrowIfNull(firstName);
            ArgumentNullException.ThrowIfNull(middleName);
            ArgumentNullException.ThrowIfNull(lastName);
            if (firstName == String.Empty)
            {
                throw new DomainException("First name cannot be empty.");
            }
            if (lastName == String.Empty)
            {
                throw new DomainException("Last name cannot be empty.");
            }

            return new FullName(firstName,middleName,lastName);
        }
    }
}
