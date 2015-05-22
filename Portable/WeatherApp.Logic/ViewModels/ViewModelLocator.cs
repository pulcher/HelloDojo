using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Logic.Models;
using Assisticant.Binding;
using WeatherApp.Logic.Services;
using System.Net.Http;

namespace WeatherApp.Logic.ViewModels
{
    public class ViewModelLocator
    {
        public static ViewModelLocator Instance { get; private set; }

		public static void Initialize(string mashapeKey)
        {
            if (Instance == null)
				Instance = new ViewModelLocator(mashapeKey);
        }

        private readonly Document _document;
        private readonly CitySelection _citySelection;

        private HttpClient _httpClient;

		private ViewModelLocator(string mashapeKey)
        {
            _document = new Document();
            _citySelection = new CitySelection();
   
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://george-vustrey-weather.p.mashape.com/api.php");
            _httpClient.DefaultRequestHeaders.Add("X-Mashape-Key", mashapeKey);
        }

        public MainViewModel Main
        {
            get
            {
                return new MainViewModel(_document, _citySelection);
            }
        }

        public CityViewModel City
        {
            get
            {
                if (_citySelection.SelectedCity == null)
                    return null;

                return new CityViewModel(
					_citySelection.SelectedCity,
                    new WeatherServiceAgent(_document, _httpClient));
            }
        }

        public NewCityViewModel NewCity
        {
            get
            {
                return new NewCityViewModel(
					_document,
					_citySelection);
            }
        }
    }
}
