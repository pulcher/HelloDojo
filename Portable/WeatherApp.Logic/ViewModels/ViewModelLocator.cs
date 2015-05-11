using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Logic.Models;
using Assisticant.Binding;

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

		private BindingManager _bindings = new BindingManager();

		private ViewModelLocator(string mashapeKey)
        {
            _document = new Document();
            _citySelection = new CitySelection();
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
					_citySelection.SelectedCity);
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
