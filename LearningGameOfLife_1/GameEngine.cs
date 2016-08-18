using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Runtime.Inter‌​opServices.WindowsRun‌​time;
using System.IO;
using System.Diagnostics;

namespace LearningGameOfLife_1
{
    /*Singleton class, main game operator*/
    public class GameEngine
    {

        //**********************************MEMBERS**********************************//
        private static readonly int MAX_WORLD_HEIGHT = 1000;
        private static readonly int MAX_WORLD_WIDTH = 1000;
        private static GameEngine _geInstance;
        public GameWorld gameWorld { get; private set; } = null;
        private PopulatingParams popParams = null;
        private NextDayCalc nxDayCalc = null;


        //**********************************METHODS**********************************//
        /*reverts with GameEngine instance*/
        public static GameEngine GEInstance
        {
            get
            {
                LogConsole.WriteLine("Game engine getInstance");
                if (_geInstance == null) { LogConsole.WriteLine("nie istnieje jeszcze instancja Game engine"); }
                else LogConsole.WriteLine("istnieje już instancja Game engine");
                
                return _geInstance ?? (_geInstance = new GameEngine());

            }
            set { }
        }


        /*private constructor initiates the board for game*/
        private GameEngine()
        {
            LogConsole.WriteLine("Game engine constructor");
        }


        /*Creates and array of int which will represent the world of game, it will return false if init will fail
        height - should be not greater than 1000
        width -should be not greater than 1000
        aliveProb - how probable that considered pixel will be alive - IDEA: create it as object, in future pixel can havemore than dead/alive stage
        IDEA new param: populateMethod - populate array in other ways tahn rand, maybe create alive guys in some patterns
        isWrappable - set the parameter dfining is world wrapped or not*/
        //https://msdn.microsoft.com/en-us/library/system.windows.media.imaging.bitmappalette(v=vs.110).aspx
        //http://www.i-programmer.info/programming/wpf-workings/527-writeablebitmap.html?start=1

        public bool InitTheworld(int height, int width, int aliveProb, bool isWrappable)
        {

            /*DIAGNOSTIC*/
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            /*DIAGNOSTIC*/

            /*check the arguments*/
            if (height > MAX_WORLD_HEIGHT | width > MAX_WORLD_WIDTH) { Console.WriteLine("Zainicjalizowno za duży świat"); return false; }
            gameWorld = new GameWorld(height, width, isWrappable);
            popParams = new PopulatingParams(aliveProb);
            gameWorld.Populate(GameWorld.MethodsOfPopulation.random, popParams);

            /*DIAGNOSTIC*/
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);
            /*DIAGNOSTIC*/

            return true;
        }


        public void Run(int noOfDays)
        {
            nxDayCalc = new NextDayCalc(NextDayCalc.NextDayRules.Conway);
            for (int i = 0; i < noOfDays; i++)
            {
                Console.WriteLine("go gog run run");
                gameWorld.getNextDay(nxDayCalc);
            }
        }


    }
}
