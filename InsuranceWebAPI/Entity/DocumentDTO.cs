using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InsuranceWebAPI.Entity
{
    public class DocumentDTO
    {
        public int DocumentId { get; set; }
        public string DocumentName { get; set; }
        public string DocumentPath { get; set; }
        public int? PolicyId { get; set; }
    }
}