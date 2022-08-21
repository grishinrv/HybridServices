using System.Net.Sockets;

namespace HybridServices.Bus
{
    /// <summary>
    /// State object for reading client data asynchronously.
    /// </summary>
    public class SocketReader
    {
        public const int BufferSize = 1024;
        /// <summary>
        /// Receive buffer.
        /// </summary>
        public byte[] Buffer { get; set; } = new byte[BufferSize];

        public Socket ClientSocket { get; set; }
    }
}