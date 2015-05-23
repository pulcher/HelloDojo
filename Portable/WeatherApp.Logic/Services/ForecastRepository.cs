using System;
using WeatherApp.Logic.Services;
using System.Threading.Tasks;
using WeatherApp.Logic.Models;
using System.Collections.Generic;

namespace WeatherApp.Logic.Services
{
	public class ForecastRepository
	{
		private readonly IStorageService _storageService;
		private readonly IWeatherServiceAgent _weatherServiceAgent;
		private readonly Document _document;

		public ForecastRepository(
			IStorageService storageService,
			IWeatherServiceAgent weatherServiceAgent,
			Document document)
		{
			_weatherServiceAgent = weatherServiceAgent;
			_storageService = storageService;
			_document = document;
		}

		public async Task Refresh(City city)
		{
			await _weatherServiceAgent.Refresh();
		}
	}
}

