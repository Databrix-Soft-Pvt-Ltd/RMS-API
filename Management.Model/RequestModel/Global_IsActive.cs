using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Model.RequestModel
{
    public class Global_IsActive
    {
        public BranchMap_IsAction BranchMapList { get; set; }
        public User_IsAction UserList { get; set; } 
        public Client_IsAction ClientList { get; set; }
        public Role_IsAction RoleList { get; set; }
    }
    public class BranchMap_IsAction
    {   
        public int BranchID { get; set; }
        public int UserID { get; set; }
        public bool IsActive { get; set; }
    }
    public class User_IsAction
    { 
        public int UserID { get; set; }
        public bool IsActive { get; set; }
    }
    public class Client_IsAction
    {
        public int clientId { get; set; }
        public int UserID { get; set; }
        public bool IsActive { get; set; }
    }
    public class Role_IsAction
    {
        public int Role_ID { get; set; }
        public int Menu_id { get; set; }
        public bool IsActive { get; set; }
    }
}
