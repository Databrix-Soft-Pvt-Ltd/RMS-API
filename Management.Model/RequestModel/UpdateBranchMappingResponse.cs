using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Model.RequestModel
{
    public class UpdateBranchMappingResponse
    {
        public long Id { get; set; }
        public long? BranchId { get; set; }
        public long? UserId { get; set; }
        
    }
}
