using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace VkDialogConverter
{
    class DialogService
    {
        public List<DialogRecord> Records { get; set; }

        public void SaveToFile(string path)
        {
            StringBuilder sb = new StringBuilder();
            foreach(var rec in Records)
            {
                sb.AppendLine(rec.Message);
            }
            using (StreamWriter outfile = new StreamWriter(path, true))
            {
                 outfile.Write(sb.ToString());
            }
        }

        public static List<DialogRecord> GetRecordsFromFile(string path, string speaker1, string speaker2)
        {
            var buffer = new List<string>();
            using (StreamReader streamReader = new StreamReader(path))
            {
                while (!streamReader.EndOfStream)
                {
                    buffer.Add(streamReader.ReadLine());
                }
            }

            List<DialogRecord> wholeDialog = new List<DialogRecord>();

            List<string> recbuffer = new List<string>();
            foreach (var s in buffer)
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

                        if (wholeDialog.Count > 0 && record.Sender != speaker1 && record.Sender != speaker2)
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

            return wholeDialog;
        }
        
        public static void CreateSets()
        {
            string speaker1 = "Roman";

            Dictionary<string, string> fileDictionary = new Dictionary<string, string>();
            fileDictionary.Add("Vano", @"C:\TrainingSet\Vano.txt");
            fileDictionary.Add("Anna", @"C:\TrainingSet\Anna.txt");
            fileDictionary.Add("Danila", @"C:\TrainingSet\Danila.txt");
            fileDictionary.Add("Dinara", @"C:\TrainingSet\Dinara.txt");
            fileDictionary.Add("Farit", @"C:\TrainingSet\Farit.txt");
            fileDictionary.Add("Igor", @"C:\TrainingSet\Igor.txt");
            fileDictionary.Add("Katherine", @"C:\TrainingSet\Katherine.txt");
            fileDictionary.Add("Lilia", @"C:\TrainingSet\Lilia.txt");
            fileDictionary.Add("Tanya", @"C:\TrainingSet\Tanya.txt");

            DialogService myReplics = new DialogService();
            DialogService theirReplics = new DialogService();
            myReplics.Records = new List<DialogRecord>();
            theirReplics.Records = new List<DialogRecord>();

            foreach (KeyValuePair<string, string> file in fileDictionary)
            {
                string speaker2 = file.Key;
                string inputPath = file.Value;
                Console.WriteLine(string.Format("Processing Dialog between {0} and {1}...", speaker1, speaker2));

                List<DialogRecord> wholeDialog = DialogService.GetRecordsFromFile(inputPath, speaker1, speaker2);
                //List<string> AllWords = new List<string>();

                //wholeDialog.ForEach(x => AllWords.AddRange(x.Words));
                //AllWords = AllWords.Distinct().ToList();

                var speaker1Role = wholeDialog.Where(x => x.Sender == speaker1).ToList();
                var speaker2Role = wholeDialog.Where(x => x.Sender == speaker2).ToList();
                wholeDialog.RemoveAll(x => x.Sender == speaker1 || x.Sender == speaker2);

                if (wholeDialog.Count > 0)
                {
                    throw new Exception("Something bad happend..." + speaker2);
                }

                //for (int i = 0; i < speaker2Role.Count; ++i)
                //{
                //    if (i < speaker1Role.Count)
                //    {
                //        speaker2Role[i].RelatedRecord = speaker1Role[i];
                //    }
                //}
                myReplics.Records.AddRange(speaker1Role);
                theirReplics.Records.AddRange(speaker2Role);
            }

            myReplics.SaveToFile(@"C:\TrainingSet\Me.txt");
            theirReplics.SaveToFile(@"C:\TrainingSet\Them.txt");

            GC.Collect();
            Console.WriteLine("Ready.");
            Console.ReadKey();
        }
    }
}
