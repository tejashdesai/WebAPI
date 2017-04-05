using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InsuranceWebAPI.Entity
{
    public class PolicyHistoryDTO
    {
        public int PolicyHistoryID { get; set; }
        public int? PolicyID { get; set; }
        public string PolicyNumber { get; set; }
        public decimal? PolicyAmount { get; set; }
        public bool? IsCurrent { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsDeleted { get; set; }
    }

    public class CurrentPolicyModel
    {
        public int PolicyHistoryID { get; set; }
        public int PolicyID { get; set; }
        public DateTime? EndDate { get; set; }
        public string PolicyNumber { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string PolicyType { get; set; }
        public DateTime? StartDate { get; set; }
        public string Name { get; set; }
        public int Month { get; set; }
    }

    public class SummaryModel
    {
        public int Month { get; set; }
        public int? Count { get; set; }
    }
}