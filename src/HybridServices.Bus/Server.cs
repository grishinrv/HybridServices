using System.Net;
using System.Net.Sockets;
using HybridServices.Utils.Helpers;

namespace HybridServices.Bus
{
    public class Server
    {
        private volatile bool _stop;
        private readonly TcpListener _tcpListener;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
        public Server(string host, int port)
        {
            ThrowHelper.CheckArgumentNull(host, nameof(host));
            _tcpListener = new TcpListener(IPAddress.Parse(host), port);
        }
        
        public void Stop() =>_stop = true;

    }
}