using System;
using System.Collections.Generic;

namespace Management.Model.RMSEntity
{
    public partial class CourierMaster
    {
        public long Id { get; set; }
        public string? CourierName { get; set; }
        public bool? IsActive { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}
