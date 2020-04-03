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

    public class MeteringUnitService : IMeteringUnitService
    {
        private IConfiguration Configuration;
        private DbContextOptions<AccountManagementDbContext> Options;

        public MeteringUnitService(IConfiguration configuration)
        {
            Configuration = configuration;

            Options = new DbContextOptionsBuilder<AccountManagementDbContext>()
                .UseSqlServer(Configuration.GetConnectionString("AccoutMangementDBConnection"))
                .Options;
        }

        public async Task<List<MeteringUnit>> GetMeteringUnits()
        {
            using (var context = new AccountManagementDbContext(Options))
            {
                return context.MeteringUnits.ToList();
            }
        }

        public async Task<MeteringUnit> GetMeteringUnit(long id)
        {
            using (var context = new AccountManagementDbContext(Options))
            {
                return context
                    .MeteringUnits
                    .Where(m => m.MeteringUnitId == id)
                    .FirstOrDefault();
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
