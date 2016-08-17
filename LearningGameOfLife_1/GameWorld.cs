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
    /// <summary>
    /// Class specyfying how does the world look like
    /// </summary>
    public class GameWorld
    {
        //**********************************MEMBERS**********************************//
        public int worldHeight { get; private set; }
        public int worldWidth { get; private set; }
        public int age{ get; private set; }
        public bool isWrappable { get; private set; }
        public int[,] world { get; private set; }
        public enum MethodsOfPopulation { random, other, test };
        public enum NextDayRules { std, other, test };

        //**********************************METHODS**********************************//
        /// <summary>
        /// Constructor of the world - feel the power
        /// </summary>
        /// <param name="worldHeight">how big is this world</param>
        /// <param name="worldWidth">how big is this world</param>
        /// <param name="isWrappable">is it round(wrappable) or flat</param>
        public GameWorld(int worldHeight, int worldWidth, bool isWrappable)
        {
            LogConsole.WriteLine("GameWorld constructor");
            this.worldHeight = worldHeight;
            this.worldWidth = worldWidth;
            this.isWrappable = isWrappable;
            world = new int[this.worldHeight, this.worldWidth];
            age = 0;
        }

        /// <summary>
        /// Populates the world maintaining rules of populating
        /// </summary>
        /// <param name="method">set of rules describing how to populate the world</param>
        /// <param name="popPar">parameters for method</param>
        /// <returns></returns>
        public bool Populate(MethodsOfPopulation method, PopulatingParams popPar)
        {
            switch (method)
            {
                case MethodsOfPopulation.random:
                    PopulatingMethodRandom(popPar);
                    age++;
                    return true;

                case MethodsOfPopulation.other:
                    return true;

                case MethodsOfPopulation.test:
                    return true;

                default:
                    return false;
            }
        }

        /// <summary>
        /// Populate the world array with live members accord. to probability value
        /// </summary>
        /// <param name="popPar">parameters for method</param>
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

        public int[,] getNextDay(NextDayRules nxDayRules)
        {
            return null;
        }



    }
}
