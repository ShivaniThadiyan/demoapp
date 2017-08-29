using Microsoft.Azure.NotificationHubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contoso.Sales.Hub.Transport
{
    public class Notifier
    {
        const string NOTIFICATION_HUB_CONNECTION_STRING = "Endpoint=sb://contososaleshub.servicebus.windows.net/;SharedAccessKeyName=DefaultFullSharedAccessSignature;SharedAccessKey=cf8fiFMBlb5vbsmV0Y91GO+nZk/4CHN+kyMNYO86fp8=";
        const string NOTIFICATION_HUB_NAME = "contososales-hub";
        public static void EnqueueNotification(string email, string title, string message)
        {
            var hubClient = NotificationHubClient.CreateClientFromConnectionString(NOTIFICATION_HUB_CONNECTION_STRING, NOTIFICATION_HUB_NAME);
            List<string> tags = new List<string>();
            tags.Add(email);

            var pushMessage = string.Format("{{ \"data\":{{ \"Title\":\"{0}\",\"Message\":\"{1}\"}} }}", title, message);
            var task = hubClient.SendGcmNativeNotificationAsync(pushMessage, tags);

            task.ContinueWith(x =>
            {
                if (!x.IsFaulted)
                {
                    // Log some meaningful information here.
                    Console.WriteLine("Notification sent successfully");
                }
                else
                {
                    Console.WriteLine(x.Exception.Message);
                }
            });
        }
    }
}
