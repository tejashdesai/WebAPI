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
    
    public partial class Document
    {
        public int DocumentId { get; set; }
        public string DocumentName { get; set; }
        public Nullable<int> PolicyId { get; set; }
        public string DocumentPath { get; set; }
    
        public virtual Policy Policy { get; set; }
    }
}
