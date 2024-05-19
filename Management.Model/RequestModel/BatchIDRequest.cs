using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Model.RequestModel
{
    public class BatchIDRequest
    {
        public string batch_id { get; set; }
        public int UserID { get; set; }
    }

    public class BatchIDDetails
    {
        public string batch_id { get; set; }
        public int UserID { get; set; }
    }

}
