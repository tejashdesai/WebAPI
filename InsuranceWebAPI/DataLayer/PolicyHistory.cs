//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InsuranceWebAPI.DataLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class PolicyHistory
    {
        public int PolicyHistoryID { get; set; }
        public int PolicyID { get; set; }
        public string PolicyNumber { get; set; }
        public Nullable<decimal> PolicyAmount { get; set; }
        public Nullable<bool> IsCurrent { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    
        public virtual Policy Policy { get; set; }
    }
}
