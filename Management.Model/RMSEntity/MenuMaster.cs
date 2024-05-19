using System;
using System.Collections.Generic;

namespace Management.Model.RMSEntity
{
    public partial class MenuMaster
    {
        public long Id { get; set; }
        public string? MainMenu { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? IsParentActive { get; set; }
    }
}
