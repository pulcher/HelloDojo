using NUnit.Framework;
using System;
using System.Threading.Tasks;
using WeatherApp.Logic.Models;
using System.Linq;
using System.Net.Http;
using System.Configuration;
using WeatherApp.Logic.Services;

namespace WeatherApp.IntegrationTests
{
	[TestFixture()]
	public partial class WeatherApiTest
	{
		[Test()]
		public void CanGetCurrentWeather()
		{
			var document = new Document();
			var http = new HttpClient();
			string mashapeKey = ConfigurationManager.AppSettings["mashape-key"];
			http.DefaultRequestHeaders.Add("X-Mashape-Key", mashapeKey);
			http.BaseAddress = new Uri("https://george-vustrey-weather.p.mashape.com/", UriKind.Absolute);

			var dallas = document.NewCity();
			dallas.Name = "Dallas";

		    var agent = new WeatherServiceAgent(http);
            agent.Refresh(document);

			Assert.AreEqual(7, dallas.Forecasts.Count());
		}
	}
}

