using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using HybridServices.Transport;
using HybridServices.Utils.Helpers;
using MessagePack;

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

    }
}