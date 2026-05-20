using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Api.Domain.DTOs.FinanceDtos;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DataAccess.EFCore
{
    // Inside your DataAccess.EFCore project
    public class ApplicationContext : IdentityDbContext<User, Role, int>
    {
        // EF Core needs this constructor to inject options
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<Developer> Developers { get; set; }
        public DbSet<Project> Projects { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<Transaction> Transactions { get; set; }


    }
}
