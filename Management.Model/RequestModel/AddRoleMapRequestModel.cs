using Management.Model.RMSEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Model.RequestModel
{
    public class AddRoleMapRequestModel
    {
        public int RoleId { get; set; } 
        public int MenuId { get; set; }
        public bool? ViewRights { get; set; }
        public bool? AddRights { get; set; }
        public bool? ModifyRights { get; set; }
        public bool? DeleteRights { get; set; }
        public bool? DownloadRights { get; set; }
        public bool? ApprovalRights { get; set; }
    }
}
