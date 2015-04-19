Homework 4
==========

For the 4th Dojo, we will be navigating between pages in a mobile app. The mechanism for doing so is very different between iOS and Android, so this video includes both.

Start by creating a `ViewModelLocator` class. This will be my "IOC" for view models. This is just going to be a singleton that produces view models from its properties.

```c#
public class ViewModelLocator
{
	private static ViewModelLocator _instance = new ViewModelLocator();

	private AddressBook _addressBook = new AddressBook();
	private PersonSelection _personSelection = new PersonSelection();

	public static ViewModelLocator Instance
	{
		get { return _instance; }
	}

	private ViewModelLocator()
	{
	}

	public AddressBookViewModel Main
	{
		get
		{
			return new AddressBookViewModel(
				_addressBook,
				_personSelection);
		}
	}
}
```

Now we can use that view model locator in the activity or view controller to get the view model.

```c#
private AddressBookViewModel _viewModel = ViewModelLocator.Instance.Main;
```

##Android Activity

On Android, create a new Activity called `DetailActivity`. Also create a new Layout called `Detail.axml` and populate it with controls to edit a person. In the example, I have fields for Name, Address, Email, and Phone. Then add a couple of buttons for "OK" and "Cancel", which we will use later.

Associate the activity with the layout by adding this call to the `OnCreate` method:

```c#
SetContentView(Resource.Layout.Detail);
```

##iOS View Controller

On iOS, create a new Navigation Controller in the storyboard. We'll need to switch the starting controller to this one, so drag the entry point to the Navigation Controller.

Layout the UI from the original page in the Root View Controller. You can change the class to the existing `ListerViewController`.

Now you can drop a new View Controller onto the storyboard. Enter a class name of `PersonDetailViewController` to create that class. Then hold Ctrl and drag from the Root View Controller to the new View Controller. This creates a segue to navigate between the two. Give the segue the identifier `showDetails`.

Layout the detail controls in this new view.

##Binding to Navigation

On either platform, create a binding for SelectedPerson. This binding will call a method whenever SelectedPerson changes.

```c#
_bindings.Bind(
	() => _viewModel.SelectedPerson,
	p => OnPersonSelected(p));
```

Create this new method. If the person is not null, then perform the navigation. In iOS:

```c#
private void OnPersonSelected(PersonViewModel person)
{
	if (person != null)
	{
		PerformSegue("showDetails", this);
	}
}
```

And in Android:

```c#
private void OnPersonSelected(PersonViewModel person)
{
	if (person != null)
	{
		StartActivity(typeof(DetailActivity));
	}
}
```

To set the `SelectedPerson` property, bind it to the list. Add the two lines within the `BindItems` call:

```c#
_bindings.BindItems(
	FindViewById<ListView>(Resource.Id.listPeople),
	() => _viewModel.People,
	Android.Resource.Layout.SimpleListItem1,

	() => _viewModel.SelectedPerson,
	p => _viewModel.SelectedPerson = p,

	(view, person, bindings) =>
	{
		bindings.BindText(
			view.FindViewById<TextView>(Android.Resource.Id.Text1),
			() => person.Name);
	});
``` 

##Clearing the Selection

When we return to the master page, we want to clear the selection. In Android, this is done in the `OnResume` method. In iOS, this is done in `ViewWillAppear`. In either case, just set the `SelectedPerson` to null.

##Detail View Model

Create a new class called `PersonDetailViewModel`. This will be the view model for the detail page. Inject a `Person` into this view model and set up properties.

```c#
public class PersonDetailViewModel
{
	private readonly Person _person;

	public PersonDetailViewModel(Person person)
	{
		_person = person;
	}

	public string Name
	{
		get { return _person.Name; }
		set { _person.Name = value; }
	}
}
```

Then create this view model from the `ViewModelLocator`:

```c#
public PersonDetailViewModel Detail
{
	get
	{
		if (_personSelection.SelectedPerson == null)
			return null;
		else
			return new PersonDetailViewModel(
				_personSelection.SelectedPerson);
	}
}
```

Now you can locate the view model from the `DetailActivity` or `PersonDetailViewController`.

##Your Homework

Finish data binding all of the details. Then create a copy of the person and data bind to that copy. On OK, copy all of the details back to the `Person`. On either OK or Cancel, navigate back to the master list.

Good luck! Let me know if you need help.