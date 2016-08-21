using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningGameOfLife_1
{

    class NextDayCalc
    {
        //**********************************MEMBERS***********************************//
        public enum NextDayRules { Conway, other, test };
        private NextDayRules nextDayRule;
        private int[,] oldGameWorlArray;
        private int[,] newGameWorlArray;
        private GameWorld _gameWorldInstance;

        //**********************************METHODS**********************************//
        /// <summary>
        /// Standard constructor
        /// </summary>
        public NextDayCalc()
        {
            this.nextDayRule = NextDayRules.Conway;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="nextDayRule">decide how next day will be generated</param>
        public NextDayCalc(NextDayRules nextDayRule)
        {
            this.nextDayRule = nextDayRule;
        }
        /// <summary>
        /// returns with next day 
        /// </summary>
        /// <param name="world">array of world that you want to get next day</param>
        /// <returns>modyfied array with new state</returns>
        public int[,] getNextDay(GameWorld gameWorldInstance)
        {

            this._gameWorldInstance = gameWorldInstance; //Get the instance of worls object 
            this.oldGameWorlArray = _gameWorldInstance.world; //Get the array representing dead/alive state 
            newGameWorlArray = new int[oldGameWorlArray.GetLength(0), oldGameWorlArray.GetLength(1)];

            switch (nextDayRule)
            {
                case NextDayRules.Conway:
                    return StdNexDayConway();
                default:
                    throw new NullReferenceException();
            }

        }

        /// <summary>
        /// cerates an array representing state of world after one day 
        /// </summary>
        private int[,] StdNexDayConway()
        {

            Console.WriteLine("-------New modyfied world generated using StdNexDayConway-----------------------");
            Console.WriteLine("oldGameWorlArray.GetLength(0)= " + oldGameWorlArray.GetLength(0));
            Console.WriteLine("oldGameWorlArray.GetLength(1)= " + oldGameWorlArray.GetLength(1));


            for (int i = 0; i < oldGameWorlArray.GetLength(0); i++)
            {
                for (int j = 0; j < oldGameWorlArray.GetLength(1); j++)
                {
                    int aliveNeighbours = 0;


                    for (int k = -1; k < 2; k++)
                    {
                        if (i + k < 0 || i + k >= oldGameWorlArray.GetLength(0)) { }
                        else
                        {
                            for (int l = -1; l < 2; l++)
                            {
                                if (j + l < 0 || j + l >= oldGameWorlArray.GetLength(1) || (k == 0 && l == 0)) { }
                                else { if (oldGameWorlArray[i + k, j + l] == 1) aliveNeighbours++; }

                            }
                        }

                    }

                    //Console.WriteLine(aliveNeighbours);
                    //test
                    if (aliveNeighbours > 3) newGameWorlArray[i, j] = 0;
                    if (aliveNeighbours < 2) newGameWorlArray[i, j] = 0;
                    if (aliveNeighbours > 1 & aliveNeighbours < 4) newGameWorlArray[i, j] = 1;

                }
            }
            return newGameWorlArray;
        }





    }



}
