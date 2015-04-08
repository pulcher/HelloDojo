using System;

using Assisticant.Fields;

namespace HelloDojo
{
    public enum LoginState
    {
        NotLoggedIn,
        Error,
        Success
    }

    public class UserLogin
    {
        private Observable<string> _userName = new Observable<string>();
        private Observable<string> _password = new Observable<string>();
        private Observable<LoginState> _loggedIn = new Observable<LoginState>(
           LoginState.NotLoggedIn);

        public string UserName
        {
            get { return _userName.Value; }
            set { _userName.Value = value; }
        }

        public string Password
        {
            get { return _password.Value; }
            set { _password.Value = value; }
        }

        public void Login()
        {
            if (_password.Value == "Ponies")
                _loggedIn.Value = LoginState.Success;
            else
                _loggedIn.Value = LoginState.Error;
        }

        public LoginState LoggedIn
        {
            get { return _loggedIn.Value; }
        }
    }
}
