using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Model.RequestModel
{
    public class GetAllRoleMappingResponse
    { 
        public long? Id {  get; set; }
        public long? RoleId { get; set; }
        public long? MenuId { get; set; }

        public long? SubMenuId { get; set; }
        public bool? ViewRights { get; set; }
        public bool? AddRights { get; set; }
        public bool? ModifyRights { get; set; }
        public bool? DeleteRights { get; set; }
        public bool? DownloadRights { get; set; }   
        public bool? ApprovalRights { get; set; } 
        public string RoleName { get; set; }
        public string MenuName { get; set; }
        public string SubMenuName { get; set; }

    } 
    public class GetAllMenuFor_RoleMappingResponse
    {
        public MainMenu[] mainMenu { get; set; }
    }
    public class MainMenu
    {
        public object mainMenu;

        public long? Id { get; set; }
        public string ParentName { get; set; } 
        public SubMainMenu[] SubMenu { get; set; }

    }
    public class SubMainMenu
    {
        public long? Id { get; set; }
        public string ChildName { get; set; }

    }
} 