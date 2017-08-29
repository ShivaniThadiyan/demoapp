using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contoso.Sales.Entities
{
    public static class HttpMethods
    {
        public static string POST = "POST";
        public static string GET = "GET";
        public static string DELETE = "DELETE";
        public static string PUT = "PUT";

    }
    public static class OperationStatus
    {
        public static string SUCCESS = "Success";
        public static string ERROR = "Error";
        public static string INFO = "Info";
    }
    public static class RestAPI
    {
        private static string URL_BASE = "http://contososales.azurewebsites.net/";
        public static string URL_GET_PROFILE = URL_BASE + "api/Profile";
    }
    public static class AppConstants
    {
        public static string ID_TOKEN = string.Empty;
        public static string ACCESS_TOKEN = string.Empty;
        public static string USER_NAME = string.Empty;
        public static string FAMILYNAME = string.Empty;
        public static string GIVENNAME = string.Empty;
        public enum LogType
        {
            INFO,
            ERROR,
            DEBUG
        }
        #region Push Notification Constants
        public const string GOOGLE_API_PROJECT_NUMBER = "742758901587";
        public const string AZURE_NOTIFICATION_LISTEN_CONNECTION_STRING = @"Endpoint=sb://contososaleshub.servicebus.windows.net/;SharedAccessKeyName=DefaultListenSharedAccessSignature;SharedAccessKey=jsPEA5EX9vlLRDOTtwEtmzG6sIW3CRIkIhC095/a0XQ=";
        public const string AZURE_NOTIFICATION_HUB_NAME = @"contososales-hub";
        #endregion

        #region Registration Tags
        public static string TAG_USER_NAME = "UserName:";
        public static string TAG_MEMBERSHIP_NUMBER = "MembershipNumber:";
        #endregion

        #region authentication constants for Azure Active Directory
        public static string clientId = "4ffcab7c-0096-436d-8fbc-518ce19cf539";
        public static string commonAuthority = "https://login.windows.net/Common";
        
        public static string returnUri = "http://m.contososales.azurewebsites.net";
        public static string graphResourceUri = "https://graph.windows.net";
        public static string graphApiVersion = "2013-11-08";
        #endregion

        public static List<string> TAGS = new List<string>();
       
    }
}
