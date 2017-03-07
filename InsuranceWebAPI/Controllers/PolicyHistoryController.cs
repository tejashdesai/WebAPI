using InsuranceWebAPI.BusinessLayer.Interface;
using InsuranceWebAPI.BusinessLayer.Service;
using InsuranceWebAPI.Entity;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//using System.Web.Mvc;

namespace InsuranceWebAPI.Controllers
{

    public class PolicyHistoryController : ApiController
    {
        private readonly IPolicyHistoryService _policyHistoryServices;

        public PolicyHistoryController()
        {
            _policyHistoryServices = new PolicyHistoryService();
        }

        [Authorize]
        [Route("CurrentPolicy")]
        [HttpGet]
        public HttpResponseMessage getCurrentPolicies()
        {
            try
            {
                var policyHistory = _policyHistoryServices.GetCurrentPolicy();
                if (policyHistory != null)
                {
                    //var policyHistoryEntities = policyHistory as List<CurrentPolicyModel> ?? policyHistory.ToList();
                    //if (policyHistoryEntities.Any())
                    return Request.CreateResponse(HttpStatusCode.OK, policyHistory);
                }
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No current policy found");
            }
            catch (System.Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError,
                    ex.Message);
            }
        }
    }
}