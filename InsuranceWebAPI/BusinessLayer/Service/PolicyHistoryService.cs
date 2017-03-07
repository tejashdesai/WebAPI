using InsuranceWebAPI.BusinessLayer.Interface;
using InsuranceWebAPI.DataLayer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InsuranceWebAPI.Entity;
using InsuranceWebAPI.DataLayer;
using AutoMapper;

namespace InsuranceWebAPI.BusinessLayer.Service
{
    public class PolicyHistoryService : IPolicyHistoryService
    {
        private readonly UnitOfWork _unitOfWork;

        /// <summary>
        /// Public constructor.
        /// </summary>
        public PolicyHistoryService()
        {
            _unitOfWork = new UnitOfWork();
        }

        public int CreatePolicyHistory(PolicyHistoryDTO policyHistoryEntity)
        {
            var policyHistory = new PolicyHistory
            {
                //ProductName = productEntity.ProductName
            };
            _unitOfWork.PolicyHistoryRepository.Insert(policyHistory);
            _unitOfWork.Save();
            return policyHistory.PolicyHistoryID;
        }

        public bool DeletePolicyHistory(int policyHistoryId)
        {
            var success = false;
            if (policyHistoryId > 0)
            {
                var policyHistory = _unitOfWork.PolicyHistoryRepository.GetByID(policyHistoryId);
                if (policyHistory != null)
                {

                    _unitOfWork.PolicyHistoryRepository.Delete(policyHistory);
                    _unitOfWork.Save();
                    success = true;
                }
            }
            return success;
        }

        public IEnumerable<PolicyHistoryDTO> GetAllPolicyHistory()
        {
            var policyHistories = _unitOfWork.PolicyHistoryRepository.GetAll().ToList();
            if (policyHistories.Any())
            {
                //Mapper.CreateMap<Product, ProductEntity>();
                var policyHistoryModel = Mapper.Map<List<PolicyHistory>, List<PolicyHistoryDTO>>(policyHistories);
                return policyHistoryModel;
            }
            return null;
        }

        public IEnumerable<CurrentPolicyModel> GetCurrentPolicy()
        {
            try
            {
                var policyHistories = _unitOfWork.PolicyHistoryRepository
                .GetManyQueryable(ph => ph.IsCurrent.HasValue ? ph.IsCurrent.Value : false).ToList();
                if (policyHistories.Any())
                {
                    var currentPolicy = policyHistories.Select(x => new CurrentPolicyModel
                    {
                        Email = x.Policy.Email,
                        EndDate = x.EndDate,
                        Mobile = x.Policy.Mobile,
                        PolicyHistoryID = x.PolicyHistoryID,
                        PolicyID = x.PolicyID,
                        PolicyNumber = x.PolicyNumber,
                        PolicyType = x.Policy.PolicyType1.PolicyType1,
                        StartDate = x.StartDate
                    }).ToList();
                    return currentPolicy;
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public bool UpdatePolicyHistory(int policyHistoryId, PolicyHistoryDTO policyHistoryEntity)
        {
            var success = false;
            if (policyHistoryEntity != null)
            {
                var policyHistory = _unitOfWork.PolicyHistoryRepository.GetByID(policyHistoryId);
                if (policyHistory != null)
                {
                    //product.ProductName = productEntity.ProductName;
                    _unitOfWork.PolicyHistoryRepository.Update(policyHistory);
                    _unitOfWork.Save();
                    success = true;
                }
            }
            return success;
        }
    }
}