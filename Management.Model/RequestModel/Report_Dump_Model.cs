using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Model.RequestModel
{
    public class Report_Dump_Model
    {
       
        public string? Ref1 { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
    }
    public class Report_Retrivel_Model
    {
        public string? Reqnumber { get; set; } 
        public string? Ref1 { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
    public class Report_Refilling_Model
    {

        public string? Refnumber { get; set; }
        public string? Ref1 { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
