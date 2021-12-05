using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ConsoleApp1
{
    public class SocketNegotiator
    {
        public Socket socket;
        public string host;
        public int port;
        public IPEndPoint IpEndPoint;

        public SocketNegotiator(string host, int port)
        {
            //var ip = Dns.GetHostEntry("localhost").AddressList[0];
            IpEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void connect()
        {
            socket.Connect(IpEndPoint);
        }

        public string Recieve()
        {
            byte[] data = new byte[256];
            int recieved = socket.Receive(data);

            return Encoding.UTF8.GetString(data).Substring(0, recieved);;
        }

        public void Send(string msg)
        {
            byte[] data = new byte[256];

            data = Encoding.UTF8.GetBytes(msg);
            socket.Send(data);
        }

        public void Shutdown()
        {
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }
    }
}