using System;
using System.IO;
using System.Net.Security;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using LABA2.Controllers;
using LABA2.Models;
using NLog;

namespace LABA2
{
    class Program
    {
        public static Logger logger = LogManager.GetCurrentClassLogger();
        
        static void Main(string[] args)
        {
            SocketNegotiator negotiator = new SocketNegotiator("127.0.0.1", 9000);
            negotiator.listen();

            while (true)
            {
                string msg = "";
                msg = negotiator.Recieve().Trim();
                if (msg == "Recieve")
                {
                    PopReciever popReciever = new PopReciever("pop.mail.ru", 995);
                    popReciever.Auth("439-4@inbox.ru", "JtC8RKzMbpPhh3mbAEY4");
                    int mailsnum = popReciever.CountEmails();
                    var mail = popReciever.RecieveOne(mailsnum);
                    negotiator.Send("1");
                }

                var newMail = new Email("439-4@inbox.ru", "439-4@inbox.ru", "439-4@inbox.ru", "439-4@inbox.ru",
                    "FGHH", "KEK");
                
                if (msg == "Send MSG")
                {
                    SmtpSender sender = new SmtpSender("smtp.mail.ru", 465);
                    sender.Connect();
                    sender.Auth("439-4@inbox.ru", "JtC8RKzMbpPhh3mbAEY4");
                    sender.SendMail(newMail);
                    negotiator.Send("2");
                }

                if (msg == "Quit App")
                {
                    negotiator.Send("0");
                    break;
                }
            }

            Console.WriteLine("End");
        }
    }
}