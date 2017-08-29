using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using Contoso.Sales.Hub.Transport;
using Contoso.Sales.Web.Models;
using Contoso.Sales.Web.DAL;



namespace Contoso.Sales.Web.Controllers
{


    [RoutePrefix("api/orders")]
    public class OrdersController : ApiController
    {
        private IUserRepository _userRepository;
        public OrdersController()
        {
            this._userRepository = new UserRepository(new Entities());
        }

        /// <summary>
        /// Get user details based on Emailid , Creates user if not exists.
        /// </summary>
        /// <returns>User details</returns>
        //[Authorize]
        public IHttpActionResult Get()
        {

            ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
            var components = ClaimsPrincipal.Current.Identity.Name.Split('#');
            var Name = components.Last();
            User user = new User();

            // Check for user existance and create if new user
            var response = _userRepository.GetUserByEmail(Name);
            if (response == null)
            {
                user.FirstName = Name;
                user.LastName = Name;
                user.Email = Name;
                user.Password = "123456";
                user.Quota = "500";
                _userRepository.InsertUser(user);
                _userRepository.Save();
            }
            return Ok(_userRepository.GetUser());
        }

        /// <summary>
        /// update Quota Value based on Emailid and send push notification to mobile app
        /// </summary>
        /// <param name="user"></param>
        /// <returns>User details after Updating quota</returns>
        //[Authorize]
        [HttpPost]
        public IHttpActionResult Post(User user)
        {
            _userRepository.UpdateQuotaByEmail(user.Email, user.Quota);
            Notifier.EnqueueNotification(user.Email, "Target Updated", string.Format("Your Target has been updated to {0}", user.Quota));
            List<User> _list = new List<User>();
            user = _userRepository.GetUserByEmail(user.Email);
            _list.Add(user);
            return Ok(_list);
        }
    }
}
