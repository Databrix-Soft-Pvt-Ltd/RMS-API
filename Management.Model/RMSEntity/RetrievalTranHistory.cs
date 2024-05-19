using System;
using System.Collections.Generic;

namespace Management.Model.RMSEntity
{
    public partial class RetrievalTranHistory
    {
        public int Id { get; set; }
        public string? ReqNumber { get; set; }
        public string? Ref1 { get; set; }
        public string? Type { get; set; }
        public string? FileStatus { get; set; }
        public string? Status { get; set; }
        public DateTime? ProcessDate { get; set; }
        public int? ProcessBy { get; set; }
        public string? Remarks { get; set; }
    }
}
