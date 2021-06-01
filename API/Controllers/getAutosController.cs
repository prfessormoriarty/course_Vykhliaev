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
    public class getAutosController : ControllerBase
    {
        [HttpGet]
        public Data Get()
        {
            ApiWorkingMethods api = new ApiWorkingMethods();
            api.StaticInfo();
            return Filtr(Converting());
        }
        private Data Converting()
        {
            Data data = new Data();
            data.data = new AutoDataStructure[ApiWorkingMethods.Staticcars.cars.Length];
            for (int i = 0; i < ApiWorkingMethods.Staticcars.cars.Length; i++)
            {
                data.data[i] = new AutoDataStructure() { aid = ApiWorkingMethods.Staticcars.cars[i].id, year = ApiWorkingMethods.Staticcars.cars[i].year, manufacturer = ApiWorkingMethods.Staticcars.cars[i].make, model = ApiWorkingMethods.Staticcars.cars[i].model };
            }
            return data;
        }
        private Data Filtr(Data data)
        {
            string aid = HttpContext.Request.Query["aid"].ToString();
            string years = HttpContext.Request.Query["year"].ToString();
            string manufacturer = HttpContext.Request.Query["manufacturer"].ToString();
            data = Filtr_aid(data, aid);
            data = Filtr_year(data, years);
            data = Filtr_manufacturer(data, manufacturer);
            return data;
        }
        private Data Filtr_manufacturer(Data data, string manufacturer)
        {
            if (!string.IsNullOrEmpty(manufacturer))
            {
                try
                {
                    Data data_manufacturer = new Data();
                    int i = 0;
                    List<AutoDataStructure> datas = new List<AutoDataStructure>();
                    foreach (AutoDataStructure item in data.data)
                    {
                        if (item.manufacturer == manufacturer)
                        {
                            datas.Add(item);
                            i++;
                        }
                    }
                    data_manufacturer.data = new AutoDataStructure[datas.Count];
                    data_manufacturer.data = datas.ToArray();
                    return data_manufacturer;
                }
                catch (Exception)
                {

                }
            }
            return data;
        }
        private Data Filtr_year(Data data, string years)
        {
            if (!string.IsNullOrEmpty(years))
            {
                try
                {
                    string[] Start_Eng = years.Split(';');
                    bool flagStart = int.TryParse(Start_Eng[0], out int startyears);
                    bool flagEnd = int.TryParse(Start_Eng[1], out int endyears);
                    if (flagEnd && flagStart)
                    {
                        Data data_year = new Data();
                        int i = 0;
                        List<AutoDataStructure> datas = new List<AutoDataStructure>();
                        foreach (AutoDataStructure item in data.data)
                        {
                            if (item.year >= startyears && item.year <= endyears)
                            {
                                datas.Add(item);
                                i++;

                            }
                        }
                        data_year.data = new AutoDataStructure[datas.Count];
                        data_year.data = datas.ToArray();
                        return data_year;
                    }
                }
                catch (Exception)
                {

                }
            }
            return data;
        }
        private Data Filtr_aid(Data data, string aid)
        {
            if (!string.IsNullOrEmpty(aid))
            {
                try
                {
                    bool flag = int.TryParse(aid, out int i_aid);
                    if (flag)
                    {
                        Data data_aid = new Data();
                        data_aid.data = new AutoDataStructure[1];
                        foreach (AutoDataStructure item in data.data)
                        {
                            if (item.aid == i_aid)
                            {
                                data_aid.data[0] = new AutoDataStructure();
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
