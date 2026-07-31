using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Management_Identity.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(new IdentityUserRole<string>
            {
                UserId="05446344-f9cc-4566-bd2c-36791b4e28ed",
                RoleId="079a5e10-e45f-4656-80c7-577664d64fc1"
            },
            new IdentityUserRole<string>
            {
                UserId="b77117e8-ebf3-41f9-8f69-65b928a2257e",
                RoleId="74a7df0b-6d11-42ca-95d6-bb0d7204d007"
            }
            );
        }
    }
}