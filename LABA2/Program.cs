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
            popReciever.Auth("439-4sit@mail.ru", "j7vrB8FGWzRAeSpa800D");
            int mailsnum = popReciever.CountEmails();
            var mail = popReciever.RecieveOne(mailsnum);
            
            
            SmtpSender sender = new SmtpSender("smtp.mail.ru", 465);
            sender.Connect();
            sender.Auth("439-4sit@mail.ru","j7vrB8FGWzRAeSpa800D");
            sender.SendMail(mail);
        }
    }
}