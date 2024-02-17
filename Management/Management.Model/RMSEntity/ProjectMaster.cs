using System;
using System.Collections.Generic;

namespace Management.Model.RMSEntity
{
    public partial class ProjectMaster
    {
        public long Id { get; set; }
        public string? ProjectName { get; set; }
        public string? LoginPageLogo { get; set; }
        public string? HeaderPageLogo { get; set; }
        public bool? IsActive { get; set; }
    }
}
