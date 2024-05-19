using System;
using System.Collections.Generic;

namespace Management.Model.RMSEntity
{
    public partial class RoleMaster
    {
        public long Id { get; set; }
        public string? RoleName { get; set; }
        public string? Description { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? IsActive { get; set; }
        public long? ModifyBy { get; set; }
        public long? ModifyDate { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
