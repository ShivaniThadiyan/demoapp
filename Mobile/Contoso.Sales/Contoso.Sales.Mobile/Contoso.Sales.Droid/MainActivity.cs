using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Gcm.Client;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Android.Content;

namespace Contoso.Sales.Droid
{
    [Activity(Label = "Contoso Sales", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
      
        protected override void OnCreate(Bundle bundle)
        {

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            var pixels = Resources.DisplayMetrics.WidthPixels; // real pixels
            var scale = Resources.DisplayMetrics.Density;
            int dps = (int)((pixels - 0.5f) / scale);

            App.ScreenWidth = dps;
            ActionBar.SetIcon(Android.Resource.Color.Transparent);
            pixels = Resources.DisplayMetrics.HeightPixels; // real pixels
            scale = Resources.DisplayMetrics.Density;
            dps = (int)((pixels - 0.5f) / scale);

            App.ScreenHeight = dps; // real pixels
            LoadApplication(new App());
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            AuthenticationAgentContinuationHelper.SetAuthenticationAgentContinuationEventArgs(requestCode, resultCode, data);
        }

    }
}

