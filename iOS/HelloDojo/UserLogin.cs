using System;

using Assisticant.Fields;

namespace HelloDojo
{
    public class UserLogin
    {
        private Observable<string> _userName = new Observable<string>();
        private Observable<string> _password = new Observable<string>();
        private Observable<string> _message = new Observable<string>();

        public string UserName {
            get { return _userName; }
            set { _userName.Value = value; }
        }

        public string Password {
            get { return _password; }
            set { _password.Value = value; }
        }

        public void Login() {
            _message.Value = string.Format (
                "Welcome, {0}, your password is {1}",
                _userName.Value, _password.Value);
        }

        public string Message {
            get { return _message; }
        }
    }
}

