using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dotNet_wepApi_entityFrameWork.Data.Config
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.Code);
            builder.Property(e => e.Code).ValueGeneratedNever();
            builder.HasIndex(e => e.PositionCode);
            builder.HasOne<Position>(e => e.Position).WithMany().HasForeignKey(e => e.PositionCode);
        }
    }
}
