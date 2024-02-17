using Microsoft.AspNetCore.Http;
using System;

namespace TwoWayCommunication.Model.RequestModel
{
    public class ConversationHistoryRequestModel
    {
        public Guid ConversationId { get; set; }
        public string Message { get; set; }
        public short SourceTypeId { get; set; }
        public long CreatedBy { get; set; } // TODO ::
        public long? ToUserId { get; set; } // TODO ::
    }

    public class ConversationDocumentRequestModel
    {
        public string ConversationId { get; set; }
        public IFormFile Files { get; set; }
        public short SourceTypeId { get; set; }
        public long CreatedBy { get; set; } // TODO ::
        public long? ToUserId { get; set; } // TODO ::
    }
}
