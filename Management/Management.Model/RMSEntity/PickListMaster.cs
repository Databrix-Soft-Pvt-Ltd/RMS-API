using System;
using System.Collections.Generic;

namespace Management.Model.RMSEntity
{
    public partial class PickListMaster
    {
        public long Id { get; set; }
        public string? LogoPath { get; set; }
        public long? TemplateId { get; set; }
        public string? Address { get; set; }
    }
}
