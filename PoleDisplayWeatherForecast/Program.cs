using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PoleDisplayWeatherForecast
{
    internal class Program
    {
        private static SerialPort serial = new SerialPort("COM3", 9600, Parity.None, 8, StopBits.One);
        static void Main(string[] args)
        {
            serial.DtrEnable = true;
            if (!serial.IsOpen)
            {
                serial.Open();
            }
            Console.WriteLine("Enter Location");
            string location = Console.ReadLine();
            while (true)
            {
                AddToDisplay(GetWeather(location));
                Thread.Sleep(300000);
            }            
        }

        private static WeatherObjectResponse GetWeather(string location) {
            var cli = new API();
            return cli.SendAndGetResponseAsync<WeatherObjectResponse>($"current.json?key={ConfigurationManager.AppSettings["APIKEY"]}&q={location}").Result;
        }

        private static void AddToDisplay(WeatherObjectResponse weatherInfo) {
            serial.Write("".PadRight(40, ' '));
            string lineOne = $"Temperature: {weatherInfo.current.feelslike_c} C".PadRight(20, ' ').Substring(0, 20);
            string lineTwo = weatherInfo.current.condition.text.PadRight(20, ' ').Substring(0, 20);
            serial.Write(lineOne + lineTwo);
        }
    }
}
