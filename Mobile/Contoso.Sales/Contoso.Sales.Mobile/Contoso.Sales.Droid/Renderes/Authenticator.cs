using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Contoso.Sales.Interfaces;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Xamarin.Forms;
using Plugin.CurrentActivity;

[assembly: Dependency(typeof(Contoso.Sales.Droid.Renderes.Authenticator))]
namespace Contoso.Sales.Droid.Renderes
{
    class Authenticator : IAuthenticator
    {
        public async Task<AuthenticationResult> Authenticate(string authority, string resource, string clientId, string returnUri)
        {
            try
            {
                //var authContext = new AuthenticationContext(authority);
                //if (authContext.TokenCache.ReadItems().Any())
                //    authContext = new AuthenticationContext(authContext.TokenCache.ReadItems().First().Authority);

                //var uri = new Uri(returnUri);
                //var platformParams = new PlatformParameters(CrossCurrentActivity.Current.Activity);
                //var authResult = await authContext.AcquireTokenAsync(resource, clientId, uri, platformParams);
                //return authResult;
                var authContext = new AuthenticationContext(authority);
                if (authContext.TokenCache.ReadItems().Any())
                    authContext = new AuthenticationContext(authContext.TokenCache.ReadItems().First().Authority);
                var authResult = await authContext.AcquireTokenAsync(resource, clientId, new Uri(returnUri), new PlatformParameters((Activity)Forms.Context));
                return authResult;
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }
    }
}