using System;
using System.Collections.Generic;

namespace Management.Model.RMSEntity
{
    public partial class SubMenuMaster
    {
        public int SubMid { get; set; }
        public string? SubMenu { get; set; }
        public int? MainMenuId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? IsChildActive { get; set; }
    }
}
