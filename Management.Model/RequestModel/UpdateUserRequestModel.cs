using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Model.RequestModel
{
    public class UpdateUserRequestModel
    {
        public long UserId { get; set; }
        public long RoleId { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; } 
        public string? EmailId { get; set; }
        public string? MobileNo { get; set; }
        public int UserType { get; set; }
        public int ByUserId { get; set; }
        public bool IsActive { get; set; } 
        public string? Ipaddress { get; set; }  
        
    }
}
