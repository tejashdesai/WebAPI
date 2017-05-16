using InsuranceWebAPI.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceWebAPI.BusinessLayer.Interface
{
    public interface IPolicyHistoryService
    {
        int CreatePolicyHistory(PolicyHistoryDTO policyHistoryEntity);
        bool UpdatePolicyHistory(int policyHistoryId, PolicyHistoryDTO policyHistoryEntity);
        bool DeletePolicyHistory(int policyHistoryId);
        IEnumerable<CurrentPolicyModel> GetCurrentPolicy(bool isCurrent = true, bool isDashboard = false);
        IEnumerable<CurrentPolicyModel> GetExpiredPolicy();
        IEnumerable<SummaryModel> GetSummary();
    }
}
