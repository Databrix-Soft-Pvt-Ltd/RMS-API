using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Model.RequestModel
{
    public class RefillingTransactionModel
    {
        public object Id { get; set; }
        public object? RefNumber { get; set; }
        public object? Ref1 { get; set; }
        public object? RetrievalId { get; set; }
        public object? File_Status { get; set; }
        public object? Status { get; set; }
        public object? Remarks { get; set; }
        public object? ItemSatus { get; set; }
        public object? RefillingDate { get; set; }
        public object? RefillingBy { get; set; }
        public object? RefillingClosedDate { get; set; }
        public object? RefillingClosedBy { get; set; }
        public object? RefillingAckBy { get; set; }
        public object? RefillingAckDate { get; set; }
        public object? RefillingReceivedDate { get; set; }
        public object? RefillingReceivedBy { get; set; }
    }

    public class RetrievalTransactionModel
    {

        public object? ID { get; set; }
        public object? Req_Number { get; set; }
        public object? Ref1 { get; set; }
        public object? File_Status { get; set; }
        public object? Status { get; set; }
        public object? Remarks { get; set; }
        public object? Item_Status { get; set; }
        public object? RetrievalBy { get; set; }
        public object? Request_Date { get; set; }
        public object? RetrievalClosedBy { get; set; }
        public object? RetrievalClosedDate { get; set; }
        public object? RetrievalApprovalBy { get; set; }
        public object? RetrievalApprovalDate { get; set; }
        public object? RetrievalRejectedBy { get; set; }
        public object? RetrievalRejectedDate { get; set; }
        public object? RetrievalCourierAckBy { get; set; }
        public object? RetrievalCourierAckDate { get; set; }
        public object? Dispatch_Date { get; set; }
        public object? CourierName { get; set; }
        public object? Consignments_Number { get; set; }
    }
    public class QuickSearch
    {
        public object? ID { get; set; }
        public object? Ref1 { get; set; }
        public object? Ref2 { get; set; }
        public object? Ref3 { get; set; }
        public object? Ref4 { get; set; }
        public object? Ref5 { get; set; }
        public object? Ref6 { get; set; }
        public object? Ref7 { get; set; }
        public object? Ref8 { get; set; }
        public object? Ref9 { get; set; }
        public object? Ref10 { get; set; }
        public object? Ref11 { get; set; }
        public object? Ref12 { get; set; }
        public object? Ref13 { get; set; }
        public object? Ref14 { get; set; }
        public object? Ref15 { get; set; }
        public object? Ref16 { get; set; }
        public object? Ref17 { get; set; }
        public object? Ref18 { get; set; }
        public object? Ref19 { get; set; }
        public object? Ref20 { get; set; }
        public object? Ref21 { get; set; }
        public object? Ref22 { get; set; }
        public object? Ref23 { get; set; }
        public object? Ref24 { get; set; }
        public object? Ref25 { get; set; }
        public object? Ref26 { get; set; }
        public object? Ref27 { get; set; }
        public object? Ref28 { get; set; }
        public object? Ref29 { get; set; }
        public object? Ref30 { get; set; }
        public object? Status { get; set; }
        public object? UploadBy { get; set; }
        public object? UploadDate { get; set; }
        public object? Item_Status { get; set; }
        public object? File_Status { get; set; }
        public object? Retrieval_Type { get; set; }
        public object? Retrieval_region { get; set; }
        public object? Req_Number { get; set; }
        public object? CourierName { get; set; }
        public object? Consignments_Number { get; set; }
    }

    public class SP_GetALLReffiling
    {
        public string? Ref_Number { get; set; }
        public string? UserName { get; set; }
        public string? Status { get; set; }
        public DateTime? Refilling_Closed_Date { get; set; }
        public DateTime? Dispatch_Date { get; set; } 
        public DateTime? Courier_ID { get; set; } 
        public string? Consignments_Number { get; set; }

    }
    public class GetAllParentsAck
    {
        public string Ref_Number { get; set; }
        public string Status { get; set; }
        public string Closed_Date { get; set; }
        public string Closed_by { get; set; }
        public string Ack_Date { get; set; }
        public string Ack_by { get; set; }
        public string Courier_Name { get; set; }
        public string Consignments_Number { get; set; }
    }

}
