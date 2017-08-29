using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Contoso.Sales.Web.Models;
using System.Data;
using System.Data.Entity;

namespace Contoso.Sales.Web.DAL
{
    public class UserRepository : IUserRepository
    {

        private Entities _context;

        public UserRepository(Entities UserContext)
        {
            this._context = UserContext;
        }

        public void InsertUser(User User)
        {
            _context.Users.Add(User);
        }

        public void UpdateUser(User User)
        {
            _context.Entry(User).State = EntityState.Modified;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public List<User> GetUser()
        {

            var user = (from u in _context.Users select u);

            return user.ToList();
        }

        //Get user details form User table based on Emialid
        public User GetUserByEmail(string Email)
        {

            var user = (from u in _context.Users
                        where u.Email == Email
                        select u).FirstOrDefault();

            return user;

        }

        //Update Quote field in user table based on Emailid
        public User UpdateQuotaByEmail(string Email, string Quota)
        {
            var user = _context.Users.Where(a => a.Email == Email).FirstOrDefault();

            if (user != null)
            {
                user.Quota = Quota;

                _context.SaveChanges();
            }
            return user;
        }


        public User UpdateActualByEmail(string Email, string Actual)
        {
            var user = _context.Users.Where(a => a.Email == Email).FirstOrDefault();

            if (user != null)
            {
                user.Actual = Actual;

                _context.SaveChanges();
            }
            return user;
        }


        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

