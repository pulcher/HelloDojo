using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WeatherApp.Logic.Models;

namespace WeatherApp.Logic.Services
{
    public class WeatherServiceAgent
    {
        private readonly HttpClient _httpClient;

        public WeatherServiceAgent(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task Refresh(Document document)
        {
            foreach (var city in document.Cities)
            {
                var response = await _httpClient.GetStringAsync(string.Format("?location={0}", city.Name));
                var records = JsonConvert.DeserializeObject<IEnumerable<ForecastRecord>>(response);
                foreach (var record in records)
                {
                    var forecast = city.NewForecast();
                    forecast.Condition = record.condition;
                    forecast.DayOfWeek = Enum.GetValues(typeof(DayOfWeek))
                        .Cast<DayOfWeek>()
                        .FirstOrDefault(d => d.ToString().StartsWith(record.day_of_week));
                    forecast.High = record.high;
                    forecast.Low = record.low;
                }
            }
        }
    }
}
