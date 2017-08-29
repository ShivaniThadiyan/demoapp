using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Contoso.Sales.API.Models
{
    public class SalesUser
    {
        [BsonId(IdGenerator = typeof(CombGuidGenerator))]
        public Guid Id { get; set; }

        [BsonElement("FirstName")]
        public string FirstName { get; set; }

        [BsonElement("LastName")]
        public string LastName { get; set; }

        [BsonElement("Email")]
        public string Email { get; set; }

        //[BsonElement("Password")]
        //public string Password { get; set; }

        [BsonElement("Quota")]
        public string Quota { get; set; }

        [BsonElement("Roles")]
        public string Roles { get; set; }



        [Required]
        [Display(Name = "User name")]
        [BsonElement("UserName")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [BsonElement("Password")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [BsonElement("ConfirmPassword")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }


        private bool disposed = false;

        // To do: update the connection string with the DNS name
        // or IP address of your server. 
        //For example, "mongodb://testlinux.cloudapp.net"
        private string connectionString = "mongodb://contososales-id:uOYTywNlAwpNLIUKu2vGlhypPLKwMdD8ycEQ5iRkkhpqi6PKolX5R2PxScaH8hUOnW02IWP0Sjt7Ru7P4WfU0w==@contososales-id.documents.azure.com:10250/?ssl=true";

        // This sample uses a database named "Tasks" and a 
        //collection named "TasksList".  The database and collection 
        //will be automatically created if they don't already exist.
        private string dbName = "salesdb";
        private string collectionName = "SalesUser";


        public async Task<SalesUser> CreateUserAsync(string user,string password)
        {


            SalesUser obj = new SalesUser();
            obj.UserName = user;
            obj.Password = password;
            //await obj.UserName = user;


        var collection =  GetTasksCollectionForEdit();
            try
            {
                collection.InsertOne(obj);
            }
            catch (MongoCommandException ex)
            {
                string msg = ex.Message;
            }


            return obj;
        }

        private IMongoCollection<SalesUser> GetTasksCollectionForEdit()
        {
            MongoClient client = new MongoClient(connectionString);
            var database = client.GetDatabase(dbName);
            var todoTaskCollection = database.GetCollection<SalesUser>(collectionName);
            return todoTaskCollection;
        }

    }
}