using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InsuranceWebAPI.Entity
{
    public class PolicyTypeDTO
    {
        public int PolicyTypeID { get; set; }
        public string PolicyTypeName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}