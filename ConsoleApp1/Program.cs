using System;
using System.Threading;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            SocketNegotiator negotiator = new SocketNegotiator("127.0.0.1", 9000);
            negotiator.connect();
            Console.WriteLine("Connected");
            Thread.Sleep(1500);
            negotiator.Send("Recieve");
            Console.WriteLine("Send 1");
            var msg = negotiator.Recieve();
            Thread.Sleep(10000);
            negotiator.Send("Send MSG");
            msg = negotiator.Recieve();
            Thread.Sleep(5000);
            negotiator.Send("Quit App");
            msg = negotiator.Recieve();
            Thread.Sleep(5000);
        }
    }
}