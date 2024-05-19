using System;
using System.Collections.Generic;

namespace Management.Model.RMSEntity
{
    public partial class RetrievalTransaction
    {
        public int Id { get; set; }
        public string? ReqNumber { get; set; }
        public string? Ref1 { get; set; }
        public string? RetrievalType { get; set; }
        public string? RetrievalRegion { get; set; }
        public int? FileStatus { get; set; }
        public string? Status { get; set; }
        public string? ItemStatus { get; set; }
        public DateTime? RequestDate { get; set; }
        public int? RequestBy { get; set; }
        public string? Remarks { get; set; }
        public int? ClosedBy { get; set; }
        public DateTime? ClosedDateTime { get; set; }
        public int? ApprovalBy { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public int? RejectBy { get; set; }
        public DateTime? RejectDate { get; set; }
        public DateTime? DispatchDate { get; set; }
        public int? DispatchBy { get; set; }
        public int? CourierId { get; set; }
        public string? ConsignmentsNumber { get; set; }
        public DateTime? CourierAckDate { get; set; }
        public int? CourierAckBy { get; set; }
    }
}
