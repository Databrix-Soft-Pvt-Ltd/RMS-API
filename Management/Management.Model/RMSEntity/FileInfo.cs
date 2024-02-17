using System;
using System.Collections.Generic;

namespace Management.Model.RMSEntity
{
    public partial class FileInfo
    {
        public long Id { get; set; }
        public string? UniqueId { get; set; }
        public long? DumpId { get; set; }
        public string? RequestId { get; set; }
        public string? FileStatus { get; set; }
        public string? Status { get; set; }
        public long? RequestBy { get; set; }
        public DateTime? RequestDate { get; set; }
        public string? DispatchAddress { get; set; }
        public long? ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public long? RejectBy { get; set; }
        public DateTime? RejectDate { get; set; }
        public string? PodNumber { get; set; }
        public DateTime? DispatchDate { get; set; }
        public long? CourierId { get; set; }
        public string? NewPodNumber { get; set; }
        public long? PodAckBy { get; set; }
        public DateTime? PodAckDate { get; set; }
        public long? RefillingBy { get; set; }
        public DateTime? RefillingDate { get; set; }
        public string? RefillingRequestNumber { get; set; }
        public long? RefillingAckBy { get; set; }
        public DateTime? RefillingAckDate { get; set; }
        public string? RefillingCartonNumber { get; set; }
    }
}
