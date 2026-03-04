using FinalNewBankApp.Accounts;
using FinalNewBankApp.Base;
using Microsoft.EntityFrameworkCore;


namespace FinalNewBankApp.Models
{
    public class BankDbContext:DbContext
    {
        public DbSet<AccountBase> Accounts { get; set; }
        public DbSet<BankTransaction> BankTransactions { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=FARZAD\\SQLEXPRESS;Database=BankAppDB;Trusted_Connection=True;TrustServerCertificate=True");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BankAccount>();
            modelBuilder.Entity<IskAccount>();
            modelBuilder.Entity<UddevallaAccount>();
        }

    }
    }

