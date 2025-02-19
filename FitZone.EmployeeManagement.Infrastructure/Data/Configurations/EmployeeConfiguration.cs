using FitZone.EmployeeManagement.Domain.Enums;
using FitZone.EmployeeManagement.Domain.Models;
using FitZone.EmployeeManagement.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitZone.EmployeeManagement.Infrastructure.Data.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasConversion(
                employeeId => employeeId.Value,
                dbId => EmployeeId.Of(dbId));

            builder.HasMany(o => o.EmployeeContracts)
                .WithOne()
                .HasForeignKey(oi => oi.EmployeeId);


            builder.ComplexProperty(
               o => o.FullName, nameBuilder =>
               {
                   nameBuilder.Property(a => a.FirstName)
                       .HasMaxLength(150)
                       .IsRequired();

                   nameBuilder.Property(a => a.MiddleName)
                        .HasMaxLength(150)
                        .IsRequired();

                   nameBuilder.Property(a => a.LastName)
                        .HasMaxLength(150)
                        .IsRequired();
               });

            builder.ComplexProperty(
               o => o.PhoneNumber, phoneBuilder =>
               {
                   phoneBuilder.Property(a => a.CountryCode)
                       .HasMaxLength(10)
                       .IsRequired();

                   phoneBuilder.Property(a => a.RemainingLetters)
                        .HasMaxLength(10)
                        .IsRequired();
               });

            builder.Property(o => o.Status)
            .HasDefaultValue(EmployeeStatus.Active)
            .HasConversion(
                s => s.ToString(),
                dbStatus => (EmployeeStatus)Enum.Parse(typeof(EmployeeStatus), dbStatus));
        }
    }
}
