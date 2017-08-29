using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
//using Contoso.Sales.Web.Models;
namespace Contoso.Sales.Web.DAL
{
    public class UserContext : DbContext
    {
        public UserContext()
            : base("name=ContosoConnectionString")
        {
        }
        //public DbSet<User> Users { get; set; }
    }
}