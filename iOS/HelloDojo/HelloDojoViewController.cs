using System;
using System.Drawing;

using Foundation;
using UIKit;

using Assisticant.Binding;

namespace HelloDojo
{
	public partial class HelloDojoViewController : UIViewController
	{
        private BindingManager _bindings = new BindingManager();

        private LoginViewModel _viewModel = new LoginViewModel(new UserLogin());

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
                () => _viewModel.UserName,
                s => _viewModel.UserName = s);
            _bindings.BindText(passwordTextField,
                () => _viewModel.Password,
                s => _viewModel.Password = s);
            _bindings.BindCommand(loginButton,
                () => _viewModel.Login(),
                () => _viewModel.CanLogIn);
            _bindings.BindText(welcomeLabel,
                () => _viewModel.Welcome);
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

