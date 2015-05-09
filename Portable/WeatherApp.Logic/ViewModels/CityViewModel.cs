using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Logic.Models;
using Assisticant.Fields;

namespace WeatherApp.Logic.ViewModels
{
    public class CityViewModel
    {
        private readonly City _city;

        private Observable<string> _message = new Observable<string>();

		public CityViewModel(City city)
        {
            _city = city;
        }

        public string Name
        {
            get { return _city.Name; }
        }

        public IEnumerable<ForecastViewModel> Forecasts
        {
            get
            {
                return
                    from forecast in _city.Forecasts
                    select new ForecastViewModel(forecast);
            }
        }
    }
}
