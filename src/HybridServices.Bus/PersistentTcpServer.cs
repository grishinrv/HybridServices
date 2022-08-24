using System;
using MessagePack;
using System.Collections.Generic;
using System.IO.Pipelines;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using HybridServices.Transport;
using HybridServices.Utils.Helpers;

namespace HybridServices.Bus
{
    /// <summary>
    /// Establishes and listens to persistent TCP connections.
    /// </summary>
    public class PersistentTcpServer
    {
        private volatile bool _stop;
        private readonly TcpListener _tcpListener;
        private readonly Action<string> _log;
        private readonly Action<Exception> _error;
        private readonly List<Task> _tcpListnerTasks = new List<Task>();
        private readonly ushort _maxListeners;

        private readonly ManualResetEvent _allDone = new ManualResetEvent(false);
        public bool IsRunning { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
        public PersistentTcpServer(string host, int port, Action<Exception> errorLog, ushort maxListeners = 0, Action<string> log = null)
        {
            _log = log ?? (s => {});
            ThrowHelper.CheckArgumentNull(host, nameof(host));
            ThrowHelper.CheckArgumentNull(host, nameof(errorLog));
            _maxListeners = maxListeners;
            _error = errorLog;
            _tcpListener = new TcpListener(IPAddress.Parse(host), port);
        }
        
        /// <summary>
        /// Stops running <see cref="PersistentTcpServer"/>
        /// </summary>
        public void Stop() =>_stop = true;

        /// <summary>
        /// Runs, until <see cref="Stop"/> is called.
        /// </summary>
        public void Run()
        {
            if (IsRunning)
                return;
            IsRunning = true;
            _stop = false;

            while (!_stop)
                MessageLoop();
        }

        private void MessageLoop()
        {
            while (_tcpListnerTasks.Count < _maxListeners)
            {
                Task processMessageTask  = Task.Run(async () =>
                    {
                        try
                        {
                            ProcessMessageFromClient(await _tcpListener.AcceptTcpClientAsync());
                        }
                        catch (Exception e)
                        {
                            _error(e);
                        }
                    });
                processMessageTask= _tcpListener.AcceptTcpClientAsync();
                _tcpListnerTasks.Add(processMessageTask);
            }

            int removeIndex = Task.WaitAny(_tcpListnerTasks.ToArray(), 500);
            if (removeIndex > -1)
                _tcpListnerTasks.RemoveAt(removeIndex);
        }

        private void ProcessMessageFromClient(TcpClient connection)
        {
            if (!connection.Connected)
                return;

            using (NetworkStream stream = connection.GetStream())
            {
                
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="port"></param>
        /// <param name="maxPendingConnections">The backlog parameter is limited to different values depending on the Operating System.
        /// You may specify a higher value, but the backlog will be limited based on the Operating System.
        /// </param>
        private void StartListening(int port, int maxPendingConnections = 64)
        {
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, port);

            Socket lictener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                lictener.Bind(localEndPoint);
                lictener.Listen(maxPendingConnections);

                while (true)
                {
                    // set event to non-signaled state.
                    _allDone.Reset();
                    
                    // Start an asynchronous socket to listen for connections
                    _log("Waiting for a connection...");
                    lictener.BeginAccept(AcceptCallback, lictener);
                    
                    // wait until a connection is made before continuing.
                    _allDone.WaitOne();
                }
            }
            catch (Exception e)
            {
                _error(e);
            }
        }

        private void AcceptCallback(IAsyncResult result)
        {
            // Signal connection acceptance thread to continue.
            _allDone.Set();
            
            // Get the socket that handles the client requests.
            Socket listener = (Socket)result.AsyncState;
            Socket handler = listener.EndAccept(result);

            SocketReader reader = new SocketReader()
            {
                ClientSocket = handler
            };
            handler.BeginReceive(reader.Buffer, 0, SocketReader.BufferSize, SocketFlags.None, ReadCallback, reader);
        }

        private void ReadCallback(IAsyncResult result)
        {
            
        }
        
        private Task ProcessMessageAsync(Socket socket)
        {
            Pipe pipe = new Pipe();
            Task writing = FillPipeAsync(socket, pipe.Writer);
            // Task reading = ReadPipeAsync(pipe.Reader);
            // return Task.WhenAll(writing, reading);
            return writing;
        }

        private async Task FillPipeAsync(Socket socket, PipeWriter writer)
        {
            const int minimumBufferSize = 512;
            while (true)
            {
#if NETSTANDARD2_1
                Memory<byte> memory = writer.GetMemory(minimumBufferSize); 
#else
                Span<byte> memory = writer.GetSpan(minimumBufferSize);
#endif
                try
                {
#if NETSTANDARD2_1                    
                    int bytesRead = await socket.ReceiveAsync(memory, SocketFlags.None);
#else
                    int bytesRead = await socket.ReceiveAsync(memory, SocketFlags.None);
#endif

                }
                catch (Exception e)
                {
                    _error(e);
                    break;
                }
            }
        }
    }
}