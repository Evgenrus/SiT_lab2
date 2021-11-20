using System;
using System.Text;
using System.Threading;
using LABA2.Models;
using LABA2.Models.Statuses;
using NLog;

namespace LABA2.Controllers
{
    public class SmtpSender : ServerConnector
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        
        public SmtpSender(string host, int port) : base(host, port)
        {
        }

        public void Connect()
        {
            var res = SendCommand("EHLO " + Host);
            if (res.IsError())
            {
                throw new NotImplementedException();
            }
            IsConnected = true;
        }

        //"yYhUEbQJWuwY8pJPcBj1"
        public override void Auth(string login, string pass)
        {
            var res = SendCommand("AUTH LOGIN");
            res = SendCommand(Convert.ToBase64String(Encoding.UTF8.GetBytes(login)));
            res = SendCommand(Convert.ToBase64String(Encoding.UTF8.GetBytes(pass)));
            if (res.IsError())
            {
                throw new NotImplementedException();
            }
            IsAuth = true;
        }

        public void SendMail(Email mail)
        {
            if (!IsAuth)
                throw new NotImplementedException();
            if (!IsConnected)
                throw new NotImplementedException();
            var res = SendCommand("MAIL FROM: " + mail.MailFrom);
            res = SendCommand("RCPT TO: " + mail.RcptTo);
            res = SendCommand("DATA");
            res = SendCommand("From: " + mail.From);
            res = SendCommand("To: " + mail.To);
            res = SendCommand("Subject: " + mail.Subject);
            res = SendCommand(mail.Body);
            res = SendCommand("\n.\n");
            if (res.IsError())
            {
                throw new NotImplementedException();
            }
            SendCommand("QUIT");
        }

        private SmtpStatus SendCommand(string command)
        {
            Writer.WriteLine(command);
            string ans = Reader.ReadLine();
            _logger.Info(ans);
            Console.WriteLine(ans);
            //Thread.Sleep(1000);
            try
            {
                return new SmtpStatus(ans);
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}