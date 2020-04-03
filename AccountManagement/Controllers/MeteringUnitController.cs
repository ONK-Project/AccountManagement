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
    public class MeteringUnitController : ControllerBase
    {

        private readonly ILogger<MeteringUnitController> Logger;
        private readonly IMeteringUnitService MeteringUnitService;

        public MeteringUnitController(ILogger<MeteringUnitController> logger, IMeteringUnitService meteringUnitService)
        {
            Logger = logger;
            MeteringUnitService = meteringUnitService;
            MeteringUnitService.CreateDB();
        }

        [HttpGet]
        public async Task<List<MeteringUnit>> Get()
        {
            return await MeteringUnitService.GetMeteringUnits();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<MeteringUnit> Get([FromRoute] long id)
        {
            return await MeteringUnitService.GetMeteringUnit(id);
        }
    }
}
