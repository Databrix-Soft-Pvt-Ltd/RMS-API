using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Model.RequestModel
{
    public class GetAllBranchResponseModel
    {
        public long Id { get; set; }
        public string? BranchName { get; set; }
        public string? BranchCode { get; set; }
        public string? Address { get; set; }
        public bool? IsActive { get; set; }
    }
}
