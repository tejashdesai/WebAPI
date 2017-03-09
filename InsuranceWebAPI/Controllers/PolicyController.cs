using InsuranceWebAPI.BusinessLayer.Interface;
using InsuranceWebAPI.BusinessLayer.Service;
using InsuranceWebAPI.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace InsuranceWebAPI.Controllers
{
    public class PolicyController : ApiController
    {
        private readonly IPolicyService _policyServices;
        private readonly IPolicyHistoryService _policyHistoryServices;
        private readonly IDocumentService _documentServices;

        public PolicyController()
        {
            _policyServices = new PolicyService();
            _policyHistoryServices = new PolicyHistoryService();
            _documentServices = new DocumentService();
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
        public async Task<HttpResponseMessage> SavePolicy()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var root = HttpContext.Current.Server.MapPath("~/Documents");
            Directory.CreateDirectory(root);
            var provider = new MultipartFormDataStreamProvider(root);
            var result = await Request.Content.ReadAsMultipartAsync(provider);
            if (result.FormData["policy"] == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            var policy = (NewPolicy)result.FormData["policy"];

            var policyEntity = new PolicyDTO
            {
                AdditionalName1 = policy.AdditionalName1,
                AdditionalName2 = policy.AdditionalName2,
                AdditionalName3 = policy.AdditionalName3,
                Address1 = policy.Address1,
                Address2 = policy.Address2,
                City = policy.City,
                CreatedDate = DateTime.Now,
                Email = policy.Email,
                Mobile = policy.Mobile,
                Mobile1 = policy.Mobile1,
                Name = policy.Name,
                PolicyType = policy.PolicyType
            };

            var policyRes = 0;
            if (policy.PolicyID > 0)
            {
                var res = _policyServices.UpdatePolicy(policy.PolicyID, policyEntity);
                if (res)
                {
                    policyRes = policy.PolicyID;
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
                    EndDate = policy.EndDate,
                    IsCurrent = true,
                    PolicyAmount = policy.PolicyAmount,
                    PolicyID = policyRes,
                    PolicyNumber = policy.PolicyNumber,
                    StartDate = policy.StartDate
                };
                var policyHistoryRes = 0;
                if (policy.PolicyHistoryID > 0)
                {
                    policyHistoryRes = _policyHistoryServices.CreatePolicyHistory(policyHistory);
                }
                else
                {
                    var res = _policyHistoryServices.UpdatePolicyHistory(policy.PolicyHistoryID, policyHistory);
                    if (res)
                    {
                        policyHistoryRes = policy.PolicyHistoryID;
                    }
                }

                if (policyHistoryRes > 0)
                {
                    //get the files
                    foreach (var file in result.FileData)
                    {
                        FileInfo finfo = new FileInfo(file.LocalFileName);
                        //File.Move(finfo.FullName,
                        //    Path.Combine(root, file.Headers.ContentDisposition.FileName.Replace("\"", "")));

                        _documentServices.CreateDocument(policyRes,finfo.FullName);
                    }

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