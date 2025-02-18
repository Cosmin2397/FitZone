using System;
using System.Collections.Generic;
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
    }
}
