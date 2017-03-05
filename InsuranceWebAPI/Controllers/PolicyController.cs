using InsuranceWebAPI.BusinessLayer.Interface;
using InsuranceWebAPI.BusinessLayer.Service;
using InsuranceWebAPI.Entity;
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

        public PolicyController()
        {
            _policyServices = new PolicyService();
        }

        // GET api/policy
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

        // GET api/policy/5
        public HttpResponseMessage Get(int id)
        {
            var policy = _policyServices.GetPolicyById(id);
            if (policy != null)
                return Request.CreateResponse(HttpStatusCode.OK, policy);
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No policy found for this id");
        }

        public int Post([FromBody] PolicyDTO policyEntity)
        {
            return _policyServices.CreatePolicy(policyEntity);
        }
    }
}