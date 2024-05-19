using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Model.RequestModel
{
    public class RequestRefillingTransaction
    {
        public string SetFlage { get; set; }
        public string? REF_Number { get; set; } 
        public string[] ListRef1 { get; set; }
        //public string? RefillingType { get; set; } 
        //public string? RefillingRegion { get; set; } 
        public string? Remarks { get; set; }
 
    }
}
