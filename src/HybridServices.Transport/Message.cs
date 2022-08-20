using MessagePack;

namespace HybridServices.Transport
{
    [MessagePackObject]
    public class Message
    {
        [Key(0)]
        public Address From { get; set; }
        [Key(1)]
        public Address To { get; set; }
        [Key(2)]
        public byte[] SerializedDto { get; set; }
    }
}