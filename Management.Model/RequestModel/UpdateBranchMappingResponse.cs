using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Model.RequestModel
{
    public class UpdateBranchMappingResponse
    {
        public long? UserId { get; set; }
        public int[] BranchId { get; set; }
   
        
    }
}
