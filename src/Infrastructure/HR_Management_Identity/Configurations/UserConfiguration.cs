using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HR_Management_Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Management_Identity.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            // var hasher = new PasswordHasher<ApplicationUser>();
            builder.HasData(
                new ApplicationUser
                {
                    Id = "05446344-f9cc-4566-bd2c-36791b4e28ed",
                    Email = "admin@localhost.com",
                    NormalizedEmail = "ADMIN@LOCALHOST.COM",
                    FirstName = "Admin",
                    LastName = "Adminian",
                    UserName = "admin@localhost.com",
                    NormalizedUserName = "ADMIN@LOCALHOST.COM",
                    PasswordHash = "AQAAAAIAAYagAAAAEB71srXiOIbY7D54EEJILretQmD+lr/HetNlPZxiL5u/pRvwGv7bBNiVe6jvnyv33g==",
                    EmailConfirmed = true,
                    SecurityStamp = "STATIC-STAMP-1",
                    ConcurrencyStamp = "STATIC-CONCURRENCY-1",
                },
                new ApplicationUser
                {
                    Id = "b77117e8-ebf3-41f9-8f69-65b928a2257e",
                    Email = "User@localhost.com",
                    NormalizedEmail = "USER@LOCALHOST.COM",
                    FirstName = "System",
                    LastName = "User",
                    UserName = "User@localhost.com",
                    NormalizedUserName = "USER@LOCALHOST.COM",
                    PasswordHash = "AQAAAAIAAYagAAAAEB71srXiOIbY7D54EEJILretQmD+lr/HetNlPZxiL5u/pRvwGv7bBNiVe6jvnyv33g==",
                    EmailConfirmed = true,
                    SecurityStamp = "STATIC-STAMP-2",
                    ConcurrencyStamp = "STATIC-CONCURRENCY-2",
                }
            );

        }
    }
}