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

        Type "



4. 
