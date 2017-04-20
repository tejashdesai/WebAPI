using InsuranceWebAPI.BusinessLayer.Interface;
using InsuranceWebAPI.BusinessLayer.Service;
using InsuranceWebAPI.Entity;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace InsuranceWebAPI.Controllers
{
    public class UserController : ApiController
    {
        private readonly IUserService _userServices;
        private readonly ISettingsService _settingsServices;
        private readonly INotificationService _notificationServices;

        public UserController()
        {
            _userServices = new UserService();
            _settingsServices = new SettingsService();
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

        [Authorize]
        [Route("updatesettings")]
        [HttpPost]
        public HttpResponseMessage UpdateSettings([FromBody]SettingsDTO settingsEntity)
        {
            if (_settingsServices.UpdateSettings(settingsEntity))
                return Request.CreateResponse(HttpStatusCode.OK, true);
            return Request.CreateResponse(HttpStatusCode.InternalServerError, false);
        }
    }
}
