using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Model.RequestModel
{
    public class GetAllBranchMappingResponse
    {
        public long? Id { get; set; }

        public long? userId { get; set; }
        
        public long? BranchId { get; set; }
        public string? BranchName { get; set; }
        public string? UserName { get; set; }
    }
}
