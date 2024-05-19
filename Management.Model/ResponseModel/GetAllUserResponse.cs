using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Model.ResponseModel
{
    public class GetAllUserResponse
    {
        public long UserId { get; set; }
        public long RoleId { get; set; }
        public string? UserName { get; set; }
        public string? EmailId { get; set; }
        public string? MobileNo { get; set; }
        public string Password { get; set; }

        public bool? isActive { get; set; }
    }
}
