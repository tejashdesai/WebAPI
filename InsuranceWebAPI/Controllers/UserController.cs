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

        public UserController()
        {
            _userServices = new UserService();
        }

        [AllowAnonymous]
        [Route("Register")]
        public IHttpActionResult Register(UserDTO userModel)
        {
            if (string.IsNullOrEmpty(userModel.UserName) || string.IsNullOrEmpty(userModel.Password))
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
    }
}
