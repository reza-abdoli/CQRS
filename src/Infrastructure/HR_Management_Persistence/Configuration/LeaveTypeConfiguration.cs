using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HR_Management_Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Management_Persistence.Configuration
{
    public class LeaveTypeConfiguration : IEntityTypeConfiguration<LeaveType>
    {
        public void Configure(EntityTypeBuilder<LeaveType> builder)
        {
            builder.HasData(
                new LeaveType
                {
                    Id=1,
                    Name = "vacation",
                    DefaultDay = 10
                },
                new LeaveType
                {
                    Id=2,
                    Name = "sick",
                    DefaultDay = 12
                }
            );
        }
    }
}