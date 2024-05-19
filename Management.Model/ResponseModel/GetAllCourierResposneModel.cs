using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Model.ResponseModel
{
    public class GetAllCourierResposneModel
    {
        public long Id { get; set; }
        public string? CourierName { get; set; }
        public bool? IsActive { get; set; }
    }
}
