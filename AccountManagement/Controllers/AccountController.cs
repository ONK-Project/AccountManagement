using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AccountManagement.Services;
using AccountManagement.Data;
using Models;

namespace AccountManagement.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AccountController : ControllerBase
    {

        private readonly ILogger<MeteringUnitController> Logger;
        private readonly IAccountService AccountService;

        public AccountController(ILogger<MeteringUnitController> logger, IAccountService accountService)
        {
            Logger = logger;
            AccountService = accountService;
            AccountService.CreateDB();
        }

        [HttpGet]
        public async Task<List<Account>> Get()
        {
            var accounts = await AccountService.GetAccounts();
            return accounts;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<Account> Get([FromRoute] long id)
        {
            return await AccountService.GetAccount(id);
        }
    }
}
