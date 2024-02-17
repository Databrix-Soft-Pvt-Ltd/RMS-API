namespace Management.API
{
    public class GlobalResponse
    {
        public int Response_Code { get; set; }
        public string Response_Message { get; set; }
        public object ReponseData { get; set; }
    }
    public class GlobalError
    {
        public int ErrorCode { get; set; }
        public string Error_Message { get; set; }
        public string Error_Trace_Point { get; set; }
    }
}
