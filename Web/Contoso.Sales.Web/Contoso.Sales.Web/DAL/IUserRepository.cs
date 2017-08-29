using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contoso.Sales.Web.Models;

namespace Contoso.Sales.Web.DAL
{
    interface IUserRepository : IDisposable
    { 
        void InsertUser(User user);
        void UpdateUser(User user);
        void Save();
       List<User> GetUser();
        User GetUserByEmail(string Email);
        User UpdateQuotaByEmail(string Email,string Quota);
        User UpdateActualByEmail(string Email, string Actual);


    }
}
