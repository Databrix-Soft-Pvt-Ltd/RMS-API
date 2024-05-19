using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Model.RequestModel
{
    public class UpdateTemplateRequest
    {
        public long Id { get; set; }
        public String TemplateName { get; set; }
        public String TemplateDescription { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public int ModifieldBy { get; set; }
        public GetAllTemplateResponse[] TemplateDetails { get; set; }
    }
}
