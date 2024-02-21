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
            builder
                .Property(e => e.HiringDate)
                .HasConversion(
                    src =>
                        src.Kind == DateTimeKind.Utc
                            ? src
                            : DateTime.SpecifyKind(src, DateTimeKind.Utc),
                    dst =>
                        dst.Kind == DateTimeKind.Utc
                            ? dst
                            : DateTime.SpecifyKind(dst, DateTimeKind.Utc)
                );
        }
    }
}
