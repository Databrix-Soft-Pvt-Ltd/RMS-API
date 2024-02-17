using System;
using System.Collections.Generic;

namespace Management.Model.RMSEntity
{
    public partial class RetrievalType
    {
        public long Id { get; set; }
        public string? RetrievalType1 { get; set; }
        public bool? IsActive { get; set; }
        public bool? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
