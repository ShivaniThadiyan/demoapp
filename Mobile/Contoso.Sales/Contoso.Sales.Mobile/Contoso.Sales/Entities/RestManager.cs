using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Diagnostics;
using System.Threading;
using Newtonsoft.Json;

namespace Contoso.Sales.Entities
{
    public static class RestManager
    {
        public async static Task<string> CallRestService(string url, string method, Dictionary<string, object> parameters, Dictionary<string, object> headers = null, string contentType = "application/json")
        {
            try
            {

                // Create an HTTP web request using the URL:
                string formattedParams = string.Empty;

                if ((string.Equals(HttpMethods.GET.ToString(), method) || string.Equals(HttpMethods.DELETE.ToString(), method)) && parameters != null && parameters.Count() > 0)
                {
                    formattedParams = string.Join("&", parameters.Select(x => x.Key + "=" + System.Net.WebUtility.UrlEncode(x.Value.ToString())));
                    url = string.Format("{0}?{1}", url, formattedParams);
                }
                else if ((string.Equals(HttpMethods.POST.ToString(), method) || string.Equals(HttpMethods.PUT.ToString(), method)) && parameters != null && parameters.Count() > 0)
                {
                    if (parameters != null && parameters.Count() > 0)
                    {
                        if ("application/json".Equals(contentType))
                        {
                            formattedParams = JsonConvert.SerializeObject(parameters.FirstOrDefault().Value);
                        }
                        else
                        {
                            formattedParams = string.Join("&", parameters.Select(x => x.Key + "=" + x.Value));
                        }
                    }
                }

                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));

                request.Accept = "application/json";

                if (headers == null)
                {
                    headers = new Dictionary<string, object>();
                }
                if (!string.IsNullOrEmpty(AppConstants.ID_TOKEN))
                {
                    headers.Add("Authorization", "Bearer " + AppConstants.ID_TOKEN);
                }
                if (headers != null)
                {
                    foreach (KeyValuePair<string, object> kvp in headers)
                    {
                        request.Headers[kvp.Key] = kvp.Value.ToString();
                    }
                }
                request.Method = method;
                //Debug.WriteLine("Url = "+url+" formattedParams =" + formattedParams);
                if (string.Equals(HttpMethods.POST.ToString(), method) || string.Equals(HttpMethods.PUT.ToString(), method))
                {
                    if (parameters.Count > 0)
                    {
                        request.ContentType = contentType;
                        Stream stream = await request.GetRequestStreamAsync();

                        using (StreamWriter writer = new StreamWriter(stream))
                        {
                            writer.Write(formattedParams);
                            writer.Flush();
                            //writer.Dispose ();
                        }
                    }
                }

                WebResponse response = await Task.Factory.FromAsync<WebResponse>(request.BeginGetResponse, request.EndGetResponse, request);
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    string res = reader.ReadToEnd();
                    return res;
                }
            }
            catch (WebException ex)
            {
                return string.Empty;
            }
        }

        private static async Task<string> GetResponseString(HttpWebRequest request)
        {
            WebResponse response =
                await
                Task.Factory.FromAsync<WebResponse>(request.BeginGetResponse, request.EndGetResponse,
                    request);
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                return reader.ReadToEnd();
            }
        }
    }
}



