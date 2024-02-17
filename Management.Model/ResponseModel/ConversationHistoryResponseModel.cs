using System;
using System.Collections.Generic;
using System.Text;

namespace TwoWayCommunication.Model.ResponseModel
{
    public class ConversationHistoryResponseModel
    {
        public string Message { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UserName { get; set; }
        public string Source { get; set; }
        public string ConversationHistoryId { get; set; }
        public int MessageTypeId { get; set; }
        public int DocumentTypeId { get; set; }
        public string FilePath { get; set; }
        public long CreatedBy { get; set; }
    }
}
