using FitZone.EmployeeManagement.Domain.Abstractions;
using FitZone.EmployeeManagement.Domain.Enums;
using FitZone.EmployeeManagement.Domain.Events;
using FitZone.EmployeeManagement.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FitZone.EmployeeManagement.Domain.Models
{
    public class Employee : Aggregate<EmployeeId>
    {
        public readonly List<EmployeeContract> _empoyeeContracts = new();

        public IReadOnlyList<EmployeeContract> EmployeeContracts => _empoyeeContracts.AsReadOnly();

        public Guid GymId { get; private set; }

        public Guid RoleId { get; private set; }

        public EmployeeStatus Status { get; private set; }

        public FullName FullName { get; private set; }

        public PhoneNumber PhoneNumber { get; private set; }

        public DateTime Birthday { get; private set; }


        public static Employee Create(EmployeeId id, Guid gymId, Guid roleId,  FullName fullName, PhoneNumber phoneNumber, DateTime birthday)
        {
            var employee = new Employee
            {
                Id = id,
                GymId = gymId,
                RoleId = roleId,
                Status = EmployeeStatus.Active,
                FullName = fullName,
                PhoneNumber = phoneNumber,
                Birthday = birthday
            };
            employee.AddDomainEvent(new EmployeeCreatedEvent(employee));
            return employee;
        }

        public void Update(Guid gymId, Guid roleId, FullName fullName, PhoneNumber phoneNumber, DateTime birthday)
        {
            RoleId = roleId;
            FullName = fullName;
            PhoneNumber = phoneNumber;
            Birthday = birthday;
            AddDomainEvent(new EmployeeUpdatedEvent(this));
        }

        public void UpdateStatus(EmployeeStatus status)
        {
            Status = status;
            AddDomainEvent(new EmployeeUpdatedEvent(this));
        }

        public void Add(EmployeeContractId contractId, DateTime startDate, decimal monthlyPayment)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(monthlyPayment);

            var employeeContract = new EmployeeContract(Id, startDate, DateTime.MaxValue, monthlyPayment);
            _empoyeeContracts.Add(employeeContract);
        }

        public void Remove(EmployeeContractId contractId)
        {
            var contract = _empoyeeContracts.FirstOrDefault(x => x.Id == contractId);
            if (contract is not null)
            {
                contract.EndDate = DateTime.Now;
            }
        }

    }
}
