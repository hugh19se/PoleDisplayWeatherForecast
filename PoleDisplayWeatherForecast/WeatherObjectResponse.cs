using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoleDisplayWeatherForecast
{
    public class WeatherObjectResponse
    {
        public Current current { get; set; }
    }
    public class Current { 
        public Condition condition { get; set; }
        public double feelslike_c { get; set; }
    }

    public class Condition {
        public string text { get; set; }
    }
}
