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

		private UserLogin _userLogin = new UserLogin();

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			_bindings.Initialize(this);

			SetContentView(Resource.Layout.Main);

			_bindings.BindText(
				FindViewById<EditText>(Resource.Id.userName),
				() => _userLogin.UserName,
				s => _userLogin.UserName = s);
			_bindings.BindCommand(
				FindViewById<Button>(Resource.Id.myButton),
				() => _userLogin.Login());
			_bindings.BindText(
				FindViewById<TextView>(Resource.Id.welcome),
				() => _userLogin.Message);
		}
	}
}


