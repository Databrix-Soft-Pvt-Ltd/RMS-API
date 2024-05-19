using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Model.RequestModel
{
    public class UpdateClientRequestModel
    {
        public long Id { get; set; }
        public string? ClientName { get; set; }
        public string? ClientCode { get; set; }
        public bool? IsActive { get; set; }

    }
}
