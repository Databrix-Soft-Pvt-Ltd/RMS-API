using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Model.RequestModel
{
    public class AddRetrivel_RequestRequestModel
    {
        public int Id { get; set; }
        public string? ReqNumber { get; set; }
        public int? IsCreatedBy { get; set; }
        public DateTime? IsCreatedDate { get; set; }
        public string? Status { get; set; }
        public int? IsClosedby { get; set; }
        public DateTime? IsClosedDate { get; set; }
    }
}
