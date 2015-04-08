using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Assisticant.Binding;

namespace HelloDojo
{
	[Activity(Label = "HelloDojo", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		private BindingManager _bindings = new BindingManager();

		private LoginViewModel _viewModel = new LoginViewModel(new UserLogin());

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			_bindings.Initialize(this);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			_bindings.BindText(
				FindViewById<EditText>(Resource.Id.userName),
				() => _viewModel.UserName,
				s => _viewModel.UserName = s);
			_bindings.BindText(
				FindViewById<TextView>(Resource.Id.password),
				() => _viewModel.Password,
				s => _viewModel.Password = s);
			_bindings.BindCommand(
				FindViewById<Button>(Resource.Id.myButton),
				() => _viewModel.Login(),
				() => _viewModel.CanLogIn);
			_bindings.BindText(
				FindViewById<TextView>(Resource.Id.welcome),
				() => _viewModel.Welcome);
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();

			_bindings.Unbind();
		}
	}
}


