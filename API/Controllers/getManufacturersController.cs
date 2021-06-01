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
    public class getManufacturersController : ControllerBase
    {

        [HttpGet]
        public Manufacturers Get()
        {
            ApiWorkingMethods api = new ApiWorkingMethods();
            api.StaticInfo();
            return Converting();
        }
        private Manufacturers Converting()
        {
            Manufacturers data = new Manufacturers();
            data.data = new string[ApiWorkingMethods.Staticcars.cars.Length];
            for (int i = 0; i < ApiWorkingMethods.Staticcars.cars.Length; i++)
            {
                data.data[i] = ApiWorkingMethods.Staticcars.cars[i].make;
            }
            string[] result = RemoveDuplicates(data.data);
            data.data = result;
            return data;
        }
        private string[] RemoveDuplicates(string[] s)
        {
            HashSet<string> set = new HashSet<string>(s);
            string[] result = new string[set.Count];
            set.CopyTo(result);
            return result;
        }
    }
}
