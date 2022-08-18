using System;
using System.Threading.Tasks;
using MessagePack;

namespace HybridServices.Transport
{
    internal abstract class ClientProxyBase
    {
        internal ClientProxyBase()
        {
        }

        protected Task Serialize()
        {
            // MessagePackSerializer.SerializeAsync();
            throw new NotImplementedException();
        }
    }
}