using FitZone.EmployeeManagement.Domain.Enums;
using FitZone.EmployeeManagement.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitZone.EmployeeManagement.Domain.ValueObjects;

namespace FitZone.EmployeeManagement.Infrastructure.Data.Configurations
{
    public class EmployeeContractsConfiguration : IEntityTypeConfiguration<EmployeeContract>
    {
        public void Configure(EntityTypeBuilder<EmployeeContract> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasConversion(
                employeeContractId => employeeContractId.Value,
                dbId => EmployeeContractId.Of(dbId));

            builder.Property(oi => oi.StartDate).IsRequired();

            builder.Property(oi => oi.EndDate).IsRequired();

            builder.Property(oi => oi.MonthlyPayment).IsRequired();
        }
    }
}
