using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace AccountManagement.Data
{
    public class AccountManagementDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<MeteringUnit> MeteringUnits { get; set; }

        public AccountManagementDbContext() { }
        public AccountManagementDbContext(DbContextOptions<AccountManagementDbContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingAccount(modelBuilder);
            OnModelCreatingMeteringUnit(modelBuilder);
            onModelCreatingSeedData(modelBuilder);
        }

        private void OnModelCreatingAccount(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasMany(a => a.MeteringUnits)
                .WithOne(hv => hv.Account)
                .HasForeignKey(x => x.AccountId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        private void OnModelCreatingMeteringUnit(ModelBuilder modelBuilder)
        {
        }

        private void onModelCreatingSeedData(ModelBuilder modelBuilder)
        {
            // Seeding accounts
            List<Account> accounts = new List<Account>();

            const long numberOfAccounts = 100;
            for(long i = 1; i<=numberOfAccounts; i++)
            {
                accounts.Add(new Account()
                {
                    AccountId = i
                });
            }
            modelBuilder.Entity<Account>().HasData(accounts);

            // Seeding Meterunits
            List<MeteringUnit> meteringUnits = new List<MeteringUnit>();

            // Creating metering units
            // 1 heat og electricty pr. account
            // 2 water pr. account

            const long numberOfHeat = numberOfAccounts;
            const long numberOfElectricty = numberOfAccounts;
            const long numberOfWater = numberOfAccounts * 2;

            // Heat
            for(long i = 1; i<=numberOfHeat; i++)
            {
                meteringUnits.Add(new MeteringUnit()
                {
                    MeteringUnitId = i,
                    AccountId = i % numberOfAccounts+1, 
                    Location = $"7300, Jelling, Bakkedraget {i % numberOfAccounts}",
                    Model = "Heat - AAA",
                    Type = "Heat"
                });
            }

            // Electricty
            for(long i = 1+numberOfHeat; i<=numberOfElectricty+numberOfHeat; i++)
            {
                meteringUnits.Add(new MeteringUnit()
                {
                    MeteringUnitId = i,
                    AccountId = i % numberOfAccounts+1,
                    Location = $"7300, Jelling, Bakkedraget {i % numberOfAccounts}",
                    Model = "Electricity - AAA",
                    Type = "Electricity"
                });
            }

            // Water
            for (long i = 1 + numberOfHeat + numberOfElectricty; i <= numberOfElectricty + numberOfHeat + numberOfWater; i++)
            {
                meteringUnits.Add(new MeteringUnit()
                {
                    MeteringUnitId = i,
                    AccountId = i % numberOfAccounts+1,
                    Location = $"7300, Jelling, Bakkedraget {i % numberOfAccounts}",
                    Model = "Water - AAA",
                    Type = "Water"
                });
            }

            modelBuilder.Entity<MeteringUnit>().HasData(meteringUnits);
        }
    }
}
