# Build 2014 Demo

In this demo we're going to show how to use Visual Studio 2013 to build a Windows Store app that interacts with data stored in Salesforce. To do this, we're going to use a NuGet package built by Salesforce for interacting with their Force.com REST API.

    Be sure and install the snippets found in the /snippets folder before starting the following steps.

For this simple sample, we're going to query a list of accounts out of Salesforce and bind them to a grid on our main page.

1. Let's open Visual Studio and create a new project

        File -> New -> Project
        Visual C# -> Windows Store -> Blank App.
        Click OK.

2. Let's first write a little bit of Xaml. We're going to create a ListView that we can use to bind the account name and description.

        Open MainPage.xaml
        In grid type: Control-K,X, TAB, TAB

    You can see that we have added a ListView that will display the name and description returned from salesforce. So far pretty simple.

3. While we're here, let's wire up the Loaded event.

        Select the page tag
        Open properties and select events
        Double click Loaded

4. This brings us to our mainpage class. We're going to need to authenticate to salesforce in order to query their API, so let's take advangage of the web authentication broker to manage the authentication.

        Type "buildauth" and hit tab immediately within the MainPage class for the auth variables

    Salesforce uses OAuth 2 for accessing the REST API. Here we have a login URL for which we'll authenticate against and a consumer key used to identify the Connected App I've previously created. I also have a callback url which is valuable for telling the web authentication broker that we've had a successful login.

5. After a successful login we're going to get back a auth token used to make subsequent calls. These calls will be made against an instance URL returned to us by Salesforce. Let's create variables for these values.

        Type "buildvars" and hit tab immediately after the auth variables

6. Okay, now it's time to install the NuGet package created by Salesforce. It is a Portable Class Library, which means it can be used with multiple platforms - Windows Store Apps, .NET 4/4.5 apps, Windows Phone 8, and it is Mono.NET compatible so you can use it in Xamarin apps deployed to iOS or Android.

        Install-Package DeveloperForce.Force



7. We're going to create a method called GetAccessToken() that will interact with the Salesforce login API and use the Web Auth Broker to parse the response and extract the token and instance url.

        Afte MainPage method type "buildtoken" and hit tab immediately
    
    You can see we define our URIs and then extract the relevant values from the response using the web auth broker.

8. Next, let's wire up the OAuth call with GetAccessToken to Salesforce using the Web Authentication Broker.

        In the Loaded event type "buildloaded" and hit tab immediately

    You'll see there are a few things to fix up.

8. First, let's add missing references.

        Add the missing references.

9. Next, we need to make the Loaded event asynchronous.

        After private add async to the Loaded method

10. We're going to bind an Account CLR object to the ListView. To simplify, let's create a class to represent the object. The Salesforce libraries will serialize the JSON response from the REST APIs into our object.

        After the MainPage class type "buildaccount" to create the Account class
        
11. You can now see that, after we authenticate, we're usibng the ForceClient to call the Force.com REST API. We're using the instance url to define the request URL, putting the oauth token in the request header, and telling Salesforce which version of their API to use.

    Next you'll see that we make an asyncquery call, using our account object, and pass in a simple query.

    We'll take the output and bind it to the list view.

    Let's run it!

12. Hit F5 to run.

        Login: demo@build.com
        Password: buildpass

13. Click the Allow button to accept.

14. There! You can see everything running as expected.
