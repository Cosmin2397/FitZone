using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitZone.EmployeeManagement.Domain.Abstractions
{
    public interface IEntity<T>: IEntity
    {
        public T Id { get; set; }
    }

    public interface IEntity
    {
        public DateTime? CreatedAt { get; set; }

        public Guid? CreatedBy { get; set; }

        public DateTime? LastModified { get;set; }

        public Guid? LastModifiedBy { get; set; }

    }
}
