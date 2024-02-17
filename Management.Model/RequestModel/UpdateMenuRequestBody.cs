using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Model.RequestModel
{
    public class UpdateMenuRequestBody
    {
        public long Id { get; set; }
        public string? MainMenu { get; set; }
        public string? SubMenu { get; set; }
    }
}
