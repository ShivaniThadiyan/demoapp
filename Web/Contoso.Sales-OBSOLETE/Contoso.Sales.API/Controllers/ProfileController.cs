using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Configuration;
using MongoDB.Driver.Linq;
using Contoso.Sales.API.Controllers;
using System.Text.RegularExpressions;

namespace Contoso.Sales.API.Controllers
{
    public class ProfileController : ApiController
    {

        // To do: update the connection string with the DNS name
        // or IP address of your server. 
        //For example, "mongodb://testlinux.cloudapp.net"
        private string connectionString = "mongodb://contososales-id:uOYTywNlAwpNLIUKu2vGlhypPLKwMdD8ycEQ5iRkkhpqi6PKolX5R2PxScaH8hUOnW02IWP0Sjt7Ru7P4WfU0w==@contososales-id.documents.azure.com:10250/?ssl=true";

        // This sample uses a database named "Tasks" and a 
        //collection named "TasksList".  The database and collection 
        //will be automatically created if they don't already exist.
        private string dbName = "salesdb";
        private string collectionName = "User";

        [HttpGet]
        [Authorize]
        public IHttpActionResult Get()
        {
            ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
            var name = ClaimsPrincipal.Current.Identity.Name;
            // Get the record from db where name = Email


            return Ok(ChackUserexistance(name));
        }



        public List<order> ChackUserexistance(string Email)
        {
            List<order> _list = new List<order>();
            MongoClient client = new MongoClient(connectionString);
            var database = client.GetDatabase(dbName);

            var collection = database.GetCollection<BsonDocument>("User");
            IMongoCollection<order> usercollection = database.GetCollection<order>("User");
            //var filter = Builders<BsonDocument>.Filter.Eq("Email", Email);
            //var result = collection.Find(filter).ToListAsync();
            var result = usercollection.AsQueryable<order>().Where<order>(sb => sb.Email == Email).SingleOrDefault();
            _list.Add(result);
            return _list;

        }
    }
}
