using Contoso.Sales.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Contoso.Sales.Helpers
{
    public class UserHelper
    {
        public async Task<List<Profile>> GetProfile(string userName)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("id", userName);
            string responseAsString = await RestManager.CallRestService(RestAPI.URL_GET_PROFILE, HttpMethods.GET, param, null);
            var jsonSerializer = new JsonSerializer();
            List<Profile> profiles = JsonConvert.DeserializeObject<List<Profile>>(responseAsString.ToString());
            return profiles;

        }
        public async Task<List<Profile>> UpdateProfile(string userName,string actualValue)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("profile", new { email=userName,actual=actualValue});
            string responseAsString = await RestManager.CallRestService(RestAPI.URL_GET_PROFILE, HttpMethods.POST, param, null);
            var jsonSerializer = new JsonSerializer();
            List<Profile> profiles = JsonConvert.DeserializeObject<List<Profile>>(responseAsString.ToString());
            return profiles;

        }
        //public async Task<List<Profile>> CheckUserQuota()
        //{
        //    string responseAsString = await RestManager.CallRestService(RestAPI.URL_CHECKQUOTA, HttpMethods.GET, null);

        //    //var client = new HttpClient();
        //    //var request = new HttpRequestMessage(HttpMethod.Get, RestAPI.URL_CHECKQUOTA);
        //    //request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", AppConstants.ACCESS_TOKEN);
        //    //var response = await client.SendAsync(request);

        //    //var content = await response.Content.ReadAsStringAsync();

        //    List<Profile> profiles = null;
        //    if (!string.IsNullOrEmpty(responseAsString))
        //    {
        //        profiles = JsonConvert.DeserializeObject<List<Profile>>(responseAsString.ToString());
        //    }
        //    else
        //    {
        //        profiles = new List<Profile>();
        //    }
        //    return profiles;
        //}
    }
}
