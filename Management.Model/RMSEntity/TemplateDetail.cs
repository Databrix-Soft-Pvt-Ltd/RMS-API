using System;
using System.Collections.Generic;

namespace Management.Model.RMSEntity
{
    public partial class TemplateDetail
    {
        public long Id { get; set; }
        public int? TempId { get; set; }
        public string DisplayName { get; set; } = null!;
        public string? DatabaseName { get; set; }
        public string? DataType { get; set; }
        public int? MaxLength { get; set; }
        public bool? IsMandatory { get; set; }
        public bool? IsUnique { get; set; }
        public string? MasterValue { get; set; }
        public string? ControlType { get; set; }
        public string? DateFormat { get; set; }
        public string? Validation { get; set; }
        public bool? IsActive { get; set; }
    }
}
