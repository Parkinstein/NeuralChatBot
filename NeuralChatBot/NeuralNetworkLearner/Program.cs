using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vocabulary;

namespace NeuralNetworkLearner
{
    class Program
    {
        static void Main(string[] args)
        {
            //VocabularyService.InitializeWords(@"C:\TrainingSet\ThemVec.txt", @"C:\TrainingSet\MeVec.txt");
            VocabularyService.InitializeDialog(@"C:\TrainingSet\Them.txt", @"C:\TrainingSet\Me.txt");
            Console.WriteLine("SUCCESS");
            Console.ReadKey();
            //var word2Vec = Word2VecBuilder.Create()
            //   .WithTrainFile(@"C:\TrainingSet\Them.txt")
            //   .WithOutputFile(@"C:\TrainingSet\ThemSmallVecB.bin")
            //   //.WithCBow(0)
            //   .WithThreads(1)
            //   .WithMinCount(2)
            //   .WithSize(50)
            //   .Build();
            //word2Vec.TrainModel();
            //DialogService.CreateSets();
        }
    }
}
