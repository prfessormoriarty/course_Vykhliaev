using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace API
{
    public class ApiWorkingMethods
    {
        static object locker = new object();
        public void StaticInfo()
        {
            RunAsync().GetAwaiter().GetResult();
        }

        static HttpClient client = new HttpClient();
        public static Cars Staticcars = null;
        async Task RunAsync()
        {
            Task<string> r = GetProductAsync("https://private-anon-81fd200bd2-carsapi1.apiary-mock.com/cars");
            string s = "{\"cars\":" + r.Result + "}";
            try
            {
                Cars cars = JsonSerializer.Deserialize<Cars>(s);
                lock (locker)
                {
                    Staticcars = cars;
                }
            }
            catch (Exception e)
            {

                string z = e.ToString();
            }

        }
        async Task<string> GetProductAsync(string path)
        {
            try
            {
                string product = null;
                System.Net.Http.HttpResponseMessage response = await client.GetAsync(path);
                if (response.IsSuccessStatusCode)
                {
                    product = await response.Content.ReadAsStringAsync();
                }
                return product;
            }
            catch (Exception e)
            {

                return e.Message + e;
            }

        }
    }
}
