using System;
using UIKit;
using Assisticant.Binding;


namespace HelloDojo
{
	public partial class HelloDojoViewController : UIViewController
	{

        private BindingManager _bindings = new BindingManager();

		private UserLogin _userLogin = new UserLogin();

		static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		public HelloDojoViewController (IntPtr handle) : base (handle)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		#region View lifecycle

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

            _bindings.Initialize(this);
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);

            _bindings.BindText(userNameTextField, () => _userLogin.UserLoginModel.UserName, s => _userLogin.UserLoginModel.UserName = s);
            _bindings.BindText(passwordTextField, () => _userLogin.UserLoginModel.Password, s => _userLogin.UserLoginModel.Password = s);
            _bindings.BindText(welcomeLabel, () => _userLogin.Message);

            _bindings.BindCommand(loginButton, _userLogin.Login, 
                () => !string.IsNullOrEmpty(_userLogin.UserLoginModel.UserName) && !string.IsNullOrEmpty(_userLogin.UserLoginModel.Password));
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
		}

		public override void ViewDidDisappear (bool animated)
        {            
            _bindings.Unbind();
			base.ViewDidDisappear (animated);
        }

		#endregion
	}
}

