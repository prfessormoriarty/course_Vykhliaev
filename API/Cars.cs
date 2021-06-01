using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API
{
    public class Cars
    {
        public Car[] cars { get; set; }
    }
    public class Car
    {
        public int year { get; set; }
        public int id { get; set; }
        public int horsepower { get; set; }
        public string make { get; set; }
        public string model { get; set; }
        public float price { get; set; }
        public string img_url { get; set; }
    }

    public class Data
    {
        public AutoDataStructure[] data { get; set; }
    }
    public class AutoDataStructure
    {
        public int aid { get; set; }
        public string model { get; set; }
        public int year { get; set; }
        public string manufacturer { get; set; }
    }

    public class Number
    {
        public int data { get; set; }
    }

    public class Manufacturers
    {
        public string[] data { get; set; }
    }

    public class Years
    {
        public string[] data { get; set; }
    }

    public class UrlData
    {
        public Url[] data { get; set; }
    }
    public class Url
    {
        public int aid { get; set; }
        public string img_url { get; set; }
    }
}
