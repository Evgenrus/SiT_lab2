using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using LABA2.Models;
using LABA2.Models.Statuses;
using NLog;

namespace LABA2.Controllers
{
    public class PopReciever : ServerConnector
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        
        public PopReciever(string host, int port) : base(host, port)
        {
        }

        public override void Auth(string login, string pass)
        {
            var res = SendCommand("USER " + login);
            Thread.Sleep(1000);
            res = SendCommand("PASS " + pass);
            Reader.ReadLine();
            //if (res.IsError())
            //{
            //    throw new NotImplementedException();
            //}

            IsAuth = IsConnected = true;
        }

        public int CountEmails()
        {
            if (!IsAuth || !IsConnected)
            {
                throw new NotImplementedException();
            }

            var res = SendCommand("STAT");
            if (res == null)
            {
                throw new NotImplementedException();
            }

            return Int32.Parse(res.Message.Split(' ')[0]);
        }

        public Email RecieveOne(int number)
        {
            SendCommand("RETR " + number.ToString());
            var stat = Reader.ReadLine();
            // if (/*!Reader.ReadLine()*/stat.StartsWith("+OK"))
            // {
            //     throw new NotImplementedException();
            // }
            var rawMail = new StringBuilder();
            string s;
            do
            {
                s = Reader.ReadLine();
                rawMail.Append(s + "\n");
                if (s == "." && Reader.Peek() == -1)
                    break;
            } while (true);

            s = rawMail.ToString();

            return new Email(s);
        }

        public IEnumerable<Email> RecieveAll()
        {
            throw new NotImplementedException();
        }

        private PopStatus SendCommand(string command)
        {
            Writer.WriteLine(command);
            string ans = Reader.ReadLine();
            _logger.Info(ans);
            Console.WriteLine(ans);
            try
            {
                return new PopStatus(ans);
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}