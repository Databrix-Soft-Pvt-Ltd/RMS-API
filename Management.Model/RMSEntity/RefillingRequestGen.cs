using System;
using System.Collections.Generic;

namespace Management.Model.RMSEntity
{
    public partial class RefillingRequestGen
    {
        public int Id { get; set; }
        public string? RefNumber { get; set; }
        public int? IsCreatedBy { get; set; }
        public DateTime? IsCreatedDate { get; set; }
        public string? Status { get; set; }
        public int? IsClosedby { get; set; }
        public DateTime? IsClosedDate { get; set; }
    }
}
