﻿using Client.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.DAL.Data.Configurations
{
    internal class DepartmentConfigurations : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            //fluent Apis for "Department" Domain
            builder.Property(x => x.Id).UseIdentityColumn(10, 10);
            builder.Property(x => x.Name).HasColumnType("varchar").HasMaxLength(50).IsRequired();
            builder.Property(x => x.Code).HasColumnType("varchar").HasMaxLength(50).IsRequired();
            builder.HasMany(x => x.Employees).WithOne(E => E.Department).HasForeignKey(E => E.DepartmentId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
