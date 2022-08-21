using System.Net;
using System.Net.Sockets;

namespace HybridServices.Transport
{
    //https://www.codeproject.com/Articles/5270779/High-Performance-TCP-Client-Server-using-TCPListen
    public class TcpTunnel
    {
        private Socket ConnectSocket(string server, int port)
        {
            Socket s = null;
            IPHostEntry hostEntry = Dns.GetHostEntry(server);;

            // Loop through the AddressList to obtain the supported AddressFamily. This is to avoid
            // an exception that occurs when the host IP Address is not compatible with the address family
            // (typical in the IPv6 case).
            foreach(IPAddress address in hostEntry.AddressList)
            {
                IPEndPoint ipe = new IPEndPoint(address, port);
                Socket tempSocket = new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                tempSocket.Connect(ipe);

                if(tempSocket.Connected)
                {
                    s = tempSocket;
                    break;
                }
            }
            return s;
        }
    }
}