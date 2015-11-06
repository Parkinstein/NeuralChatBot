using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace VkDialogConverter
{
    class DialogRecord
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

        private const string IgnoreRegularTemplate = @"\d\d\.\d\d\.\d\d";

        public DialogRecord(List<string> message)
        {
            if (message.Count > 1)
            {
                if (!Regex.IsMatch(message[0].Trim(), IgnoreRegularTemplate) && !string.IsNullOrEmpty(message[0]) && !string.IsNullOrWhiteSpace(message[0]))
                {
                    Sender = message[0];
                }
                StringBuilder sb = new StringBuilder();
                for (int i = 1; i < message.Count; ++i)
                {
                    var s1 = message[i].Replace("\n", "").Replace(",", "").Replace("\t", "");
                    if (!Regex.IsMatch(s1.Trim(), IgnoreRegularTemplate) && !string.IsNullOrWhiteSpace(s1)&&!s1.Contains("Forwarded messages"))
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
                var s1 = message[i].Replace("\n", "").Replace(",", "").Replace("\t", "");
                if (!Regex.IsMatch(s1.Trim(), IgnoreRegularTemplate) && !string.IsNullOrWhiteSpace(s1) && !s1.Contains("Forwarded messages"))
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
    }
}
