using Microsoft.EntityFrameworkCore;
using PublicHolidays.Domain.Entities;

namespace PublicHolidays.Domain
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Country> Countries { get; set; }
        public DbSet<Holiday> Holidays { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>()
            .HasIndex(c => c.Code)
            .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
