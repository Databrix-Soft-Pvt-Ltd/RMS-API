using System;
using System.Collections.Generic;

namespace Management.Model.RMSEntity
{
    public partial class ClientMaster
    {
        public long Id { get; set; }
        public string? ClientName { get; set; }
        public string? ClientCode { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? IsActive { get; set; }
        public string? Address { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
