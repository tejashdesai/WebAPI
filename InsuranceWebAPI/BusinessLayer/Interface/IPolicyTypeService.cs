using InsuranceWebAPI.Entity;
using System.Collections.Generic;

namespace InsuranceWebAPI.BusinessLayer.Interface
{
    public interface IPolicyTypeService
    {
        PolicyTypeDTO GetPolicyTypeById(int PolicyTypeId);
        IEnumerable<PolicyTypeDTO> GetAllPolicyType();
        int CreatePolicyType(PolicyTypeDTO PolicyTypeEntity);
        bool UpdatePolicyType(int PolicyTypeId, PolicyTypeDTO PolicyTypeEntity);
        bool DeletePolicyType(int PolicyTypeId);
    }
}
