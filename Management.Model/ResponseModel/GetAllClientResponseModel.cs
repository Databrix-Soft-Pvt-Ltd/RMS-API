using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Model.ResponseModel
{
    public class GetAllClientResponseModel
    {
        public long Id { get; set; }
        public string? ClientName { get; set; }
        public string? ClientCode { get; set; }
    }
}
