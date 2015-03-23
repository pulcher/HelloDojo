using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace HelloDojo
{
	[Activity(Label = "HelloDojo", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		private UserLogin _userLogin = new UserLogin();

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			SetContentView(Resource.Layout.Main);

			Button button = FindViewById<Button>(Resource.Id.myButton);
			EditText userName = FindViewById<EditText>(Resource.Id.userName);
			EditText password = FindViewById<EditText>(Resource.Id.password);
			TextView welcome = FindViewById<TextView>(Resource.Id.welcome);

			userName.Text = _userLogin.UserName;
			password.Text = _userLogin.Password;
			welcome.Text = _userLogin.Message;
			
			button.Click += delegate
			{
				_userLogin.UserName = userName.Text;
				_userLogin.Password = password.Text;
				_userLogin.Login();
				welcome.Text = _userLogin.Message;
			};
		}
	}
}


