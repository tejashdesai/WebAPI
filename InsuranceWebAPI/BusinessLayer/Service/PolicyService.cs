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
                //ProductName = productEntity.ProductName
            };
            _unitOfWork.PolicyRepository.Insert(policy);
            _unitOfWork.Save();
            return policy.PolicyID;
        }

        public bool DeletePolicy(int policyId)
        {
            var success = false;
            if (policyId > 0)
            {
                var policy = _unitOfWork.PolicyRepository.GetByID(policyId);
                if (policy != null)
                {

                    _unitOfWork.PolicyRepository.Delete(policy);
                    _unitOfWork.Save();
                    success = true;
                }
            }
            return success;
        }

        public IEnumerable<PolicyDTO> GetAllPolicy()
        {
            var policies = _unitOfWork.PolicyRepository.GetAll().ToList();
            if (policies.Any())
            {
                //Mapper.CreateMap<Product, ProductEntity>();
                var productsModel = Mapper.Map<List<Policy>, List<PolicyDTO>>(policies);
                return productsModel;
            }
            return null;
        }

        public PolicyDTO GetPolicyById(int policyId)
        {
            var policy = _unitOfWork.PolicyRepository.GetByID(policyId);
            if (policy != null)
            {
                var policyModel = Mapper.Map<Policy, PolicyDTO>(policy);
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
                    //product.ProductName = productEntity.ProductName;
                    _unitOfWork.PolicyRepository.Update(policy);
                    _unitOfWork.Save();
                    success = true;
                }
            }
            return success;
        }
    }
}