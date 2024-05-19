using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Model.RequestModel
{
    public class RetrivelProcess_Model
    {
        public string Process_Type { get; set; } // Approval - Close- Dispath/ Partial Dispatch...
        public string REQNumber { get; set; }
        public string[] Ref1 { get; set; } 
        public int? IsProcessBy { get; set; }
        public int? CourierId { get; set; }
        public string? ConsignmentsNumber { get; set; }
    }
}
