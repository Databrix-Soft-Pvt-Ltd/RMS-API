using System;
using System.Collections.Generic;

namespace Management.Model.RMSEntity
{
    public partial class RefillingTransaction
    {
        public int Id { get; set; }
        public string? RefNumber { get; set; }
        public string? Ref1 { get; set; }
        public int? RetrievalId { get; set; }
        public int? FileStatus { get; set; }
        public string? Status { get; set; }
        public string? Remarks { get; set; }
        public string? ItemSatus { get; set; }
        public DateTime? RefillingDate { get; set; }
        public int? RefillingBy { get; set; }
        public DateTime? RefillingClosedDate { get; set; }
        public int? RefillingClosedBy { get; set; }
        public int? RefillingAckBy { get; set; }
        public DateTime? RefillingAckDate { get; set; }
        public DateTime? PickupDate { get; set; }
        public DateTime? RefillingReceivedDate { get; set; }
        public int? RefillingReceivedBy { get; set; }
        public DateTime? DispatchDate { get; set; }
        public string? DispatchAddress { get; set; }
        public int? CourierId { get; set; }
        public string? ConsignmentsNumber { get; set; }
    }
}
