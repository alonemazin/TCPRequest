using System;
using System.Net.Sockets;
using System.ComponentModel;

namespace SocketProxy.Proxy
{
    /// <summary>
    /// Event arguments class for the EncryptAsyncCompleted event.
    /// </summary>
    public class CreateConnectionAsyncCompletedEventArgs : AsyncCompletedEventArgs
    {
        private TcpClient _proxyConnection;
        
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="error">Exception information generated by the event.</param>
        /// <param name="cancelled">Cancelled event flag.  This flag is set to true if the event was cancelled.</param>
        /// <param name="proxyConnection">Proxy Connection.  The initialized and open TcpClient proxy connection.</param>
        public CreateConnectionAsyncCompletedEventArgs(Exception error, bool cancelled, TcpClient proxyConnection)
            : base(error, cancelled, null)
        {
            _proxyConnection = proxyConnection;
        }

        /// <summary>
        /// The proxy connection.
        /// </summary>
        public TcpClient ProxyConnection
        {
            get { return _proxyConnection; }
        }
    }

} 
