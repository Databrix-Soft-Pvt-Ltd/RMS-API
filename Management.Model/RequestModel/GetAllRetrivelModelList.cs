using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Model.RequestModel
{
    public class GetAllRetrivelModels
    {
        
            public string ReqNumber { get; set; }
            public string Status { get; set; }
            public DateTime? Closed_Date { get; set; }
            public string? UserName { get; set; }
     
    }
    public class GetAllRetrivelModels_Approval
    { 
        public string ReqNumber { get; set; }
        public string Status { get; set; }
        public DateTime? Closed_Date { get; set; }
        public string? UserName { get; set; }

    }

    public class GetAllRetrivelModels_Dispatch
    {
        public string ReqNumber { get; set; }
        public string Status { get; set; }
        [DataType(DataType.Date)]
        public DateTime Closed_Date { get; set; }
        [DataType(DataType.Date)]
        public DateTime Approval_Date { get; set; }
        public string? UserName { get; set; }
        public string? ClosedName { get; set; }

    }

    public class GetAllRetrivelModels_Ref
    {
        public string Ref { get; set; }
        public string ReqNumber { get; set; }

        public string retrieval_reason { get; set; }
        public string retrieval_type { get; set; }
        public string remarks { get; set; }
        public string FileStatus { get; set; }
        [DataType(DataType.Date)]
        public DateTime? RequestDate { get; set; } 
        public string? UserName { get; set; } 
        public int? ApprovalBy { get; set; }
        public DateTime? Approval_Date { get; set; }

    }
}
