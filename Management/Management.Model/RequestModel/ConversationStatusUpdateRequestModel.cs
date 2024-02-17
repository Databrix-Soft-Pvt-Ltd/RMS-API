using System;

namespace TwoWayCommunication.Model.RequestModel
{
    public class ConversationStatusUpdateRequestModel
    {
        public string ConversationId { get; set; }
        public short ConversationStatusId { get; set; }
        public long? ToUserId { get; set; }
    }
}
