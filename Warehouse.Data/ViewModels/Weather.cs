using System;

namespace Warehouse.Data.ViewModels
{
    public class WeatherViewModel
    {           
        public string ProductId { get; set; }           
        public float Humidity { get; set; }
        public float Temprature { get; set; }
        public float Light { get; set; }
        public string Category { get; set; }
    }
}