using InsuranceWebAPI.BusinessLayer.Interface;
using InsuranceWebAPI.BusinessLayer.Service;
using InsuranceWebAPI.Entity;
using Newtonsoft.Json;
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
            dynamic policy = JsonConvert.DeserializeObject(result.FormData["policy"]);

            var policyEntity = new PolicyDTO
            {
                AdditionalName1 = policy.additionalName1,
                AdditionalName2 = policy.additionalName2,
                AdditionalName3 = policy.additionalName3,
                Address1 = policy.address1,
                Address2 = policy.address2,
                City = policy.city,
                CreatedDate = DateTime.Now,
                Email = policy.email,
                Mobile = policy.mobile,
                Mobile1 = policy.mobile1,
                Name = policy.name,
                PolicyType = policy.policyType,
                IsActive = true,
                IsDeleted = false
            };

            var policyRes = 0;
            if (policy.policyID > 0)
            {
                var res = _policyServices.UpdatePolicy(policy.policyID, policyEntity);
                if (res)
                {
                    policyRes = policy.policyID;
                }
            }
            else
            {
                policyRes = _policyServices.CreatePolicy(policyEntity);
            }
            var deletedFiles = policy.deletedFiles;
            if (deletedFiles != null)
            {
                var fileList = policy.deletedFiles.split(',');
                foreach (var item in fileList)
                {
                    _documentServices.DeleteDocument(policyRes, item);
                    File.Delete(Path.Combine(HttpContext.Current.Server.MapPath("~/Documents/" + policyRes), item));
                }
            }

            if (policyRes > 0)
            {
                var policyHistory = new PolicyHistoryDTO
                {
                    EndDate = policy.endDate,
                    IsCurrent = true,
                    PolicyAmount = policy.policyAmount,
                    PolicyID = policyRes,
                    PolicyNumber = policy.policyNumber,
                    StartDate = policy.startDate,
                    IsDeleted = false
                };
                var policyHistoryRes = 0;
                if (policy.policyHistoryID > 0)
                {
                    var res = _policyHistoryServices.UpdatePolicyHistory(policy.policyHistoryID, policyHistory);
                    if (res)
                    {
                        policyHistoryRes = policy.policyHistoryID;
                    }
                }
                else
                {
                    policyHistoryRes = _policyHistoryServices.CreatePolicyHistory(policyHistory);
                }

                if (policyHistoryRes > 0)
                {
                    var subDir = HttpContext.Current.Server.MapPath("~/Documents/" + policyRes);
                    Directory.CreateDirectory(subDir);
                    //get the files
                    foreach (var file in result.FileData)
                    {
                        FileInfo finfo = new FileInfo(file.LocalFileName);
                        var fileName = file.Headers.ContentDisposition.FileName.Replace("\"", "");
                        var destFile = Path.Combine(subDir, fileName);
                        if (File.Exists(destFile))
                        {
                            File.Delete(destFile);
                        }
                        File.Move(finfo.FullName, destFile);
                        _documentServices.CreateDocument(policyRes, fileName, destFile);
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