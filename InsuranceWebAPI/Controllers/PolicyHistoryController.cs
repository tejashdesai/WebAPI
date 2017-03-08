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
        [Route("currentpolicy")]
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

        [Authorize]
        [Route("dashboard")]
        [HttpGet]
        public HttpResponseMessage getDashboardData()
        {
            try
            {
                var policyHistory = _policyHistoryServices.GetCurrentPolicy(false, true);
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

        [Authorize]
        [Route("deletehistory/{id}")]
        [HttpGet]
        public HttpResponseMessage DeletePolicyHistory(int id)
        {
            try
            {
                var policyHistory = _policyHistoryServices.DeletePolicyHistory(id);
                if (policyHistory)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Policy history deleted successfully.");
                }
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error while delete policy history.");
            }
            catch (System.Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                    ex.Message);
            }
        }
    }
}