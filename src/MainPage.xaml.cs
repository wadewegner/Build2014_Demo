using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238
using Salesforce.Common;
using Salesforce.Common.Models;
using Salesforce.Force;

namespace App
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private const string AuthorizationEndpointUrl = "https://na15.salesforce.com/services/oauth2/authorize";
        private const string ConsumerKey = "3MVG9A2kN3Bn17htZsz6k.c8C_VXsbeOveDK9u.Fx10jXNITX7VUaEFdDAJpBoZPFIqkFYBK_6nKTJZISzI3Q";
        private const string CallbackUrl = "sfdc://success";

        private string _accessToken;
        private string _instanceUrl;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                await GetAccessToken();

                var client = new ForceClient(_instanceUrl, _accessToken, "v29.0");
                var accounts = await client.QueryAsync<Account>("SELECT id, name, description FROM Account");

                GridView1.ItemsSource = accounts.records;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
                
        }

        private async Task GetAccessToken()
        {
            var startUrl = Common.FormatAuthUrl(AuthorizationEndpointUrl, ResponseTypes.Token, ConsumerKey,
                WebUtility.UrlEncode(CallbackUrl), DisplayTypes.Popup);
            var startUri = new Uri(startUrl);
            var endUri = new Uri(CallbackUrl);

            var webAuthenticationResult =
                await Windows.Security.Authentication.Web.WebAuthenticationBroker.AuthenticateAsync(
                    Windows.Security.Authentication.Web.WebAuthenticationOptions.None,
                    startUri,
                    endUri);

            switch (webAuthenticationResult.ResponseStatus)
            {
                case Windows.Security.Authentication.Web.WebAuthenticationStatus.Success:
                    var responseData = webAuthenticationResult.ResponseData;
                    var responseUri = new Uri(responseData);
                    var decoder = new WwwFormUrlDecoder(responseUri.Fragment.Replace("#", "?"));

                    _accessToken = decoder.GetFirstValueByName("access_token");
                    _instanceUrl = WebUtility.UrlDecode(decoder.GetFirstValueByName("instance_url"));

                    return;

                case Windows.Security.Authentication.Web.WebAuthenticationStatus.ErrorHttp:
                    throw new Exception(webAuthenticationResult.ResponseErrorDetail.ToString());

                default:
                    throw new Exception(webAuthenticationResult.ResponseData);
            }
        }

        public class Account
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
        }
                    

    }
}
