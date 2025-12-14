using FLoanAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FLoanAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<USER> Users { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<LogEntry> Logs { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<USER>().HasIndex(u => u.Username).IsUnique();
            modelBuilder.Entity<USER>().HasIndex(u => u.Email).IsUnique();

            
            modelBuilder.Entity<Loan>()
                .HasOne(l => l.USER)
                .WithMany(u => u.Loans)
                .HasForeignKey(l => l.USERID);

            modelBuilder.Entity<Loan>()
                .Property(l => l.Amount)
                .HasColumnType("decimal(18, 2)");

            
            modelBuilder.Entity<Loan>()
                .Property(l => l.Currency)
                .HasConversion<string>();

            modelBuilder.Entity<Loan>()
                .Property(l => l.Status)
                .HasConversion<string>();
        }
    }
}
