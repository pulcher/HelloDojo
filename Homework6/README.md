# Homework 6: Local Storage

There are two reasons to save data to the local file system.

1. To persist user preferences
2. To cache data while offline

We will exercise both in the weather app.

By the way, the iOS and Android apps now inject a fake weather service agent. This removes the dependency upon Newtonsoft JSON .NET, so you can run the apps under Starter again. The real service agent is still in the folder, but is just not part of the project.

## Storage Service

The storage service is responsible for saving and loading objects. It's responsibilities are expressed as the `IStorageService` interface.

```C#
public interface IStorageService
{
	List<CityMemento> LoadCities();
	void SaveCities(IEnumerable<CityMemento> cities);

	List<ForecastMemento> LoadForecasts();
	void SaveForecasts(IEnumerable<ForecastMemento> forecasts);
}
```

Notice that these methods take and return mementos, not document objects. The reason for this is that persistence is a different concern than representing application state. Just as returning data from a service is a different concern. Because of these differences, the classes should be allowed to vary independently.

The key differentiator of a memento is that it is a `DataContract`. This lets us use the `DataContractSerializer` to save and load XML. If we still had a dependency upon Newtonsoft JSON .NET, we could use that as a JSON serializer. But the XML data contract serializer is part of the framework, and so does not impact the app size limit.

```C#
[DataContract]
public class CityMemento
{
	[DataMember]
	public string Name { get; set; }
}
```

Notice that a city memento just has a name. It does not contain forecasts, as you might expect. That's because saving and loading cities is done separately from saving and loading forecasts. If the document were one big tree, then it would have to be saved and loaded as one. We are going to do these things at separate times.

Also notice that each app has its own implementation of the `IStorageService` interface. They will end up being exactly the same code. However, the `File` object is not portable. So this code can't live in a PCL. It has to live in a platform-specific library. There is a trick we could use to create platform-specific implementations of portable class libraries, but that's beyond what we want to accomplish in today's dojo.

## Save User Preferences

The user can add a city to their preferences. The list of cities will be saved to local storage so that they don't have to enter it again.

A document should be able to generate a set of mementos to be saved. Implement the `Save()` method on the `Document` object to map `Cities` to `CityMementos`.

```C#
public List<CityMemento> Save()
{
	// Turn the _cities collection into a list of CityMemento.
}
```

You want to save the user's collection of cities every time they change it. So you can subscribe to the results of the `Save()` method. When the result changes (because the user has added or removed a city), then call the storage service. Do this in the `ViewModelLocator` constructor.

```C#
_bindings.Bind(
	() => _document.Save(),
	c => _storageService.SaveCities(c));
```

The storage service needs to write the mementos to a file.

```C#
public void SaveCities(IEnumerable<CityMemento> cities)
{
	var fileName = GetFileName();

	FileMode fileMode = File.Exists(fileName)
		? FileMode.Truncate
		: FileMode.CreateNew;

	using (var stream = new FileStream(
		fileName,
		fileMode))
	{
		var dc = new DataContractSerializer(
			typeof(DocumentMemento));

		dc.WriteObject(stream, new DocumentMemento
		{
			Cities = cities.ToList()
		});
	}
}
```

Set a breakpoint in this method and run the app. It will create an empty XML document on start up. Then add a city, and the breakpoint will be hit again. This time, it will write a document with one city.

## Load User Preferences

Now we need to load those cities back in. You can see that if you kill the app and start it again, the list is empty.

Add this code to the `ViewModelLocator` constructor just before the binding that you just added:

```C#
List<CityMemento> cities = _storageService.LoadCities();
_document.Load(cities);
```

We want to do this before we bind the `Save()` call. Otherwise, we will overwrite the file with an empty list of cities.

Write the code to load cities from the Storage Service.

```C#
public List<CityMemento> LoadCities()
{
	var fileName = GetFileName();

	if (File.Exists(fileName))
	{
		try
		{
			using (var stream = new FileStream(
                fileName,
                FileMode.Open))
			{
				var dc = new DataContractSerializer(
					typeof(DocumentMemento));

				var document = (DocumentMemento)dc.ReadObject(stream);
				return document.Cities;
			}
		}
		catch (Exception x)
		{
			// TODO: Indicate the error to the user.
		}
	}

	return new List<CityMemento>();
}

```

Once we load the city mementos from storage, we need to load them into the document.

```C#
public void Load(IEnumerable<CityMemento> cities)
{
	// Clear out the _cities collection and add a new one for each city memento.
}
```

Now when you start the app, the city that you added before should appear in the list.

## Cache Forecasts

There's nothing more frustrating than waiting for an app to load data that you know you've already seen. This is especially irksome when you lose your network connection, and you just want to see what was there before. So whenever we fetch data, we'll cache it for next time.

Rather than calling the service agent directly, we are now calling a `ForecastRepository`. This encapsulates both the service agent and the storage service.

When the user navigates to a city, the app will call the forecast repository to refresh the city. The forecast repository is currently just passing this call through to the service agent. Add the code to cache the forecasts to the storage service once the service agent is done.

```C#
public async Task Refresh(City city)
{
	await _weatherServiceAgent.Refresh();

	var forecasts = city.SaveForecasts();
	_storageService.SaveForecasts(city.Name, forecasts);
}
```

You will also need to write the code for the two `SaveForecasts`. The one on the `City` will simply return a list of forecast mementos. Then the one on the storage service will write those mementos to a file. This will be just like the `SaveCities` method, but it will select a file name based on the name of the city. This allows the app to cache a different set of forecasts for each city, and refresh only the one that the user is currently loading.

Set a breakpoint and run the app. You'll see that it saves an XML file after the service agent finishes. The service agent in this case is the fake, but it still waits 2 seconds to simulate network latency.

## Load Cached Forecasts

The forecast repository will load from the storage service first. Then it will call the service agent. That way, the user can see some data even while we are fetching an update. And if the update fails, the user is still seeing the old data.

```C#
public async Task Refresh(City city)
{
	List<ForecastMemento> forecasts =
		_storageService.LoadForecasts(city.Name);
	city.LoadForecasts(forecasts);

	await _weatherServiceAgent.Refresh();

	forecasts = city.SaveForecasts();
	_storageService.SaveForecasts(city.Name, forecasts);
}
```

Notice that the forecasts are loaded before the first `await`. That's how we are going to get data back to the user immediately. The asynchronous call will return at this point, and the UI thread will continue, showing the user the cached data. But when the service agent finishes refreshing the forecasts, then the UI will update and show the new information.

You will need to implement the `LoadForecasts(string cityName)` method of the storage service. Again, the file name is based on the name of the city.

Now set the breakpoint and see that the app is loading the forecasts when you navigate to the city. Clear the breakpoint and you can see that it immediately displays old data. After 2 seconds, the old data is replaced with new fake data.

## Conclusion

That's pretty much all that most mobile apps will do. Call services to send and receive data. Store user preferences on the local file system. And use local storage to cache data while offline.

Now take these skills and go make some apps! Let's work together to build apps that Improving can use both internally, and as publically available portfolio pieces.