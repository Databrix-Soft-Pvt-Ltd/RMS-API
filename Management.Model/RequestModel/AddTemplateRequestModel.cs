using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Model.RequestModel
{
    public class AddTemplateRequestModel
    {

        public string? DisplayName { get; set; }
        public string? DatabaseName { get; set; }
        public string? DataType { get; set; }
        public int? MaxLength { get; set; }
        public bool? IsMandatory { get; set; }
        public bool? IsUnique { get; set; }
        public string? MasterValue { get; set; }
        public string? ControlType { get; set; }
        public string? DateFormat { get; set; }
        public string? Validation { get; set; }
    }
}
