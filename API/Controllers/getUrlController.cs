using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class getUrlController : Controller
    {
        [HttpGet]
        public UrlData Get()
        {
            ApiWorkingMethods api = new ApiWorkingMethods();
            api.StaticInfo();
            return Filtr(Converting());
        }
        private UrlData Converting()
        {
            UrlData data = new UrlData();
            data.data = new Url[ApiWorkingMethods.Staticcars.cars.Length];
            for (int i = 0; i < ApiWorkingMethods.Staticcars.cars.Length; i++)
            {
                data.data[i] = new Url() { aid = ApiWorkingMethods.Staticcars.cars[i].id, img_url = ApiWorkingMethods.Staticcars.cars[i].img_url };
            }
            return data;
        }
        private UrlData Filtr(UrlData data)
        {
            string aid = HttpContext.Request.Query["aid"].ToString();
            data = Filtr_aid(data, aid);
            return data;
        }
        private UrlData Filtr_aid(UrlData data, string aid)
        {
            if (!string.IsNullOrEmpty(aid))
            {
                try
                {
                    bool flag = int.TryParse(aid, out int i_aid);
                    if (flag)
                    {
                        UrlData data_aid = new UrlData();
                        data_aid.data = new Url[1];
                        foreach (Url item in data.data)
                        {
                            if (item.aid == i_aid)
                            {
                                data_aid.data[0] = new Url();
                                data_aid.data[0] = item;
                                return data_aid;
                            }
                        }

                    }
                    return null;
                }
                catch (Exception)
                {

                }
            }
            return data;
        }
    }
}
