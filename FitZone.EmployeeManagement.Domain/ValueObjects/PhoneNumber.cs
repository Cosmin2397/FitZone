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
    }
}
