using System.Net;
using System.Net.Sockets;
using System.Text;

public class SocketNegotiator
{
    public Socket socket;
    public string host;
    public int port;
    public IPEndPoint IpEndPoint;
    public Socket listener;

    public SocketNegotiator(string host, int port)
    {
        IpEndPoint = new IPEndPoint(IPAddress.Parse(host), port);
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    }

    private void listen()
    {
        socket.Bind(IpEndPoint);
        socket.Listen(10);
        listener = socket.Accept();
    }

    public string Recieve()
    {
        byte[] data = new byte[256];

        this.listen();
        int recieved = listener.Receive(data);

        return Encoding.UTF8.GetString(data);
    }

    public void Send(string msg)
    {
        byte[] data = new byte[256];

        data = Encoding.UTF8.GetBytes(msg);
        listener.Send(data);
    }

    public void Shutdown()
    {
            listener.Shutdown(SocketShutdown.Both);
            listener.Close();
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
    }
}