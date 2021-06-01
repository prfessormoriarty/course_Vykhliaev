using System;
using System.IO;
using System.Net;

namespace TG_bot
{
    public class ReguestController
    {
        public static string baseAddr = "https://course-api-vikhliaev.azurewebsites.net";
        public static string GetManufacturers()
        {
            string html = "";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseAddr + "/getManufacturers");
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }

            return html;
        }

        public static string GetAutos(string filters)
        {
            string html = "";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseAddr + "/getAutos"+filters);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }

            return html;
        }

        internal static string GetAutoImage(string filters)
        {
            string html = "";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseAddr + "/getUrl" + filters);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }

            return html;
        }

        internal static string GetYears()
        {
            string html = "";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseAddr + "/getYears");
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }

            return html;
        }

        internal static string GetNumber()
        {
            string html = "";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseAddr + "/getNumberCars");
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }

            return html;
        }
    }
}