using System.Threading.Tasks;
using System;

namespace WeatherApp.Logic.Services
{
    public interface IWeatherServiceAgent
    {
        Task Refresh();
    }
}
