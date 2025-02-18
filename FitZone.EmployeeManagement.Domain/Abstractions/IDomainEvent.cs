using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitZone.EmployeeManagement.Domain.Abstractions
{
    public interface IDomainEvent: INotification
    {
        Guid EventId => Guid.NewGuid();

        public DateTime OcurredOn => DateTime.Now;

        public string EventType => GetType().AssemblyQualifiedName;
    }
}
