using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace AuxiliaryApp
{
    class Program
    {
        static int port = 8006;
        
        static void Main(string[] args)
        {
            IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);

            Socket listenSocket = new Socket(AddressFamily.InterNetwork
                , SocketType.Stream, ProtocolType.Tcp);

            listenSocket.Bind(endpoint);

            listenSocket.Listen(5);
            Console.WriteLine("Waiting for connections");

            byte[] data = new byte[256];
            Socket sock = listenSocket.Accept();

            string message = "recieve";
            sock.Send(Encoding.UTF8.GetBytes(message));

            do
            {
                sock.Receive(data);
                message = Encoding.UTF8.GetString(data);
                Console.WriteLine(message);
            } while (listenSocket.Available > 0);
                
            message = "send";
            sock.Send(Encoding.UTF8.GetBytes(message));
            
            do
            {
                sock.Receive(data);
                message = Encoding.UTF8.GetString(data);
                Console.WriteLine(message);
            } while (listenSocket.Available > 0);

            sock.Shutdown(SocketShutdown.Both);
            listenSocket.Shutdown(SocketShutdown.Both);
            sock.Close();
            listenSocket.Close();
        }
    }
}