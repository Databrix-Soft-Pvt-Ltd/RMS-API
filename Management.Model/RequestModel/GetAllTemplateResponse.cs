using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Model.RequestModel
{
    public class GetTemplateName
    { 
        public String TemplateName { get; set; }
        public String TemplateDescription { get; set; } 
        public bool IsActive { get; set; } 
        public int CreatedBy { get; set; } 
        public int ModifieldBy { get; set; }
        public GetAllTemplateResponse[] TemplateDetails { get; set; }
    }
    public class GetAllTemplateResponse
    { 
        public long Id { get; set; }
        public int? TempID { get; set; } 
        public string? DisplayName { get; set; }
        public string? TempName { get; set; }
        public string? TempDescription { get; set; }
        public string? DatabaseName { get; set; }
        public string? DataType { get; set; }
        public int? MaxLength { get; set; }
        public bool? IsMandatory { get; set; }
        public bool? IsUnique { get; set; }
        public string? MasterValue { get; set; }
        public string? ControlType { get; set; }
        public string? DateFormat { get; set; }
        public string? Validation { get; set; }
        public bool IsActive { get; set; }
    }
}
