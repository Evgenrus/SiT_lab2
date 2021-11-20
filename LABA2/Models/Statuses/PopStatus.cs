using System;
using System.Text.RegularExpressions;

namespace LABA2.Models.Statuses
{
    public class PopStatus : Status
    {
        public PopStatus(string answer)
        {
            if (!Regex.IsMatch(answer, @"^[+-]\w*") && !Regex.IsMatch(answer, @"\d*.*"))
            {
                throw new ArgumentException("Cannot parse answer");
            }

            var index = answer.IndexOf(" ");
            Code = answer.Substring(0, index).Trim();
            Message = answer.Substring(index).Trim();
        }
        
        public override bool IsError()
        {
            if (Code.StartsWith("-"))
            {
                return true;
            }

            return false;
        }
    }
}