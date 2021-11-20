using System;
using System.Text.RegularExpressions;

namespace LABA2.Models.Statuses
{
    public class SmtpStatus : Status
    {
        public SmtpStatus(string answer)
        {
            if (!Regex.IsMatch(answer, @"^\d\d\d\D\w*"))
            {
                throw new ArgumentException("Cannot parse answer");
            }
            Code = answer.Substring(0, 3).Trim();
            Message = answer.Substring(3).Trim();
        }

        public override bool IsError()
        {
            if (Code.StartsWith("2") || Code.StartsWith("3"))
            {
                return false;
            }
            
            return true;
        }
    }
}