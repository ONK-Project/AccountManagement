using AccountManagement.Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountManagement.Services
{
    public interface IMeteringUnitService
    {
        Task<List<MeteringUnit>> GetMeteringUnits();
        Task<MeteringUnit> GetMeteringUnit(long Id);
        bool CreateDB();
    }
}
