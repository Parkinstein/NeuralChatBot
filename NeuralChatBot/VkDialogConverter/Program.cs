using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace VkDialogConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            string speaker1 = "Roman";
            string speaker2 = "Vano";
            var buffer = new List<string>();
            using(StreamReader streamReader = new StreamReader(@"C:\TrainingSet\Вано.txt"))
            {           
                while (!streamReader.EndOfStream)
                {
                    buffer.Add(streamReader.ReadLine()); 
                }
            }
            
            List<DialogRecord> wholeDialog = new List<DialogRecord>();

            List<string> recbuffer = new List<string>();
            foreach(var s in buffer)
            {
                if (string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s))
                {
                    if (recbuffer.Count > 1)
                    {
                        var record = new DialogRecord(recbuffer);
                        if (wholeDialog.Count > 0 && record.Sender == wholeDialog.Last().Sender)
                        {
                            wholeDialog.Last().Message += " ";   
                            wholeDialog.Last().Append(record);
                        }
                        else
                        {
                            wholeDialog.Add(record);
                        }

                        if(wholeDialog.Count > 0 && record.Sender!= speaker1 && record.Sender != speaker2)
                        {
                            wholeDialog.Last().Message += " ";
                            wholeDialog.Last().Append(recbuffer);
                            wholeDialog.Remove(record);
                        }                    
                    }
                    recbuffer = new List<string>();                 
                }
                else
                {
                    recbuffer.Add(s);
                }           
            }

            var record1 = new DialogRecord(recbuffer);
            if (wholeDialog.Count > 0 && record1.Sender == wholeDialog.Last().Sender)
            {
                wholeDialog.Last().Message += " ";
                wholeDialog.Last().Append(recbuffer);
            }
            else
            {
                wholeDialog.Add(record1);
            }
            List<string> AllWords = new List<string>();
            wholeDialog.ForEach(x => AllWords.AddRange(x.Words));
            AllWords = AllWords.Distinct().ToList();
            var speaker1Role = wholeDialog.Where(x => x.Sender == speaker1).ToList();
            var speaker2Role = wholeDialog.Where(x => x.Sender == speaker2).ToList();
            wholeDialog.RemoveAll(x => x.Sender == speaker1 || x.Sender == speaker2);

            for (int i = 0; i < speaker2Role.Count; ++i)
            {
                if (i < speaker1Role.Count)
                {
                    speaker2Role[i].RelatedRecord = speaker1Role[i];
                }
            }
             
            Console.ReadKey();
        }
    }
}
