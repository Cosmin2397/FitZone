using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitZone.EmployeeManagement.Domain.Abstractions
{
    public interface IAggregate<T> : IAggregate, IEntity<T>
    {

    }

    public interface IAggregate: IEntity
    {
        IReadOnlyList<IDomainEvent> DomainEvents { get;  }

        IDomainEvent[] ClearDomainEvents();
    }
}
