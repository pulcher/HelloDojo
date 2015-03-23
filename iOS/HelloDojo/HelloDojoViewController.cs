using System;
using System.Drawing;

using Foundation;
using UIKit;

namespace HelloDojo
{
	public partial class HelloDojoViewController : UIViewController
	{
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

			userNameTextField.Text = _userLogin.UserName;
			passwordTextField.Text = _userLogin.Password;
			welcomeLabel.Text = _userLogin.Message;
			loginButton.TouchUpInside += LoginButton_TouchUpInside;
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
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

		void LoginButton_TouchUpInside(object sender, EventArgs e)
		{
			_userLogin.UserName = userNameTextField.Text;
			_userLogin.Password = passwordTextField.Text;
			_userLogin.Login ();
			welcomeLabel.Text = _userLogin.Message;
		}
	}
}

