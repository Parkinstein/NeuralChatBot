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
            Console.ReadKey();
        }
    }
}
