using Assisticant.Fields;

namespace HelloDojo
{
    public class UserLogin
    {
        public UserLoginModel UserLoginModel = new UserLoginModel();
        private Observable<string> _message = new Observable<string>();

        public void Login() {
            _message.Value = string.Format (
                "Welcome, {0}, your password is {1}",
                UserLoginModel.UserName, UserLoginModel.Password);
        }

        public string Message {
            get { return _message.Value; }
        }
    }
}

