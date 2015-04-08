using System;

using Assisticant.Fields;

namespace HelloDojo
{
	public class LoginViewModel
	{
		private readonly UserLogin _userLogin;

		public LoginViewModel(UserLogin userLogin)
		{
			_userLogin = userLogin;
		}

		public string UserName
		{
			get { return _userLogin.UserName; }
			set { _userLogin.UserName = value; }
		}

		public string Password
		{
			get { return _userLogin.Password; }
			set { _userLogin.Password = value; }
		}

		public bool CanLogIn
		{
			get
			{
				return
					!string.IsNullOrWhiteSpace(UserName) &&
					!string.IsNullOrWhiteSpace(Password);
			}
		}

		public void Login()
		{
			_userLogin.Login();
		}

		public string Welcome
		{
			get
			{
				if (_userLogin.LoggedIn == LoginState.Success)
					return string.Format(
						"Welcome, {0}. Your Password is {1}.",
						UserName, Password);
				else if (_userLogin.LoggedIn == LoginState.Error)
					return string.Format(
						"The password for {0} is invalid.",
						UserName);
				else
					return String.Empty;
			}
		}
	}
}

