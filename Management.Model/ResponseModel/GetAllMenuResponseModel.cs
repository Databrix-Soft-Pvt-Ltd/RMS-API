using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Model.ResponseModel
{
    public class GetAllMenuResponseModel
    {
        public long Id { get; set; }
        public long SubMID { get; set; }
        public string? MainMenu { get; set; } 
        public string? SubMenu { get; set; }
        public bool? isParent { get; set; }
        public bool? isChild { get; set; }
 
    }
}
