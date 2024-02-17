namespace TwoWayCommunication.Model.RequestModel
{
    public class ConversationRequestModel
    {
        public long UserId { get; set; }
        public short SupportTypeId { get; set; }
        public string Message { get; set; }
        public short SourceTypeId { get; set; }
        public string ConnectionId { get; set; }
    }
}
