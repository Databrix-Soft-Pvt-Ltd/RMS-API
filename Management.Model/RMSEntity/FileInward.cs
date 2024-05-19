using System;
using System.Collections.Generic;

namespace Management.Model.RMSEntity
{
    public partial class FileInward
    {
        public int Id { get; set; }
        public string? BatchId { get; set; }
        public string? Ref1 { get; set; }
        public string? DocumentType { get; set; }
        public int? BranchInwardBy { get; set; }
        public DateTime? BranchInwardDate { get; set; }
        public string? FileNo { get; set; }
        public string? CartonNo { get; set; }
        public string? FileStatus { get; set; }
        public string? Status { get; set; }
        public int? FileAckBy { get; set; }
        public DateTime? FileAckDate { get; set; }
    }
}
