using InsuranceWebAPI.BusinessLayer.Interface;
using InsuranceWebAPI.BusinessLayer.Service;
using InsuranceWebAPI.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace InsuranceWebAPI.Controllers
{
    public class UserController : ApiController
    {
        private readonly IUserService _userServices;
        private readonly INotificationService _notificationServices;

        public UserController()
        {
            _userServices = new UserService();
            _notificationServices = new NotificationService();
        }

        [AllowAnonymous]
        [Route("Register")]
        public IHttpActionResult Register(UserDTO userModel)
        {
            if (userModel == null || string.IsNullOrEmpty(userModel.UserName) || string.IsNullOrEmpty(userModel.Password))
            {
                return BadRequest("Please provide username and password");
            }

            int result = _userServices.CreateUser(userModel);
            if (result > 0)
            {
                return Ok();
            }
            return InternalServerError();
        }

        [Authorize]
        [Route("notification/{id}")]
        public HttpResponseMessage SendNotification(int id)
        {
            if (_notificationServices.SendNotification(id))
                return Request.CreateResponse(HttpStatusCode.OK, true);
            return Request.CreateResponse(HttpStatusCode.BadRequest, false);
        }

    }
}
