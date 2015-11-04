using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Word2Vec.Net;
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
                            wholeDialog.Last().Append(recbuffer);
                        }
                        else
                        {
                            wholeDialog.Add(record);
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

            var speaker1Role = wholeDialog.Where(x => x.Sender == speaker1).ToList();
            var speaker2Role = wholeDialog.Where(x => x.Sender == speaker2).ToList();
            wholeDialog.RemoveAll(x => x.Sender != speaker1 && x.Sender != speaker2);
            //TODO: понять, почему некоторые сообщения имеют не правильного автора
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
