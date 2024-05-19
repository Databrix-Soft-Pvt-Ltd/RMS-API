using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Model.ResponseModel
{
    public class GetAllClientMappingResponse
    {
        public long? Id { get; set; }

        public long? ClientId { get; set; }

        public long? userId { get; set; }
        public string? ClinetName { get; set; }
        public string? UserName { get; set; } 
    }
}
