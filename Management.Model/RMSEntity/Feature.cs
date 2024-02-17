using System;
using System.Collections.Generic;

namespace Management.Model.RMSEntity
{
    public partial class Feature
    {
        public Feature()
        {
            RoleFeatures = new HashSet<RoleFeature>();
        }

        public long FeatureIdPk { get; set; }
        public string FeatureName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<RoleFeature> RoleFeatures { get; set; }
    }
}
