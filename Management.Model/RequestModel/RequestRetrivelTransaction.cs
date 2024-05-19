using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Model.RequestModel
{
    public class RequestRetrivelTransaction
    {
        public string SetFlage { get; set; }
        public string? ReqNumber { get; set; } 
        public string[] ListRef1 { get; set; }
        public string? RetrievalType { get; set; } 
        public string? RetrievalRegion { get; set; } 
        public string? Remarks { get; set; }
 
    }
}
