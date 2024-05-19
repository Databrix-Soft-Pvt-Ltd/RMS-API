using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Model.RequestModel
{
    public class RefillingProcess_Model
    {
        public string Process_Type { get; set; } // Approval - Close- Dispath/ Partial Dispatch...
        public string REFNumber { get; set; }
        public string[] ListRef1 { get; set; } 
        public int? IsProcessBy { get; set; } 
        public string? Dispatch_Address { get; set; }
        public int? Courier_ID { get; set; }
        public string? Consignments_Number { get; set; }

    }
    public class GetAllRefillingModels_Closed
    {

        public string Ref_Number { get; set; }
        public string Status { get; set; }
        public DateTime? Closed_Date { get; set; }
        public string? UserName { get; set; }
        public string? Courier_Name { get; set; }
        public string? Consignments_Number { get; set; }

    }

    public class GetAllRetrivelModels_Ack
    {
        public string Ref_Number { get; set; }
        public string Status { get; set; } 
        public DateTime Closed_Date { get; set; }
        public string? Closed_by { get; set; }
        public DateTime Ack_Date { get; set; }
        public string? Ack_by { get; set; }
        public string? Courier_Name { get; set; }
        public string? Consignments_Number { get; set; }
    }
}
