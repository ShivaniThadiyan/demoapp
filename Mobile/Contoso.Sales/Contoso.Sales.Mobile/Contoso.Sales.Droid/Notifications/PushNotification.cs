using System;
using System.Collections.Generic;
using Gcm.Client;
using Xamarin.Forms;
using Contoso.Sales.Droid.Notifications;
using Contoso.Sales.Interfaces;
using Contoso.Sales.Droid;
using Plugin.CurrentActivity;
using Contoso.Sales.Entities;

[assembly: Dependency (typeof (PushNotification))]
namespace Contoso.Sales.Droid.Notifications
{
	public class PushNotification: IPushNotification
	{
		#region IPushNotification implementation
		public void Register (string userId)
		{
			try
			{
                AppConstants.TAGS.Clear();
                AppConstants.TAGS.Add(userId);
                // Check to ensure everything's set up right
                GcmClient.CheckDevice(CrossCurrentActivity.Current.Activity);
                GcmClient.CheckManifest(CrossCurrentActivity.Current.Activity);

                // Register for push notifications                
                GcmClient.Register(CrossCurrentActivity.Current.Activity, Contoso.Sales.Entities.AppConstants.GOOGLE_API_PROJECT_NUMBER);
            }
			catch (Java.Net.MalformedURLException)
			{
				Console.WriteLine ("There was an error creating the Mobile Service. Verify the URL");
			}
			catch (Exception e)
			{
				//Console.WriteLine ("Error: "+e.InnerException.Message.ToString());
			}
		}
		#endregion		
	}
}

