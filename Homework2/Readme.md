Homework 2
==========

I tried recording last night, but lost the video due to an error in my rig. So I’ll write up the instructions and try the recording again Friday night.

## Setup

To get the latest, you will need to add a remote to the upstream repository. If you’ve used GitHub for Windows to clone your fork, then you might already have a remote called `michaellperry`. You can check with: 

```
git remote –v
```

If it’s not there, add it with:

```
git remote add michaellperry git@github.com:michaellperry/HelloDojo.git
```

Once you have the remote, you will need a clean master branch to pull into. If you’ve already done some work in master, then you’ll need to clean it up first.

Create a working branch:

```
git checkout master –b work
```

Find the SHA of the upstream branch in the log. Look for the one with my name and email address (probably “Added plist files” 1d1fe2f):

```
git log
```

Copy the SHA and move master back to that point:

```
git checkout master
git reset 1d1fe2f –hard
```

Now you can pull to master

```
git pull michaellperry master
```

Do your work in a branch, and you won’t need to clean master again.

## Assignment

Now, on to the content.

The Lister solution is in the iOS or Android folder. This solution has a view already built for you. It contains an edit box and a button where you can enter a name. Then it contains a list for all of the names that have been entered. We want to bind this UI to a set of models.

Let’s start with the AddressBook. Create a folder called `Models`, and in that folder create a class called `AddressBook`. This class will contain a list of people.

```c#
public class AddressBook
{
  private ObservableList<Person> _people =
    new ObservableList<Person>(
      new Person[] {
        new Person
        {
          Name = "Sargon"
        },
        new Person
        {
          Name = "Hammurabi"
        },
        new Person
        {
          Name = "Asherbanipal"
        }
    }
  );

  public IEnumerable<Person> People
  {
    get { return _people; }
  }

  public Person NewPerson()
  {
    Person person = new Person();
    _people.Add(person);
    return person;
  }
}
```

Notice that we are using `ObservableList<T>` This comes from the namepace `Assisticant.Collections`. We return an enumerable of the people objects as `IEnumerable` so that they cannot be changed through the property. The only way to change the list is to call `NewPerson()`.

Now, what is a Person? It’s just an object with a name:

```c#
public class Person
{
  private Observable<string> _name =
    new Observable<string>();

  public string Name
  {
    get { return _name; }
    set { _name.Value = value; }
  }
}
```

Now we can data bind this list through the view model. Create a folder called ViewModels and add the `AddressBookViewModel` class:


```c#
public class AddressBookViewModel
{
  private readonly AddressBook _addressBook;

  public AddressBookViewModel(AddressBook addressBook)
  {
    _addressBook = addressBook;
  }

  public IEnumerable<PersonViewModel> People
  {
    get
    {
      return
        from person in _addressBook.People
        select new PersonViewModel(person);
    }
  }
}
```

Notice that this is projecting `Person` objects into `PersonViewModel`. So create that projection:

```c#
public class PersonViewModel
{
  private readonly Person _person;

  public PersonViewModel(Person person)
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

Now let’s go to the `Activity` or `ViewController` and add the bindings. On Android, it looks like this:

```c#
_bindings.BindItems(
  FindViewById<ListView>(Resource.Id.listPeople),
  () => _viewModel.People,
  Android.Resource.Layout.SimpleListItem1,
  (view, person, bindings) =>
  {
    bindings.BindText(
      view.FindViewById<TextView>(Android.Resource.Id.Text1),
      () => person.Name);
  });
```

The `ListView` is labeled `listPeople`. We bind the items to the view model’s `People` property. Then we need to specify the layout for a list item.

On Android, we have a few default layouts for list items. You can find the complete list on http://developer.xamarin.com/guides/android/user_interface/working_with_listviews_and_adapters/part_3_-_customizing_a_listview's_appearance/. We’re just using the simple list item with a single line of text. This layout contains a `TextView` with the ID of `Text1`.

The second lambda takes the view (the cell of the list), the person from the `People` enumerable, and a new binding manager. We use the new binding manager to bind the person’s name to the `TextView`.

On iOS, it’s a little simpler, you don’t specify the layout.

```c#
_bindings.BindItems(
  tablePeople,
  () => _viewModel.People,
  (view, person, bindingManager) =>
  {
    bindingManager.BindText(
      view.TextLabel,
      () => person.Name);
  });
```

The view that you get back is a `UITableCellView`. You can bind to its `TextLabel` property. BTW, here’s what you would have to do if you were doing raw iOS development: http://developer.xamarin.com/guides/ios/user_interface/tables/part_2_-_populating_a_table_with_data/. I think the data binding approach is simpler.

So now you should be able to run the app and see the list. On Friday, I’ll record the video showing you how to add items to the list. If you want to give it a shot, you already have the tools you need from the last Dojo.

Good luck!
