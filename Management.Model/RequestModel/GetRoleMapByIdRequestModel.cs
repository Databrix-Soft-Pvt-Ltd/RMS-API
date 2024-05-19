using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Model.RequestModel
{
    public class GetRoleMapByIdRequestModel
    {
        public long RoleId { get; set; }
         public long MenuID { get; set; } 
    }
}
