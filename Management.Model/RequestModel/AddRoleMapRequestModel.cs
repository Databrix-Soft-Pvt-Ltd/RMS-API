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
        public int Role_ID { get; set; }
        public List<MainMenu1> mainMenu { get; set; }
    }
    public class MainMenu1
    {
        public long? MainId { get; set; }
        public string ParentName { get; set; }
        public bool IsSeleted { get; set; }
        public List<SubMainMenu1> SubMenu { get; set; }

    }
    public class SubMainMenu1
    {
        public long? SubMenuID { get; set; }
        public string ChildName { get; set; }
        public bool SubIsSelected { get; set; }
        public bool ISViewRights { get; set; }
        public bool IsAddRights { get; set; }
        public bool IsModifyRights { get; set; }
        public bool IsDeleteRights { get; set; }
        public bool IsDownloadRights { get; set; }
        public bool IsApprovalRights { get; set; }

    }
}
 
