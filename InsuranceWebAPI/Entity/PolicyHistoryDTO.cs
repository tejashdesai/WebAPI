﻿using System;
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
}