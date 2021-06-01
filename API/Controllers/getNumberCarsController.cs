using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class getNumberCarsController : ControllerBase
    {
        [HttpGet]
        public Number Get()
        {
            ApiWorkingMethods api = new ApiWorkingMethods();
            api.StaticInfo();
            return Converting();
        }

        private Number Converting()
        {
            Number ret = new Number();
            ret.data = ApiWorkingMethods.Staticcars.cars.Length;
            return ret;
        }
    }
}
