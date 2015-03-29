using System;
using Assisticant.Fields;

namespace HelloDojo
{
    public class UserLoginModel
    {
        private Observable<string> _userName = new Observable<string>();
        private Observable<string> _password = new Observable<string>();

        public UserLoginModel()
        {
        }

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
    }
}

