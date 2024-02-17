using System;
using System.Collections.Generic;
using System.Text;

namespace TwoWayCommunication.Model.RequestModel
{
    public class ConversationSearchRequestModel
    {
        public string Text { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public bool? PastConversation { get; set; }
        public short? ConversationStatusId { get; set; }
        public short? SupportTypeId { get; set; }
    }
}
