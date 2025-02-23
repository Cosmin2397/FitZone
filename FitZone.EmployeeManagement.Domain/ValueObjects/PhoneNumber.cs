using FitZone.EmployeeManagement.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitZone.EmployeeManagement.Domain.ValueObjects
{
    public record PhoneNumber
    {
        public int CountryCode { get; } = default!;

        public int RemainingLetters { get; } = default!;

        public PhoneNumber()
        {
            
        }

        public PhoneNumber(int countryCode, int remainingLetters)
        {
            CountryCode = countryCode;
            RemainingLetters = remainingLetters;
        }

        public static PhoneNumber Of(int countryCode, int remainingLetters)
        {
            ArgumentNullException.ThrowIfNull(countryCode);
            ArgumentNullException.ThrowIfNull(remainingLetters);

            if (countryCode == 0)
            {
                throw new DomainException("Country Code should be greater than 0.");
            }
            if (remainingLetters == 0)
            {
                throw new DomainException("Invalid phone number.");
            }

            return new PhoneNumber(countryCode, remainingLetters);
        }
    }
}
