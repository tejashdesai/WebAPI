using InsuranceWebAPI.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceWebAPI.BusinessLayer.Interface
{
    public interface IPolicyService
    {
        PolicyDTO GetPolicyById(int policyId);
        IEnumerable<PolicyDTO> GetAllPolicy();
        int CreatePolicy(PolicyDTO policyEntity);
        bool UpdatePolicy(int policyId, PolicyDTO policyEntity);
        bool DeletePolicy(int policyId);
    }
}
