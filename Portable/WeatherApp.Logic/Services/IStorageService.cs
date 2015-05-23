using System;
using System.Collections.Generic;

namespace WeatherApp.Logic.Services
{
	public interface IStorageService
	{
		List<CityMemento> LoadCities();
		void SaveCities(IEnumerable<CityMemento> cities);

		List<ForecastMemento> LoadForecasts(string cityName);
		void SaveForecasts(string cityName, IEnumerable<ForecastMemento> forecasts);
	}
}

