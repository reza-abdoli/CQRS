using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HR_Management_Identity.Models;
using Microsoft.AspNetCore.Identity;
using Xunit;

namespace HR_Management_UnitTests.Common
{
    public class GetHasherTest
    {
        [Fact]
        public void Test1()
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            string hash = hasher.HashPassword(null, "P@ssword1");
            Console.WriteLine(hash);
        }
    }
}