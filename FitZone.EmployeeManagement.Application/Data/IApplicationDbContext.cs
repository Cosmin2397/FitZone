using FitZone.EmployeeManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitZone.EmployeeManagement.Application.Data
{
    public interface IApplicationDbContext
    {
        DbSet<Employee> Employees { get; }

        DbSet<EmployeeContract> EmployeesContracts { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
