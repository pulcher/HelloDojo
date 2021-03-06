﻿using System;
using System.Drawing;

using Foundation;
using UIKit;
using Assisticant.Binding;

namespace Lister
{
	public partial class ListerViewController : UIViewController
	{
        private AddressBookViewModel _viewModel =
            new AddressBookViewModel(
                new AddressBook(),
                new PersonSelection());
		private BindingManager _bindings = new BindingManager();

		public ListerViewController (IntPtr handle) : base (handle)
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
			
			// Perform any additional setup after loading the view, typically from a nib.
			_bindings.Initialize (this);
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

            _bindings.BindText(
                textName,
                () => _viewModel.NewName,
                s => _viewModel.NewName = s);
            _bindings.BindCommand(
                buttonAdd,
                () => _viewModel.AddPerson(),
                () => _viewModel.CanAddPerson);
            _bindings.BindItems(
                tablePeople,
                () => _viewModel.People,
                (view, person, bindings) =>
                {
                    bindings.BindText(
                        view.TextLabel,
                        () => person.Name);
                });
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

            _bindings.Unbind();
		}

		#endregion
	}
}

