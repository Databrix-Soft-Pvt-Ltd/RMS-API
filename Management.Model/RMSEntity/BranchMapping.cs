using System;
using System.Collections.Generic;

namespace Management.Model.RMSEntity
{
    public partial class BranchMapping
    {
        public long Id { get; set; }
        public long? BranchId { get; set; }
        public long? UserId { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? IsActive { get; set; }
    }
}
