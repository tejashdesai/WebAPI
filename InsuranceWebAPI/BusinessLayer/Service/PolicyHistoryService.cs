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
                EndDate = policyHistoryEntity.EndDate,
                IsCurrent = true,
                PolicyAmount = policyHistoryEntity.PolicyAmount,
                PolicyID = policyHistoryEntity.PolicyID.Value,
                PolicyNumber = policyHistoryEntity.PolicyNumber,
                StartDate = policyHistoryEntity.StartDate
            };
            _unitOfWork.PolicyHistoryRepository.Insert(policyHistory);
            _unitOfWork.Save();
            return policyHistory.PolicyHistoryID;
        }

        public bool DeletePolicyHistory(int policyHistoryId)
        {
            var success = false;
            var policyHistory = _unitOfWork.PolicyHistoryRepository.GetByID(policyHistoryId);
            if (policyHistory != null)
            {
                policyHistory.IsDeleted = true;

                _unitOfWork.PolicyHistoryRepository.Update(policyHistory);
                _unitOfWork.Save();
                success = true;
            }
            return success;
        }

        public IEnumerable<CurrentPolicyModel> GetCurrentPolicy(bool isCurrent, bool isDashboard)
        {
            try
            {
                var policyHistories = new List<PolicyHistory>();
                if (isDashboard)
                {
                    policyHistories = _unitOfWork.PolicyHistoryRepository
               .GetManyQueryable(ph => ph.EndDate <= DateTime.Now.AddDays(30) && ph.EndDate >= DateTime.Now).ToList();
                }
                else
                {
                    policyHistories = _unitOfWork.PolicyHistoryRepository
               .GetManyQueryable(ph => ph.IsCurrent.HasValue ? ph.IsCurrent.Value : false).ToList();
                }

                if (policyHistories.Any())
                {
                    var currentPolicy = policyHistories.Select(x => new CurrentPolicyModel
                    {
                        Name = x.Policy.Name,
                        Email = x.Policy.Email,
                        EndDate = x.EndDate,
                        Mobile = x.Policy.Mobile,
                        PolicyHistoryID = x.PolicyHistoryID,
                        PolicyID = x.PolicyID,
                        PolicyNumber = x.PolicyNumber,
                        PolicyType = x.Policy.PolicyType1.PolicyType1,
                        StartDate = x.StartDate,
                        Month = x.StartDate.Value.Month
                    }).ToList();
                    return currentPolicy;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
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
                    policyHistory.EndDate = policyHistoryEntity.EndDate;
                    policyHistory.IsCurrent = policyHistoryEntity.StartDate <= DateTime.Now && policyHistoryEntity.EndDate > DateTime.Now ? true : false;
                    policyHistory.PolicyAmount = policyHistoryEntity.PolicyAmount;
                    policyHistory.PolicyID = policyHistoryEntity.PolicyID.Value;
                    policyHistory.PolicyNumber = policyHistoryEntity.PolicyNumber;
                    policyHistory.StartDate = policyHistoryEntity.StartDate;
                    policyHistory.ModifiedDate = DateTime.Now;

                    _unitOfWork.PolicyHistoryRepository.Update(policyHistory);
                    _unitOfWork.Save();
                    success = true;
                }
            }
            return success;
        }

        public IEnumerable<SummaryModel> GetSummary()
        {
            try
            {
                var summary = new List<SummaryModel>();

                summary = (from month in Enumerable.Range(1, 12)
                           let key = month
                           join policyHistory in _unitOfWork.PolicyHistoryRepository.GetManyQueryable(ph => ph.IsCurrent.HasValue ? ph.IsCurrent.Value : false)
                           on key equals policyHistory.StartDate.Value.Month into g
                           select new SummaryModel { Month = key, Count = g.Count() }).ToList();

                return summary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}