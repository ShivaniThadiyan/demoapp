using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Gcm.Client;
using Android.Util;
using Android.Support.V4.Content;
using Android.Media;
using Android.Graphics;
using WindowsAzure.Messaging;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Contoso.Sales.Entities;

namespace Contoso.Sales.Droid.Notifications
{
    /// <summary>
    /// Registering service to receive push notifications on background
    /// </summary>
    /// <seealso cref="Gcm.Client.GcmServiceBase" />
    [Service]
    public class GcmService : GcmServiceBase
    {
        public static string RegistrationID { get; private set; }
        private NotificationHub Hub { get; set; }

        public void RemoveNotification(int id)
        {
            // Get the notification manager:
            NotificationManager notificationManager = GetSystemService(Context.NotificationService) as NotificationManager;
            notificationManager.Cancel(id);
        }

        public GcmService()
            : base(GcmBroadcastReceiver.SENDER_IDS)
        {
        }

        protected override void OnRegistered(Context context, string registrationId)
        {
            RegistrationID = registrationId;
            Hub = new NotificationHub(Contoso.Sales.Entities.AppConstants.AZURE_NOTIFICATION_HUB_NAME, Contoso.Sales.Entities.AppConstants.AZURE_NOTIFICATION_LISTEN_CONNECTION_STRING, context);
            try
            {
                Hub.UnregisterAll(registrationId);
            }
            catch (Exception ex)
            {
                //TODO: Log Error
            }
            try
            {
                Hub.Register(registrationId, AppConstants.TAGS.ToArray());
                //var hubRegistration = Hub.Register(registrationId, tags.ToArray());
            }
            catch (Exception ex)
            {
                //TODO: Log Error
            }
        }

        protected override void OnMessage(Context context, Intent intent)
        {
            if (intent != null && intent.Extras != null)
            {
                string Title = intent.Extras.GetString("Title");
                string Message = intent.Extras.GetString("Message");
              
                if (Title != null&& Message != null)
                {
                    createNotification(Title,Message);
                }
            }
        }

        protected override void OnUnRegistered(Context context, string registrationId)
        {
            createNotification("GCM Unregistered...", "The device has been unregistered!");
        }

        protected override bool OnRecoverableError(Context context, string errorId)
        {
            return base.OnRecoverableError(context, errorId);
        }

        protected override void OnError(Context context, string errorId)
        {
            //TODO: Log Error
        }
        private void createNotification(string title, string desc)
        {
            Notification.BigTextStyle textStyle = new Notification.BigTextStyle();
            textStyle.BigText(desc);
            // Set up an intent so that tapping the notifications returns to this app:
            Intent intent = new Intent(this, typeof(MainActivity));

            // Create a PendingIntent; we're only using one PendingIntent (ID = 0):
            const int pendingIntentId = 0;
            PendingIntent pendingIntent =
                PendingIntent.GetActivity(this, pendingIntentId, intent, PendingIntentFlags.OneShot);

            Notification.Builder builder = new Notification.Builder(this)
            .SetContentIntent(pendingIntent)
            .SetContentTitle(title)
            .SetContentText(desc)
            //.SetStyle(textStyle)
            .SetSmallIcon(Resource.Drawable.icon);

            // Build the notification:
            Notification notification = builder.Build();

            // Get the notification manager:
            NotificationManager notificationManager =
                GetSystemService(Context.NotificationService) as NotificationManager;

            // Publish the notification:
            notificationManager.Notify((int)DateTime.Now.Ticks, notification);

        }
        
    }
}