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

        /*Members*/
        private static readonly int MAX_WORLD_HEIGHT = 1000;
        private static readonly int MAX_WORLD_WIDTH = 1000;
        private static GameEngine geInstance;
        private int[,] worldArray = null;
        private GameWorld gameWorld = null;
        private PopulatingParams popParams = null;
        private WriteableBitmap gameWorldAsWriteableBitmap = null;
        private Image worldAsImage;
        Grid rootGrid = null;


        /*reverts with GameEngine instance*/
        public static GameEngine GEInstance
        {
            get { return geInstance ?? (geInstance = new GameEngine()); }
            set { }
        }

        internal void ShowWorld(Grid rootGrid)
        {
            rootGrid.Children.Add(WriteableBitmap2Image(gameWorldAsWriteableBitmap));
        }

        internal void setRootGrid(Grid rootGrid)
        {
            this.rootGrid = rootGrid;
        }

        /*private constructor initiates the board for game*/
        private GameEngine()
        {
            LogConsole.WriteLine("Game engine constructor");

            /*this should not be here*/
            InitTheworld(900, 900, 15, true);
            gameWorldAsWriteableBitmap = getActualGameWorldAsWriteableBitmap(gameWorld.worldHeight, gameWorld.worldWidth, gameWorld.world);
            worldAsImage = WriteableBitmap2Image(gameWorldAsWriteableBitmap);
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
            if (height > MAX_WORLD_HEIGHT | width > MAX_WORLD_WIDTH) { Console.WriteLine("Zainicjalizowna za duży świat"); return false; }
            gameWorld = new GameWorld(height, width, isWrappable);
            popParams = new PopulatingParams(97);
            gameWorld.Populate(GameWorld.MethodsOfPopulation.random, popParams);

            /*DIAGNOSTIC*/
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);
            /*DIAGNOSTIC*/

            return true;
        }


        /*return with WriteableBitmap of actual world
        should I porotect it form null?
        what if I want represent a fello with more than one pix*/
        public WriteableBitmap getActualGameWorldAsWriteableBitmap(int worldHeight, int worldWidth, int[,] world)
        {
            var wbmap = new WriteableBitmap(gameWorld.worldHeight, gameWorld.worldWidth, 10, 10, PixelFormats.Bgra32, null);
            byte[] pixels = new byte[wbmap.PixelHeight * wbmap.PixelWidth * wbmap.Format.BitsPerPixel / 8];

            int poz = 0;
            for (int i = 0; i < world.GetLength(0); i++)
            {
                for (int j = 0; j < world.GetLength(1); j++)
                {
                    poz = 4 * (i * world.GetLength(1) + j);
                    if (world[i, j] == 1)
                    {
                        pixels[poz + 0] = 0xff;
                        pixels[poz + 1] = 0x00;
                        pixels[poz + 2] = 0x00;
                        pixels[poz + 3] = 0xff;
                    }
                }
            }
            wbmap.WritePixels(new Int32Rect(0, 0, wbmap.PixelWidth, wbmap.PixelHeight), pixels, wbmap.PixelWidth * wbmap.Format.BitsPerPixel / 8, 0);
            CreateThumbnail("test2.png", wbmap);
            return wbmap;
        }

        private Image WriteableBitmap2Image(WriteableBitmap gameWorldAsWriteableBitmap)
        {
            var anImage = new Image();
            anImage.Width = gameWorldAsWriteableBitmap.PixelWidth;
            anImage.Height = gameWorldAsWriteableBitmap.PixelHeight;
            anImage.HorizontalAlignment = HorizontalAlignment.Left;
            anImage.VerticalAlignment = VerticalAlignment.Top;
            anImage.Margin = new Thickness(500, 500, 0, 0);
            anImage.Source = gameWorldAsWriteableBitmap;
            return anImage;
        }


        /*CLEAN MEEEEEEEEEEEEEEEE*/
        void CreateThumbnail(string filename, BitmapSource image5)
        {
            if (filename != string.Empty)
            {
                using (FileStream stream5 = new FileStream(filename, FileMode.Create))
                {
                    PngBitmapEncoder encoder5 = new PngBitmapEncoder();
                    encoder5.Frames.Add(BitmapFrame.Create(image5));
                    encoder5.Save(stream5);
                }
            }
        }



    }
}
