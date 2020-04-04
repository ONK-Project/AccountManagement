using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountManagement.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Models;

namespace AccountManagement.Services
{
    
    public class AccountService : IAccountService
    {
        private IConfiguration Configuration;
        private DbContextOptions<AccountManagementDbContext> Options;

        public AccountService(IConfiguration configuration)
        {
            Configuration = configuration;

            Options = new DbContextOptionsBuilder<AccountManagementDbContext>()
                .UseSqlServer(Configuration.GetConnectionString("AccoutManagementDBConnection"))
                .Options;
        }

        public async Task<List<Account>> GetAccounts()
        {
            using(var context = new AccountManagementDbContext(Options))
            {
                var accounts = await context.Accounts
                    .Include(a => a.MeteringUnits)
                    .ToListAsync<Account>();
                return accounts;
            }
        }

        public async Task<Account> GetAccount(long id)
        {
            using (var context = new AccountManagementDbContext(Options))
            {
                return await context
                    .Accounts
                    .Include(a => a.MeteringUnits)
                    .FirstAsync(a => a.AccountId == id);
            }
        }

        public bool CreateDB()
        {
            using (var context = new AccountManagementDbContext(Options))
            {
                if (true && (context.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists())
                    return false;

                context.Database.EnsureDeleted();
                return context.Database.EnsureCreated();
            }
        }
    }
}
