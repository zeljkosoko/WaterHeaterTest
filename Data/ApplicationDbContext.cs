using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace WaterHeaterTest.Data
{
    public class ApplicationDbContext : IdentityDbContext<LoginUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<LoginUser> LoginUsers { get; set; }
    }
}
