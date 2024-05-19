using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Model.RequestModel
{
    public class UpdateRoleMappingResponse
    { 
        //public  int ID { get; set; }
        public long? RoleId { get; set; } 
        public UpdateRoleRights[] UpdateRights { get; set; }
    }
    public class UpdateRoleRights
    {
        public long MenuId { get; set; }
        public bool? ViewRights { get; set; }
        public bool? AddRights { get; set; }
        public bool? ModifyRights { get; set; }
        public bool? DeleteRights { get; set; }
        public bool? DownloadRights { get; set; }
        public bool? ApprovalRights { get; set; }
    }

    public class UpdateRoleRight_by_Role_ID
    {
        public long RoleId { get; set; }
        public bool? ViewRights { get; set; }
        public bool? AddRights { get; set; }
        public bool? ModifyRights { get; set; }
        public bool? DeleteRights { get; set; }
        public bool? DownloadRights { get; set; }
        public bool? ApprovalRights { get; set; }
    }


}
