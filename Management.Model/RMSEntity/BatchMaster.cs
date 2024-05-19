using System;
using System.Collections.Generic;

namespace Management.Model.RMSEntity
{
    public partial class BatchMaster
    {
        public int Id { get; set; }
        public string? BatchId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CourierId { get; set; }
        public string? ConsignmentNumber { get; set; }
        public int? BatchCloseBy { get; set; }
        public DateTime? BatchCloseDate { get; set; }
        public string? Status { get; set; }
        public string? FilePath { get; set; }
        public int? CourierAckBy { get; set; }
        public DateTime? CourierAckDate { get; set; }
        public string? CourierRemark { get; set; }
        public string? RequestStatus { get; set; }
        public string? OldBatchId { get; set; }
    }
}
