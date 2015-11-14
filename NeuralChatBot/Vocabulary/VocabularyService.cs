using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vocabulary.DbContext;
using Vocabulary.Models;
using System.IO;
using System.Text.RegularExpressions;
using VkDialogConverter;
using Newtonsoft.Json;

namespace Vocabulary
{
    public static class VocabularyService
    {
        public static int VectorSize { get; set; }
        public static int WordsCount { get; set; }
        
        
        public static void InitializeWords(string teacherFile, string studentFile)
        {
            Console.WriteLine("Preparing student vocabulary...");
            var StudentVocub = PrepareData(studentFile).GroupBy(x => x.Key).Select(g => g.First()).ToList();
            Console.WriteLine("Preparing teacher vocabulary...");
            var TeacherVocub = PrepareData(teacherFile).GroupBy(x => x.Key).Select(g => g.First()).ToList();

            using (VocabularyDb Context = new VocabularyDb())
            {
                Context.Configuration.AutoDetectChangesEnabled = false;
                foreach (var word in TeacherVocub)
                {
                    Console.WriteLine("Adding teaching Word: " + word.Key);
                    Context.Words.Add(new Word
                    {
                        IsTeacher = true,
                        Value = word.Key,
                        Coords = JsonConvert.SerializeObject(word.Value)
                    });
                }

                Console.WriteLine("Saving changes in db...");
                Context.SaveChanges();
                foreach (var word in StudentVocub)
                {
                    Console.WriteLine("Adding studying Word: " + word.Key);
                    Context.Words.Add(new Word
                    {
                        IsTeacher = false,
                        Value = word.Key,
                        Coords = JsonConvert.SerializeObject(word.Value)
                    });                   
                }
                Console.WriteLine("Saving changes in db...");
                Context.SaveChanges();
                Context.Configuration.AutoDetectChangesEnabled = true;
            }

            GC.Collect();
        }

        private static List<KeyValuePair<string, double[]>> PrepareData(string path)
        {
            var buffer = new List<KeyValuePair<string, double[]>>();
            using (StreamReader streamReader = new StreamReader(path))
            {
                if (!streamReader.EndOfStream)
                {
                    ParseFirstString(streamReader.ReadLine());
                }

                while (!streamReader.EndOfStream)
                {
                    buffer.Add(ParseString(streamReader.ReadLine()));                
                }
            }
            return buffer;
        }

        private static void ParseFirstString(string dataString)
        {
            var subStrings = dataString.Split(' ');
            if (subStrings.Count() != 2)
            {
                throw new Exception("This is not first string, dude");
            }

            int wordscount;
            int size;
            

            if(int.TryParse(subStrings[0], out wordscount))
            {
                WordsCount = wordscount;
            }
            else
            {
                throw new Exception("Cannot read wordscount");
            }
                      
            if (int.TryParse(subStrings[1], out size))
            {
                VectorSize = size;
            }
            else
            {
                throw new Exception("Cannot read size");
            }         
            
        }

        private static KeyValuePair<string, double[]> ParseString(string dataString)
        {
            var subStrings = dataString.Split(' ');
            if (subStrings.Count() != 2)
            {
                throw new Exception("This is not correct string, dude");
            }
            return new KeyValuePair<string, double[]>(subStrings[0], ParseCoords(subStrings[1]));
        }

        private static double[] ParseCoords(string coords)
        {
            double[] result = new double[VectorSize];
                     
            for (int i = coords.Length-1; i >= 0; --i)
            {
                if (coords[i] == ',')
                {
                    if(i>=2 && coords[i-2] == '-')
                    {
                        coords=coords.Insert(i - 2, "|");
                    }
                    else if(i >= 1 && coords[i - 1] == '0')
                    {
                        coords=coords.Insert(i - 1, "|");
                    }
                }
            }
            var coordStrings = coords.Split('|').ToList();
            coordStrings.RemoveAll(x => x == string.Empty);

            if (coordStrings.Count > VectorSize)
            {
                throw new Exception("Wrong format, dude");
            }
            for (int i= 0;i< coordStrings.Count; ++i)
            {
                double.TryParse(coordStrings[i], out result[i]);
            }
            return result;
        }
        
        public static void InitializeDialog(string teacherFile, string studentFile)
        {
            var records = DialogService.CreateSets();
        }
    }
}
