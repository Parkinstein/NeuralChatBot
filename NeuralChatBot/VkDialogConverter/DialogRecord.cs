using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace VkDialogConverter
{
    public class DialogRecord
    {
        public DialogRecord RelatedRecord { get; set; }
        public string Sender { get; set; }
        public string Message { get; set; }
        public List<string> Words
        {
            get
            {
                return Message.Split(' ').Where(x=>!string.IsNullOrEmpty(x)&&!string.IsNullOrWhiteSpace(x)).ToList();
            }
        }

        private const string IgnoreRegularPattern = @"\d\d\.\d\d\.\d\d|\d*:\d\d|Forwarded Messages|views|Wall post by a community";
        

        public DialogRecord(List<string> message)
        {
            if (message.Count > 1)
            {
                if (!string.IsNullOrEmpty(message[0]) && !string.IsNullOrWhiteSpace(message[0]))
                {
                    Sender = message[0];
                }
                StringBuilder sb = new StringBuilder();
                for (int i = 1; i < message.Count; ++i)
                {
                    var s1 = Regex.Replace(message[i].Replace("\n", "").Replace(",", "").Replace("\t", ""),IgnoreRegularPattern, String.Empty);
                    if (ValidateString(s1))
                    {
                        sb.Append(s1+" ");
                    }
                }
                Message = sb.ToString();
            }                   
        }

        public void Append(List<string> message)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < message.Count; ++i)
            {
                var s1 = Regex.Replace(message[i].Replace("\n", "").Replace(",", "").Replace("\t", ""), IgnoreRegularPattern, String.Empty);
                if (ValidateString(s1))
                {
                    sb.Append(s1 + " ");
                }
            }
            Message += sb.ToString();
            
               
        }
        public void Append(DialogRecord other)
        {
            if (other.Sender != this.Sender)
            {
                return;
            }
            this.Message += other.Message;
        }

        private bool ValidateString(string input)
        {
          
            return (!Regex.IsMatch(input.Trim(), IgnoreRegularPattern) 
                && !string.IsNullOrWhiteSpace(input));
        }
    }
}
