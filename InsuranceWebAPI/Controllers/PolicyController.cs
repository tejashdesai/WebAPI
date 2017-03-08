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
    public class PolicyController : ApiController
    {
        private readonly IPolicyService _policyServices;
        private readonly IPolicyHistoryService _policyHistoryServices;

        public PolicyController()
        {
            _policyServices = new PolicyService();
            _policyHistoryServices = new PolicyHistoryService();
        }

        [Authorize]
        public HttpResponseMessage Get()
        {
            var policies = _policyServices.GetAllPolicy();
            if (policies != null)
            {
                var policyEntities = policies as List<PolicyDTO> ?? policies.ToList();
                if (policyEntities.Any())
                    return Request.CreateResponse(HttpStatusCode.OK, policyEntities);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Policies not found");
        }

        [Authorize]
        [Route("policy/{id}")]
        public HttpResponseMessage Get(int id)
        {
            var policy = _policyServices.GetPolicyById(id);
            if (policy != null)
                return Request.CreateResponse(HttpStatusCode.OK, policy);
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No policy found for this id");
        }

        [Authorize]
        [Route("savepolicy")]
        [HttpPost]
        public HttpResponseMessage SavePolicy([FromBody] NewPolicy newPolicy)
        {
            var policyEntity = new PolicyDTO
            {
                AdditionalName1 = newPolicy.AdditionalName1,
                AdditionalName2 = newPolicy.AdditionalName2,
                AdditionalName3 = newPolicy.AdditionalName3,
                Address1 = newPolicy.Address1,
                Address2 = newPolicy.Address2,
                City = newPolicy.City,
                CreatedDate = DateTime.Now,
                Email = newPolicy.Email,
                Mobile = newPolicy.Mobile,
                Mobile1 = newPolicy.Mobile1,
                Name = newPolicy.Name,
                PolicyType = newPolicy.PolicyType
            };
            var policyRes = 0;
            if (newPolicy.PolicyID > 0)
            {
                var res = _policyServices.UpdatePolicy(newPolicy.PolicyID, policyEntity);
                if (res)
                {
                    policyRes = newPolicy.PolicyID;
                }
            }
            else
            {
                policyRes = _policyServices.CreatePolicy(policyEntity);
            }

            if (policyRes > 0)
            {
                var policyHistory = new PolicyHistoryDTO
                {
                    EndDate = newPolicy.EndDate,
                    IsCurrent = true,
                    PolicyAmount = newPolicy.PolicyAmount,
                    PolicyID = policyRes,
                    PolicyNumber = newPolicy.PolicyNumber,
                    StartDate = newPolicy.StartDate
                };
                var policyHistoryRes = 0;
                if (newPolicy.PolicyHistoryID > 0)
                {
                    policyHistoryRes = _policyHistoryServices.CreatePolicyHistory(policyHistory);
                }
                else
                {
                    var res = _policyHistoryServices.UpdatePolicyHistory(newPolicy.PolicyHistoryID, policyHistory);
                    if (res)
                    {
                        policyHistoryRes = newPolicy.PolicyHistoryID;
                    }
                }

                if (policyRes > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, policyRes);
                }
                else
                {
                    _policyServices.DeletePolicy(policyRes);
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error while saving policy history.");
                }
            }
            return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error while saving policy.");

        }

        [Authorize]
        [Route("deletepolicy/{id}")]
        [HttpGet]
        public HttpResponseMessage DeletePolicy(int id)
        {
            try
            {
                var policy = _policyServices.DeletePolicy(id);
                if (policy)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Policy deleted successfully.");
                }
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error while delete policy.");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                    ex.Message);
            }
        }
    }
}