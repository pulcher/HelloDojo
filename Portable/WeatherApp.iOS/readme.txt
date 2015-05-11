Create a MasterViewController.Keys.cs file with the following content:

using WeatherApp.Logic.ViewModels;

namespace WeatherApp
{
    public partial class MasterViewController
    {
        partial void InitializeViewModelLocator()
        {
            ViewModelLocator.Initialize("Your Mashape key");

        }
    }
}
