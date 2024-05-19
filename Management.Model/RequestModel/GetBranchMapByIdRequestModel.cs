using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Model.RequestModel
{
    public class GetBranchMapByIdRequestModel
    {
        public long? UserID { get; set; }
        public long? BranchID { get; set; }
    }
}
