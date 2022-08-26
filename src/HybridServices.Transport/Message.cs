using System;
using MessagePack;

namespace HybridServices.Transport
{
    [MessagePackObject]
    public class Message
    {
        [Key(0)]
        public Guid TrackId { get; set; }
        [Key(1)]
        public Address From { get; set; }
        [Key(2)]
        public Address To { get; set; }
        [Key(3)]
        public bool AnswerNeeded { get; set; }
        [Key(4)]
        public byte[] SerializedDto { get; set; }
    }
}