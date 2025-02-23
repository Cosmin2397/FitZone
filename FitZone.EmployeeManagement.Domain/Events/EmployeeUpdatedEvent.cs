using FitZone.EmployeeManagement.Domain.Abstractions;
using FitZone.EmployeeManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitZone.EmployeeManagement.Domain.Events
{
    public record EmployeeUpdatedEvent(Employee employee) : IDomainEvent;
}
