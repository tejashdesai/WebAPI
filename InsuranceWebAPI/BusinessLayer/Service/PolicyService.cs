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
    public class PolicyService : IPolicyService
    {
        private readonly UnitOfWork _unitOfWork;

        /// <summary>
        /// Public constructor.
        /// </summary>
        public PolicyService()
        {
            _unitOfWork = new UnitOfWork();
        }

        public int CreatePolicy(PolicyDTO policyEntity)
        {
            var policy = new Policy
            {
                AdditionalName1 = policyEntity.AdditionalName1,
                AdditionalName2 = policyEntity.AdditionalName2,
                AdditionalName3 = policyEntity.AdditionalName3,
                Address1 = policyEntity.Address1,
                Address2 = policyEntity.Address2,
                City = policyEntity.City,
                CreatedDate = DateTime.Now,
                Email = policyEntity.Email,
                Mobile = policyEntity.Mobile,
                Mobile1 = policyEntity.Mobile1,
                Name = policyEntity.Name,
                PolicyType = policyEntity.PolicyType
            };
            _unitOfWork.PolicyRepository.Insert(policy);
            _unitOfWork.Save();
            return policy.PolicyID;
        }

        public bool DeletePolicy(int policyId)

        {
            var success = false;
            var policy = _unitOfWork.PolicyRepository.GetByID(policyId);
            if (policy != null)
            {
                policy.IsDeleted = true;

                _unitOfWork.PolicyRepository.Update(policy);
                _unitOfWork.Save();
                success = true;
            }
            return success;
        }

        public IEnumerable<PolicyDTO> GetAllPolicy()
        {
            var policies = _unitOfWork.PolicyRepository.GetAll().Where(x => !x.IsDeleted.Value).ToList();
            if (policies.Any())
            {
                var policyModel = Mapper.Map<List<Policy>, List<PolicyDTO>>(policies);
                return policyModel;
            }
            return null;
        }

        public ModifyPolicy GetPolicyById(int policyId)
        {
            var policy = _unitOfWork.PolicyRepository.GetByID(policyId);
            if (policy != null && !policy.IsDeleted.Value)
            {
                var policyModel = new ModifyPolicy
                {
                    AdditionalName1 = policy.AdditionalName1,
                    AdditionalName2 = policy.AdditionalName2,
                    AdditionalName3 = policy.AdditionalName3,
                    Address1 = policy.Address1,
                    Address2 = policy.Address2,
                    City = policy.City,
                    Email = policy.Email,
                    Mobile = policy.Mobile,
                    Mobile1 = policy.Mobile1,
                    Name = policy.Name,
                    PolicyID = policy.PolicyID,
                    PolicyType = policy.PolicyType,
                    PolicyHistory = policy.PolicyHistories.Select(x => new PolicyHistoryDTO
                    {
                        EndDate = x.EndDate,
                        IsCurrent = x.IsCurrent,
                        IsDeleted = x.IsDeleted,
                        PolicyAmount = x.PolicyAmount,
                        PolicyHistoryID = x.PolicyHistoryID,
                        PolicyNumber = x.PolicyNumber,
                        StartDate = x.StartDate
                    }).ToList()
                };
                return policyModel;
            }
            return null;
        }

        public bool UpdatePolicy(int policyId, PolicyDTO policyEntity)
        {
            var success = false;
            if (policyEntity != null)
            {
                var policy = _unitOfWork.PolicyRepository.GetByID(policyId);
                if (policy != null)
                {
                    policy.AdditionalName1 = policyEntity.AdditionalName1;
                    policy.AdditionalName2 = policyEntity.AdditionalName2;
                    policy.AdditionalName3 = policyEntity.AdditionalName3;
                    policy.Address1 = policyEntity.Address1;
                    policy.Address2 = policyEntity.Address2;
                    policy.City = policyEntity.City;
                    policy.ModifiedDate = DateTime.Now;
                    policy.Email = policyEntity.Email;
                    policy.Mobile = policyEntity.Mobile;
                    policy.Mobile1 = policyEntity.Mobile1;
                    policy.Name = policyEntity.Name;
                    policy.PolicyType = policyEntity.PolicyType;

                    _unitOfWork.PolicyRepository.Update(policy);
                    _unitOfWork.Save();
                    success = true;
                }
            }
            return success;
        }
    }
}