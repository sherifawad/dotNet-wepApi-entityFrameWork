using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dotNet_wepApi_entityFrameWork.Data.Config
{
    public class PositionConfiguration : IEntityTypeConfiguration<Position>
    {
        public void Configure(EntityTypeBuilder<Position> builder)
        {

            builder.HasKey(p => p.Code);
            builder.HasIndex(p => p.Name).IsUnique();
            builder.HasData(
                new Position { Code = 1, Name = "Manager" },
                new Position { Code = 2, Name = "HR" },
                new Position { Code = 3, Name = "Programmer" },
                new Position { Code = 4, Name = "Accountant" }
            );
        }
    }
}