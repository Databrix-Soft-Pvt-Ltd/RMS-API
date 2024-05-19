using System;
using System.Collections.Generic;

namespace Management.Model.RMSEntity
{
    public partial class RoleFeature1
    {
        public long Id { get; set; }
        public long? RoleId { get; set; }
        public long? SubMenuId { get; set; }
        public bool? ViewRights { get; set; }
        public bool? AddRights { get; set; }
        public bool? ModifyRights { get; set; }
        public bool? DeleteRights { get; set; }
        public bool? DownloadRights { get; set; }
        public bool? ApprovalRights { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
