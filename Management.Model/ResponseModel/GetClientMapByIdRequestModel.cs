using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Model.ResponseModel
{
    public class GetClientMapByIdRequestModel
    {
        public long? UserID { get; set; }
        public long? ClientId { get; set; }
    }
       
}
