using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using InsuranceWebAPI.DataLayer;
using InsuranceWebAPI.BusinessLayer.Interface;
using InsuranceWebAPI.BusinessLayer.Service;
using System.Net.Http;
using InsuranceWebAPI.Entity;
using System.Collections.Generic;
using System;

namespace InsuranceWebAPI.Controllers
{
    public class PolicyTypeController : ApiController
    {
        private readonly IPolicyTypeService _policyTypeServices;

        public PolicyTypeController()
        {
            _policyTypeServices = new PolicyTypeService();
        }

        [Authorize]
        [Route("policytypes")]
        [HttpGet]
        public HttpResponseMessage GetPolicyTypes()
        {
            var policyTypes = _policyTypeServices.GetAllPolicyType();
            if (policyTypes != null)
            {
                var policyTypeEntities = policyTypes as List<PolicyTypeDTO> ?? policyTypes.ToList();
                if (policyTypeEntities.Any())
                    return Request.CreateResponse(HttpStatusCode.OK, policyTypeEntities);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Policy Type not found");
        }

        [Authorize]
        [Route("policytype/{id}")]
        [HttpGet]
        public HttpResponseMessage GetPolicyType(int id)
        {
            var policyType = _policyTypeServices.GetPolicyTypeById(id);
            if (policyType != null)
                return Request.CreateResponse(HttpStatusCode.OK, policyType);
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No policy type found for this id");
        }

        [Authorize]
        [Route("savepolicytype")]
        [HttpPost]
        public HttpResponseMessage SavePolicyType([FromBody]PolicyTypeDTO policyType)
        {
            var policyTypeEntity = new PolicyTypeDTO
            {
                PolicyTypeName = policyType.PolicyTypeName
            };

            if (policyType.PolicyTypeID > 0)
            {
                policyTypeEntity.ModifiedDate = DateTime.Now;
                if (_policyTypeServices.UpdatePolicyType(policyType.PolicyTypeID, policyTypeEntity))
                {
                    return Request.CreateResponse(HttpStatusCode.OK, policyType.PolicyTypeID);
                }
            }
            else
            {
                policyTypeEntity.CreatedDate = DateTime.Now;
                var res = _policyTypeServices.CreatePolicyType(policyTypeEntity);
                if (res > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, policyTypeEntity.PolicyTypeID);
                }
            }
            return Request.CreateResponse(HttpStatusCode.InternalServerError, "Error while saving policy type.");
        }

        [Authorize]
        [Route("deletepolicytype/{id}")]
        [HttpGet]
        public HttpResponseMessage DeletePolicyType(int id)
        {
            if (_policyTypeServices.DeletePolicyType(id))
            {
                return Request.CreateResponse(HttpStatusCode.OK, id);
            }
            return Request.CreateResponse(HttpStatusCode.InternalServerError, "Error while deleting policy type.");
        }
    }
}