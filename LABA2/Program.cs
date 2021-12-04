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
            PopReciever popReciever = new PopReciever("pop.mail.ru", 995);
            popReciever.Auth("439-4@inbox.ru", "JtC8RKzMbpPhh3mbAEY4");
            int mailsnum = popReciever.CountEmails();
            var mail = popReciever.RecieveOne(mailsnum);

            var newMail = new Email("439-4@inbox.ru", "439-4@inbox.ru", "439-4@inbox.ru", "439-4@inbox.ru",
                "FGHH", "KEK");
            
            SmtpSender sender = new SmtpSender("smtp.mail.ru", 465);
            sender.Connect();
            sender.Auth("439-4@inbox.ru","JtC8RKzMbpPhh3mbAEY4");
            sender.SendMail(newMail);
        }
    }
}