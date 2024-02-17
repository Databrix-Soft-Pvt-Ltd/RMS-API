using System;
using System.Collections.Generic;
using System.Text;

namespace TwoWayCommunication.Model.ResponseModel
{
    public class ConversationResponseModel
    {
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserPhoneNumber { get; set; }
        public string SupportType { get; set; }
        public string ConversationStatus { get; set; }
        public DateTime ConverstationStartDate { get; set; }
        public string ConversationId { get; set; }
        public short ConversationStatusId { get; set; }
        public List<ConversationHistoryResponseModel> ConversationHistoryList { get; set; }
        public long? ToUserId { get; set; }
        public short SupportTypeId { get; set; }
        public long? FromUserId { get; set; }
    }
}
