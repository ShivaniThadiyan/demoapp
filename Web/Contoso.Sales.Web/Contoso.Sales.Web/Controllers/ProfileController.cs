using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Text.RegularExpressions;
using Contoso.Sales.Web.Models;
using Contoso.Sales.Web.DAL;

namespace Contoso.Sales.Web.Controllers
{
    public class ProfileController : ApiController
    {

        private IUserRepository _userRepository;
        public ProfileController()
        {
            this._userRepository = new UserRepository(new Entities());
        }

        /// <summary>
        /// Used in mobuile app to get user details based on Emailid , Creates user if not exists.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>User details</returns>
        [HttpGet]
        //[Authorize]
        public IHttpActionResult Get(string id)
        {
            User user = new User();
            var response = _userRepository.GetUserByEmail(id);
            if (response == null)
            {
                user.FirstName = id;
                user.LastName = id;
                user.Email = id;
                user.Password = "123456";
                user.Quota = "500";

                _userRepository.InsertUser(user);
                _userRepository.Save();
            }
            user = _userRepository.GetUserByEmail(id);

            List<User> _list = new List<User>();
            _list.Add(user);

            return Ok(_list);
        }

        /// <summary>
        /// Used in mobuile app to update Actual Value based on Emailid
        /// </summary>
        /// <param name="model"></param>
        /// <returns>User details after Updating Actual</returns>
        //[Authorize]
        [HttpPost]
        public IHttpActionResult Post([FromBody]QuotaModel model)
        {
            User user = new User();
            user = _userRepository.UpdateActualByEmail(model.Email, model.Actual);

            List<User> _list = new List<User>();
            _list.Add(user);
            return Ok(_list);
        }
    }
}
