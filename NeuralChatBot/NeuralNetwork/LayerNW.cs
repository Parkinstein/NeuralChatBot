using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    // Класс - слой нейросети
    public class LayerNW
    {
        double[,] Weights;
        int cX, cY;

        // Заполняем веса случайными числами
        public void GenerateWeights()
        {
            Random rnd = new Random();
            for (int i = 0; i < cX; i++)
            {
                for (int j = 0; j < cY; j++)
                {
                    Weights[i, j] = rnd.NextDouble() - 0.5;
                }
            }
        }

        // Выделяет память под веса
        protected void GiveMemory()
        {
            Weights = new double[cX, cY];
        }

        // Конструктор с параметрами. передается количество входных и выходных нейронов
        public LayerNW(int countX, int countY)
        {
            cX = countX;
            cY = countY;
            GiveMemory();
        }

        public int countX
        {
            get { return cX; }
        }

        public int countY
        {
            get { return cY; }
        }

        public double this[int row, int col]
        {
            get { return Weights[row, col]; }
            set { Weights[row, col] = value; }
        }

    }
}
