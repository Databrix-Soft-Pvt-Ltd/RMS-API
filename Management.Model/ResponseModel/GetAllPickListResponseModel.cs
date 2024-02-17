using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Model.ResponseModel
{
    public class GetAllPickListResponseModel
    {
        public long Id { get; set; }
        public string? LogoPath { get; set; }
        public long? TemplateId { get; set; }
        public string? Address { get; set; }
    }
}
