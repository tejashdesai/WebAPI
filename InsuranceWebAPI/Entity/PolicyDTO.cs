using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InsuranceWebAPI.Entity
{
    public class PolicyDTO
    {
        public int PolicyID { get; set; }
        public string Name { get; set; }
        public int? PolicyType { get; set; }
        public string Mobile { get; set; }
        public string Mobile1 { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string AdditionalName1 { get; set; }
        public string AdditionalName2 { get; set; }
        public string AdditionalName3 { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
    }
}