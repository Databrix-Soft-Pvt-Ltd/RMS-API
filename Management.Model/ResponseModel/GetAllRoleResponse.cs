using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Model.ResponseModel
{
    public class GetAllRoleResponse
    {

        public long RoleIdPk { get; set; }
        public string RoleName { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
