using System;
using WeatherApp.Logic;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Linq;
using WeatherApp.Logic.Services;

namespace WeatherApp.Android
{
	public class AndroidStorageService : IStorageService
	{
		public List<CityMemento> LoadCities()
		{
            throw new NotImplementedException();
		}

		public void SaveCities(IEnumerable<CityMemento> cities)
		{
            throw new NotImplementedException();
        }

		public List<ForecastMemento> LoadForecasts(string cityName)
		{
            throw new NotImplementedException();
        }

		public void SaveForecasts(string cityName, IEnumerable<ForecastMemento> forecasts)
		{
            throw new NotImplementedException();
        }

		private static string GetFileName()
		{
			var documents = Environment.GetFolderPath(
				Environment.SpecialFolder.MyDocuments);
			return Path.Combine(documents, "cities.xml");
		}
	}
}

