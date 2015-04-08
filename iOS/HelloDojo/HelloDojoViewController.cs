using System;
using System.Drawing;

using Foundation;
using UIKit;
using Assisticant.Binding;

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

            _bindings.BindText(userNameTextField,
                () => _userLogin.UserName,
                s => _userLogin.UserName = s);
            _bindings.BindCommand(loginButton,
                () => _userLogin.Login());
            _bindings.BindText(welcomeLabel,
                () => _userLogin.Message);
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
		}

		public override void ViewDidDisappear (bool animated)
		{
			base.ViewDidDisappear (animated);
		}

		#endregion
	}
}

