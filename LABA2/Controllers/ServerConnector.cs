using System.IO;
using System.Net.Security;
using System.Net.Sockets;
using System.Threading;

namespace LABA2.Controllers
{
    public abstract class ServerConnector
    {
        protected readonly int Port;
        protected readonly string Host;
        protected readonly SslStream SafeStream;
        protected StreamReader Reader;
        protected StreamWriter Writer;
        protected TcpClient Client;
        protected Stream Stream;
        protected bool IsAuth = false;
        protected bool IsConnected = false;

        public ServerConnector(string host, int port)
        {
            Host = host;
            Port = port;
            Client = new TcpClient(Host, Port);
            Stream = Client.GetStream();
            SafeStream = new SslStream(Stream);
            SafeStream.AuthenticateAsClient(Host);
            Writer = new StreamWriter(SafeStream);
            Reader = new StreamReader(SafeStream);
            Writer.AutoFlush = true;
        }

        public abstract void Auth(string login, string pass);
    }
}