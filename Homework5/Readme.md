#Web Services

Most mobile apps will need to access web services. These are, after all, connected devices. Or at least occasionally. But more on that later.

We will be using a C# library called Newtonsoft JSON .NET by James Newton-King. This library alone is bigger than the Xamarin Starter limit, so we will have to exercise it with unit tests. But if you have a license, you'll be able to call web services from an Android or iOS app.

Since we'll be testing in .NET, you can use Visual Studio to perform this Dojo, even if you don't have a license. The test and Logic projects will still open for you, even if the iOS and Android projects do not.

###Portable Class Libraries

We're going to write one library that can be used in the iOS, Android, and unit test projects. So that we can use this library across several platforms, we will make it a Portable Class Library (PCL). To create a PCL in Xamarin Studio, select New Project, and then in the "Cross-platform" section choose "Libary". The WeatherApp solution in the repository already contains a PCL called WeatherApp.Logic.

The solution also contains an iOS, Android, and IntegrationTest project. The integration test is an NUnit Library Project (in the "Other" section under ".NET"). These all have a reference to the Logic project, so the code that we write there can be used on all platforms without recompiling.

These projects all have references to Assisticant, which has two parts. Things like Observable are in a PCL, so they can be used inside of other PCLs like Logic. Things like BindingManager are in platform-specific assemblies, so they can only be used in the main app.

###Models

So that we can reuse models across all platforms and within unit tests, they are in the PCL. They use Observable so that we can data bind them.

The model uses the same patterns that we've already studied, so we won't go through building them again. It's a three-level hierarchy:

* Document
* City
* Forecast

A Document contains a list of Cities. A City has a name and a list of Forecasts.

###View Models

So that we can reuse view models across devices, I've put these into the Logic project as well. I go back and forth on this practice. On the one hand, the app on iPhone, Android Phone, and Windows Phone should be very similar, and could probably use the same view models. On the other hand, iPad, Android tablet, and Windows will be a different experience with fewer, denser pages.

The ViewModelLocator currently adds a specific city to the document, and then returns its view model. When we implement local storage next time, we will finish out this pattern. The cities will be stored in a document, and the user will have a screen with which to add them. The Android app is currently data bound to the city view model, but not the iOS app.

###Mashape Keys

For this Dojo, you will need to sign up for a [Mashape](https://www.mashape.com) account. You can use several free services to try things out. The one that we will use is [Ultimate Weather Forecasts](https://www.mashape.com/george-vustrey/ultimate-weather-forecasts). Create an "Application" on the Mashape site and add this API. Then you can click "Get The Keys" to copy your API key.

My Mashape key is not checked into the source repository. You will have to enter your own. I have added the files where the keys are stored to .gitignore. These are in

* Keys.xml in the Android application
* MasterViewController.Keys.cs in the iOS application, and
* app.config in the IntegrationTests project

The readme.txt files in those various projects tell you how to create this file and enter your Mashape key.

##WeatherServiceAgent

You will be implementing the WeatherServiceAgent class. This class has an empty implementation of the Refresh method. Refresh is supposed to call the Weather API for each city in the document, and populate its forecasts. The integration test verifies that it adds seven forecasts for Dallas.

###HttpClient

To implement the service, use HttpClient. This is part of the framework, and does not count toward the Xamarin Starter limit. An HttpClient is initialized in the integration test and injected into the WeatherServiceAgent.

Construct a query string and call the GetStringAsync method. Await this call and you will get back a string. You can verify the string within the debugger and see that it looks just like the one from the Mashape Weather service.

###JSON.NET

Now you'll need to deserialize this string into a collection of objects.

First, create a class that represents a forecast record. You can copy the string from the debugger into the clipboard, or use Fiddler to hit the service directly. If you use the Mashape test page in the browser, then the response will be truncated.

Now paste this as JSON. In Visual Studio, you can use "Edit", "Paste Special", "Paste As JSON Classes". If you are using Xamarin Studio or you don't see this option, then use the [json2csharp](http://json2csharp.com/) converter to generate the class. Rename this class as ForecastRecord and add it to the Logic project.

Now you can deserialize the string. Use

```C#
JsonConvert.DeserializeObject<IEnumerable<ForecastRecord>>(response)
```

Finish the test off by clearing the forecasts from the City and adding a new Forecast for each ForecastRecord.

##Bonus Question

Why do you have to create a Forecast and a ForecastRecord? Can't you reuse one class?

I have several answers to this question, some practical and some philosophical. I go back-and-forth on some questions, but never on this one. I always have separate classes.