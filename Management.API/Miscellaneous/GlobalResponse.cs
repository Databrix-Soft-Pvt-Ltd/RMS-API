using System.Data;
using System.Text;

namespace Management.API.Miscellaneous
{
    public class GlobalResponse
    {
        public long Response_Code { get; set; }
        public string Response_Message { get; set; }
        public object ReponseData { get; set; }
    }
    public class GlobalError
    {
        public long ErrorCode { get; set; }
        public string Error_Message { get; set; }
        public string Error_Trace_Point { get; set; }
    }
    


}
