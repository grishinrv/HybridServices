using MessagePack;

namespace HybridServices.Transport
{
    [MessagePackObject]
    public sealed class Address
    {
        [Key(0)]
        public string ServiceName { get; set; }
        [Key(1)]
        public string ModuleName { get; set; }
        [Key(2)]
        public string MethodName { get; set; }
    }
}