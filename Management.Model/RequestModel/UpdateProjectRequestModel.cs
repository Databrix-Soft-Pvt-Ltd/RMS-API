using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Model.RequestModel
{
    public class UpdateProjectRequestModel
    {
        public long Id { get; set; }

        public string? ProjectName { get; set; }
        public string? LoginPageLogo { get; set; }
        public string? HeaderPageLogo { get; set; }

        public bool? IsActive { get; set; }
    }
}
