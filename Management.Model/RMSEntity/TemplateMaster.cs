using System;
using System.Collections.Generic;

namespace Management.Model.RMSEntity
{
    public partial class TemplateMaster
    {
        public int TempId { get; set; }
        public string? TempName { get; set; }
        public string? TempDescription { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? IsCreatedDate { get; set; }
        public int? IsCreatedBy { get; set; }
        public DateTime? IsModifiedDate { get; set; }
        public int? IsModifiledBy { get; set; }
    }
}
