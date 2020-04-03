using AccountManagement.Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountManagement.Services
{
    public interface IAccountService
    {
        Task<Account> GetAccount(long Id);
        Task<List<Account>> GetAccounts();
        bool CreateDB();
    }
}
