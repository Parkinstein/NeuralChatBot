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

        public const string IgnoreRegularTemplate = @"\d\d\.\d\d\.\d\d";

        public DialogRecord(string sender, List<string> message)
        {
            Sender = sender;

            StringBuilder sb = new StringBuilder();
            foreach(var s in message)
            {
                var s1 = s.Replace("\n", "").Replace(",","").Replace("\t", "");
                if (!Regex.IsMatch(s1.Trim(), IgnoreRegularTemplate) &&!string.IsNullOrWhiteSpace(s1))
                {
                    sb.Append(s1);
                }               
            }
            Message = sb.ToString();
        }
        
        public void Append(List<string> message)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var s in message)
            {
                var s1 = s.Replace("\n", "");
                if (!Regex.IsMatch(s1.Trim(), IgnoreRegularTemplate) && !string.IsNullOrWhiteSpace(s1))
                {
                    sb.Append(s1);
                }
            }
            Message += sb.ToString();
        }
    }
}
