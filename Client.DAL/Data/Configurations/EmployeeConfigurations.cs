using Client.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.DAL.Data.Configurations
{
    internal class EmployeeConfigurations : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            // Fluent APIs For "Employee"
            builder.Property(E => E.Name).HasColumnType("varchar").HasMaxLength(50).IsRequired();
            builder.Property(E=>E.Adress).IsRequired();
            builder.Property(E => E.Salary).HasColumnType("decimal(12,2)");

            builder.Property(e => e.Gender)
                .HasConversion(
                (Gender) => Gender.ToString(),
                (genderString) => (Gender)Enum.Parse(typeof(Gender), genderString, true)
                             );

            //builder.Property(e => e.EmployeeType)
            //    .HasConversion(
            //    (EmpType) => EmpType.ToString(),
            //    (genderString) => (EmpType)Enum.Parse(typeof(Gender), genderString, true)
            //                 );
                
        }
    }
}
