using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Model.RequestModel
{
    public class AddRoleRequestModel
    {

        public string RoleName { get; set; } = null!;
        public string Description { get; set; } = null!;

        public bool? IsActive { get; set; }

    }
}
