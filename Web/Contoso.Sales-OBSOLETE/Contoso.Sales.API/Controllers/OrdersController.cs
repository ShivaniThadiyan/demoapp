using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

using System.Web;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson;
using MongoDB;
using System.Threading.Tasks;
using MongoDB.Driver;
using System.Configuration;
using MongoDB.Driver.Linq;
using System.ComponentModel.DataAnnotations;
using Newtonsoft;
using Newtonsoft.Json;
using Contoso.Sales.Hub.Transport;

namespace Contoso.Sales.API.Controllers
{
    [RoutePrefix("api/orders")]
    public class OrdersController : ApiController
    {
        [Authorize]        
        public IHttpActionResult Get()
        {
            //ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;

            //var Name = ClaimsPrincipal.Current.Identity.Name;
            //var Name1 = User.Identity.Name;

            //var userName = principal.Claims.Where(c => c.Type == "sub").Single().Value;
            order obj = new order();

            return Ok(obj.Createorders());
        }

        [Authorize]
        //[Route("")]
        [HttpPost]
        public IHttpActionResult Post(order order)
        {
            var listAfterUpdate = order.Updateorders(order.Email, order.Quota);
            Notifier.EnqueueNotification(string.Empty, "Quota Updated", string.Format("Your quota has been updated to {0}", order.Quota));
            return Ok(listAfterUpdate);
        }


    }


    #region Helpers

    public class order
    {
        //public int orderID { get; set; }
        //public string CustomerName { get; set; }
        //public string ShipperCity { get; set; }
        //public Boolean IsShipped { get; set; }


        [BsonId(IdGenerator = typeof(CombGuidGenerator))]
        public Guid Id { get; set; }

        [BsonElement("FirstName")]
        public string FirstName { get; set; }

        [BsonElement("LastName")]
        public string LastName { get; set; }

        [BsonElement("Email")]
        public string Email { get; set; }

        [BsonElement("Password")]
        public string Password { get; set; }

        [BsonElement("Quota")]
        public string Quota { get; set; }

        [BsonElement("Roles")]
        public string Roles { get; set; }

        // To do: update the connection string with the DNS name
        // or IP address of your server. 
        //For example, "mongodb://testlinux.cloudapp.net"
        private string connectionString = "mongodb://contososales-id:uOYTywNlAwpNLIUKu2vGlhypPLKwMdD8ycEQ5iRkkhpqi6PKolX5R2PxScaH8hUOnW02IWP0Sjt7Ru7P4WfU0w==@contososales-id.documents.azure.com:10250/?ssl=true";

        // This sample uses a database named "Tasks" and a 
        //collection named "TasksList".  The database and collection 
        //will be automatically created if they don't already exist.
        private string dbName = "salesdb";
        private string collectionName = "User";


        public  List<order> Createorders()
        {
            var collection = GetTasksCollection();
            return collection.Find(new BsonDocument()).ToList();
            
            //return orderList;
        }

        private  IMongoCollection<order> GetTasksCollection()
        {
            MongoClient client = new MongoClient(connectionString);
            var database = client.GetDatabase(dbName);
            var todoTaskCollection = database.GetCollection<order>(collectionName);
            return todoTaskCollection;
        }


        public List<order> Updateorders(string email, string quota)
        {

            MongoClient client = new MongoClient(connectionString);
            var database = client.GetDatabase(dbName);

            var collection = database.GetCollection<BsonDocument>("User");
            IMongoCollection<order> usercollection = database.GetCollection<order>("User");

            var updoneresult = usercollection.UpdateOne(
                                 Builders<order>.Filter.Eq("Email", email),
                                 Builders<order>.Update.Set("Quota", quota));


           return usercollection.Find(new BsonDocument()).ToList();

        }

        public int ChackUserexistance(string Email)
        {
            MongoClient client = new MongoClient(connectionString);
            var database = client.GetDatabase(dbName);

            var collection = database.GetCollection<BsonDocument>("User");
            IMongoCollection<order> usercollection = database.GetCollection<order>("User");
            //var filter = Builders<BsonDocument>.Filter.Eq("Email", Email);
            //var result = collection.Find(filter).ToListAsync();
            var result = usercollection.AsQueryable<order>().Where<order>(sb => sb.Email == Email).SingleOrDefault();
            if (result==null)
                return 0;
            else
                return 1;

        }
    }

    #endregion
}
