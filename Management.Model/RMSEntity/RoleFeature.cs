using System;
using System.Collections.Generic;

namespace Management.Model.RMSEntity
{
    public partial class RoleFeature
    {
        public long FeatureIdFk { get; set; }
        public long RoleIdFk { get; set; }
        public bool ViewPerm { get; set; }
        public bool AddPerm { get; set; }
        public bool EditPerm { get; set; }
        public bool DeletePerm { get; set; }
        public long CreatedBy { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual Feature FeatureIdFkNavigation { get; set; } = null!;
        public virtual Role RoleIdFkNavigation { get; set; } = null!;
    }
}
