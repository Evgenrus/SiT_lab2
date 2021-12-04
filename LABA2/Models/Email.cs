using System;
using System.Text;

namespace LABA2.Models
{
    public class Email
    {
        public string MailFrom { get; set; }
        public string RcptTo { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public Email(string mailFrom, string rcptTo, 
            string from, string to, string subject, string body)
        {
            MailFrom = mailFrom;
            RcptTo = rcptTo;
            From = from;
            To = to;
            Subject = subject;
            Body = body;
        }

        public Email(Email sample)
        {
            MailFrom = sample.MailFrom;
            RcptTo = sample.RcptTo;
            From = sample.From;
            To = sample.To;
            Subject = sample.Subject;
            Body = sample.Body;
        }

        public Email(string rawMail)
        {
            //TODO REFACTOR
            var strings = rawMail.Split("\n");
            int i = 0;
            while (i < strings.Length-1)
            {
                if (strings[i].Contains("Return"))
                {
                    MailFrom = strings[i].Substring(strings[i].IndexOf("439-4@inbox.ru"), strings[i].Length-strings[i].IndexOf("439-4@inbox.ru"));
                }

                if (strings[i].Contains("From"))
                {
                    From = strings[i].Substring(strings[i].IndexOf("439-4@inbox.ru"), strings[i].Length-strings[i].IndexOf("439-4@inbox.ru"));
                }

                if (strings[i].Contains("To"))
                {
                    To = strings[i].Substring(strings[i].IndexOf("439-4@inbox.ru"), strings[i].Length-strings[i].IndexOf("439-4@inbox.ru"));
                    RcptTo = To;
                }

                if (strings[i].Contains("Subject"))
                {
                    var temp = strings[i].Split(' ');
                    if (temp.Length > 1)
                    {
                        Subject = temp[1];
                    }
                    else
                    {
                        Subject = "";
                    }
                }

                if (strings[i] == "")
                {
                    var sb = new StringBuilder();
                    while (i <= strings.Length-3)
                    {
                        sb.Append(strings[i] + "\n");
                        i++;
                    }

                    Body = sb.ToString();
                }

                i++;
            }
        }

        public override string ToString()
        {
            StringBuilder rawMail = new StringBuilder();
            rawMail.Append(MailFrom + "\n");
            rawMail.Append(RcptTo + "\n");
            rawMail.Append(From + "\n");
            rawMail.Append(To + "\n");
            rawMail.Append(Subject + "\n");
            rawMail.Append(Body + "\n");
            return rawMail.ToString();
        }
    }
}