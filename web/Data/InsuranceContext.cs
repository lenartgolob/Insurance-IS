using web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace web.Data
{
    public class InsuranceContext : IdentityDbContext<ApplicationUser>
    {
        public InsuranceContext(DbContextOptions<InsuranceContext> options) : base(options)
        {
        }

        public DbSet<InsuranceType> InsuranceType { get; set; }
        public DbSet<InsurancePolicy> InsurancePolicy { get; set; }
        public DbSet<Insured> Insured { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<InsuranceType>().ToTable("Insurance type");
            modelBuilder.Entity<InsurancePolicy>().ToTable("Insurance policy");
            modelBuilder.Entity<Insured>().ToTable("Insured");
        }
    }
}