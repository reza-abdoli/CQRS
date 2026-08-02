using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Management_Identity.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Id = "079a5e10-e45f-4656-80c7-577664d64fc1",
                    Name = "Employee",
                    NormalizedName = "EMPLOYEE",
                    ConcurrencyStamp = "STATIC-ROLE-STAMP-1"
                },
                new IdentityRole
                {
                    Id = "74a7df0b-6d11-42ca-95d6-bb0d7204d007",
                    Name = "administrator",
                    NormalizedName = "ADMINISTRATOR",
                    ConcurrencyStamp = "STATIC-ROLE-STAMP-2"
                }
            );
        }
    }
}