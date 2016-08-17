using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace LearningGameOfLife_1
{

    /* Class specyfying how does the world look like */
    public class GameWorld
    {
        /* Members */
        public int worldHeight { get; private set; }
        public int worldWidth { get; private set; }
        public bool isWrappable { get; private set; }
        public int[,] world { get; private set; }
        public enum MethodsOfPopulation { random, other, test };

        /*Constructor*/
        public GameWorld(int worldHeight, int worldWidth, bool isWrappable)
        {
            LogConsole.WriteLine("GameWorld constructor");
            this.worldHeight = worldHeight;
            this.worldWidth = worldWidth;
            this.isWrappable = isWrappable;
            world = new int[this.worldHeight, this.worldWidth];
        }

        /*Populates the world maintaining rules of populating*/
        public bool Populate(MethodsOfPopulation method, PopulatingParams popPar)
        {
            switch (method)
            {
                case MethodsOfPopulation.random:
                    PopulatingMethodRandom(popPar);
                    return true;

                case MethodsOfPopulation.other:
                    return true;

                case MethodsOfPopulation.test:
                    return true;

                default:
                    return false;
            }
        }

        /*Populate the world array with live members accord. to probability value*/
        private void PopulatingMethodRandom(PopulatingParams popPar)
        {
            Random rnd = new Random();

            for (int i = 0; i < world.GetLength(0); i++)
            {
                for (int j = 0; j < world.GetLength(1); j++)
                {
                    if (rnd.Next(0, 100) > popPar.aliveProbability) world[i, j] = 0;
                    else world[i, j] = 1;
                }
            }
        }

    }
}
