using System;
using System.Collections.Generic;

namespace Management.Model.RMSEntity
{
    public partial class Role
    {
        public Role()
        {
            RoleFeatures = new HashSet<RoleFeature>();
        }

        public long RoleIdPk { get; set; }
        public string RoleName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<RoleFeature> RoleFeatures { get; set; }
    }
}
