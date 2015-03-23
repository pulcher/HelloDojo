// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace HelloDojo
{
	[Register ("HelloDojoViewController")]
	partial class HelloDojoViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton loginButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField passwordTextField { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField userNameTextField { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel welcomeLabel { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (loginButton != null) {
				loginButton.Dispose ();
				loginButton = null;
			}
			if (passwordTextField != null) {
				passwordTextField.Dispose ();
				passwordTextField = null;
			}
			if (userNameTextField != null) {
				userNameTextField.Dispose ();
				userNameTextField = null;
			}
			if (welcomeLabel != null) {
				welcomeLabel.Dispose ();
				welcomeLabel = null;
			}
		}
	}
}
