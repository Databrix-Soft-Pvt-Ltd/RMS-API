using System;
using System.Collections.Generic;

namespace Management.Model.RMSEntity
{
    public partial class DocumentInward
    {
        public int Id { get; set; }
        public int? FileInwardId { get; set; }
        public int? CheckListId1 { get; set; }
        public int? CheckListId2 { get; set; }
        public int? CheckListId3 { get; set; }
        public string? DocumentStatus { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ScrutinyBy { get; set; }
        public DateTime? ScrutinyDate { get; set; }
    }
}
