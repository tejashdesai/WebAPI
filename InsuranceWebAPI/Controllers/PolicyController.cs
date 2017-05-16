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
            var policyRes = 0;

            try
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

                PolicyDTO policyEntity = new PolicyDTO
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

                if (policy.policyID > 0)
                {
                    int pId = policy.policyID;
                    var res = _policyServices.UpdatePolicy(pId, policyEntity);
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
                    string deleteFiles = Convert.ToString(policy.deletedFiles);
                    var fileList = deleteFiles.Split(',');
                    foreach (var item in fileList)
                    {
                        _documentServices.DeleteDocument(policyRes, item);
                        if (File.Exists(Path.Combine(HttpContext.Current.Server.MapPath("~/Documents/" + policyRes), item)))
                        {
                            File.Delete(Path.Combine(HttpContext.Current.Server.MapPath("~/Documents/" + policyRes), item));
                        }
                    }
                }

                if (policyRes > 0)
                {
                    List<PolicyHistoryDTO> policyHistories = policy.policyHistory.ToObject<List<PolicyHistoryDTO>>();

                    for (int i = 0; i < policyHistories.Count; i++)
                    {
                        DateTime EndDate = policyHistories[i].EndDate.HasValue ? policyHistories[i].EndDate.Value : DateTime.Now;
                        DateTime StartDate = policyHistories[i].StartDate.HasValue ? policyHistories[i].StartDate.Value : DateTime.Now;
                        var policyHistory = new PolicyHistoryDTO
                        {
                            EndDate = EndDate,
                            IsCurrent = DateTime.Now.Date >= StartDate.Date && DateTime.Now.Date <= EndDate.Date ? true : false,
                            PolicyAmount = policyHistories[i].PolicyAmount,
                            PolicyID = policyRes,
                            PolicyNumber = policyHistories[i].PolicyNumber,
                            StartDate = StartDate,
                            IsDeleted = false
                        };
                        var policyHistoryRes = 0;

                        if (policyHistories[i].PolicyHistoryID > 0)
                        {
                            int phId = policyHistories[0].PolicyHistoryID;
                            var res = _policyHistoryServices.UpdatePolicyHistory(phId, policyHistory);
                            if (res)
                            {
                                policyHistoryRes = phId;
                            }
                        }
                        else
                        {
                            policyHistoryRes = _policyHistoryServices.CreatePolicyHistory(policyHistory);
                        }
                    }


                    if (policyRes > 0)
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
                            _documentServices.CreateDocument(policyRes, fileName, "Documents/" + policyRes + "/" + fileName);
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
            catch (Exception ex)
            {
                _policyServices.DeletePolicy(policyRes);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
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