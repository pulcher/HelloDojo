using System;

namespace HelloDojo
{
    public class UserLogin
    {
        private string _userName;
        private string _password;
        private string _message;

        public string UserName {
            get { return _userName; }
            set { _userName = value; }
        }

        public string Password {
            get { return _password; }
            set { _password = value; }
        }

        public void Login() {
            _message = string.Format (
                "Welcome, {0}, your password is {1}",
                _userName, _password);
        }

        public string Message {
            get { return _message; }
        }
    }
}

