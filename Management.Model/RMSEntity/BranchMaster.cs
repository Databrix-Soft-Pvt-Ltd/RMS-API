using System;
using System.Collections.Generic;

namespace Management.Model.RMSEntity
{
    public partial class BranchMaster
    {
        public long Id { get; set; }
        public string? BranchName { get; set; }
        public string? BranchCode { get; set; }
        public string? Address { get; set; }
        public bool? IsActive { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public long? UpdatedBy { get; set; }
    }
}
