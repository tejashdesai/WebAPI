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
    public class PolicyTypeService : IPolicyTypeService
    {
        private readonly UnitOfWork _unitOfWork;

        public PolicyTypeService()
        {
            _unitOfWork = new UnitOfWork();
        }

        public int CreatePolicyType(PolicyTypeDTO PolicyTypeEntity)
        {
            var policyType = new PolicyType
            {
                CreatedDate = DateTime.Now,
                PolicyTypeName = PolicyTypeEntity.PolicyTypeName
            };
            _unitOfWork.PolicyTypeRepository.Insert(policyType);
            _unitOfWork.Save();
            return policyType.PolicyTypeID;
        }

        public bool DeletePolicyType(int PolicyTypeId)
        {
            var success = false;
            var entity = _unitOfWork.PolicyTypeRepository.GetByID(PolicyTypeId);
            _unitOfWork.PolicyTypeRepository.Delete(entity);
            _unitOfWork.Save();
            success = true;
            return success;
        }

        public IEnumerable<PolicyTypeDTO> GetAllPolicyType()
        {
            var policyTypes = _unitOfWork.PolicyTypeRepository.GetAll().ToList();
            if (policyTypes.Any())
            {
                var policyTypeModel = policyTypes.Select(x => new PolicyTypeDTO
                {
                    CreatedDate = x.CreatedDate,
                    ModifiedDate = x.ModifiedDate,
                    PolicyTypeID = x.PolicyTypeID,
                    PolicyTypeName = x.PolicyTypeName
                });
                return policyTypeModel;
            }
            return null;
        }

        public PolicyTypeDTO GetPolicyTypeById(int PolicyTypeId)
        {
            var policyType = _unitOfWork.PolicyTypeRepository.GetByID(PolicyTypeId);
            if (policyType != null)
            {
                var policyTypeModel = new PolicyTypeDTO
                {
                    PolicyTypeID = policyType.PolicyTypeID,
                    PolicyTypeName = policyType.PolicyTypeName
                };
                return policyTypeModel;
            }
            return null;
        }

        public bool UpdatePolicyType(int PolicyTypeId, PolicyTypeDTO PolicyTypeEntity)
        {
            var success = false;
            if (PolicyTypeEntity != null)
            {
                var policyType = _unitOfWork.PolicyTypeRepository.GetByID(PolicyTypeId);
                if (policyType != null)
                {
                    policyType.PolicyTypeName = PolicyTypeEntity.PolicyTypeName;
                    policyType.ModifiedDate = PolicyTypeEntity.ModifiedDate;

                    _unitOfWork.PolicyTypeRepository.Update(policyType);
                    _unitOfWork.Save();
                    success = true;
                }
            }
            return success;
        }
    }
}