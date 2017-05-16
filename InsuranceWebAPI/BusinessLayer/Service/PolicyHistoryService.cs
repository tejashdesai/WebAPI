using InsuranceWebAPI.BusinessLayer.Interface;
using InsuranceWebAPI.DataLayer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InsuranceWebAPI.Entity;
using InsuranceWebAPI.DataLayer;

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
                PolicyID = policyHistoryEntity.PolicyID.HasValue ? policyHistoryEntity.PolicyID.Value : 0,
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
               .GetManyQueryable(ph => ph.EndDate <= DateTime.Now.AddDays(30) && ph.EndDate >= DateTime.Now && (ph.Policy.IsDeleted.HasValue ? !ph.Policy.IsDeleted.Value : false)).ToList();
                }
                else
                {
                    //     policyHistories = _unitOfWork.PolicyHistoryRepository
                    //.GetManyQueryable(ph => (ph.IsCurrent.HasValue ? ph.IsCurrent.Value : false) && (ph.Policy.IsDeleted.HasValue ? !ph.Policy.IsDeleted.Value : false)).ToList();
                    policyHistories = _unitOfWork.PolicyHistoryRepository
                    .GetManyQueryable(ph => (DateTime.Now.Date >= (ph.StartDate.HasValue ? ph.StartDate.Value.Date : DateTime.Now.Date) &&
                     DateTime.Now.Date <= (ph.EndDate.HasValue ? ph.EndDate.Value.Date : DateTime.Now.Date)) &&
                     (ph.Policy.IsDeleted.HasValue ? !ph.Policy.IsDeleted.Value : false)).ToList();
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
                        PolicyType = x.Policy?.PolicyType1?.PolicyTypeName,
                        StartDate = x.StartDate,
                        Month = x.EndDate.HasValue ? x.EndDate.Value.Month : 0
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
                    policyHistory.PolicyID = policyHistoryEntity.PolicyID.HasValue ? policyHistoryEntity.PolicyID.Value : 0;
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
                           join policyHistory in _unitOfWork.PolicyHistoryRepository.GetManyQueryable(ph => (ph.IsCurrent.HasValue ? ph.IsCurrent.Value : false) && (ph.Policy.IsDeleted.HasValue ? !ph.Policy.IsDeleted.Value : false))
                           on key equals policyHistory.EndDate.HasValue ? policyHistory.EndDate.Value.Month : 0 into g
                           select new SummaryModel { Month = key, Count = g.Count() }).ToList();

                return summary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<CurrentPolicyModel> GetExpiredPolicy()
        {
            try
            {
                var policyHistories = new List<PolicyHistory>();
                policyHistories = _unitOfWork.PolicyHistoryRepository
                .GetManyQueryable(ph => (DateTime.Now.Date > (ph.EndDate.HasValue ? ph.EndDate.Value.Date : DateTime.Now.Date)) &&
                 (ph.Policy.IsDeleted.HasValue ? !ph.Policy.IsDeleted.Value : false)).ToList();

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
                        PolicyType = x.Policy?.PolicyType1?.PolicyTypeName,
                        StartDate = x.StartDate,
                        Month = x.StartDate.HasValue ? x.StartDate.Value.Month : 0
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
    }
}