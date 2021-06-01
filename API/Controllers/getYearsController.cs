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
    public class getYearsController : ControllerBase
    {
        [HttpGet]
        public Years Get()
        {
            ApiWorkingMethods api = new ApiWorkingMethods();
            api.StaticInfo();
            return Converting();
        }

        private Years Converting()
        {
            string[] data = new string[ApiWorkingMethods.Staticcars.cars.Length];
            for (int i = 0; i < ApiWorkingMethods.Staticcars.cars.Length; i++)
            {
                data[i] = ApiWorkingMethods.Staticcars.cars[i].year.ToString();
            }
            string[] result = RemoveDuplicates(data);
            Years ret = new Years();
            ret.data = result;
            return ret;
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
