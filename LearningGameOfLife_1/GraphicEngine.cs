using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace LearningGameOfLife_1
{
    public class GraphicEngine
    {
        //**********************************MEMBERS**********************************//
        private static GraphicEngine _graphicEngine = null;
        private static GameEngine _gameEngine = GameEngine.GEInstance;
        private MainWindow mainWindowHandle;
        public static GraphicEngine GraphicEngineInstance {
            get { return _graphicEngine ?? (_graphicEngine = new GraphicEngine()); }
            set { }
        }
        private WriteableBitmap gameWorldAsWriteableBitmap = null;
        private Image worldAsImage;
        Grid rootGrid = null;

        //**********************************METHODS**********************************//


        /// <summary>
        /// Transfers reference of main window to GameEngine
        /// </summary>
        /// <param name="mainWindow"></param>
        public void setHandler(MainWindow mainWindow)
        {
            this.mainWindowHandle = mainWindow;
        }
        /// <summary>
        /// initializes the wiev on aplication startup
        /// IDEA: load logo or something similar
        /// </summary>
        public void initView()
        {
            gameWorldAsWriteableBitmap = getActualGameWorldAsWriteableBitmap(_gameEngine.gameWorld.world.GetLength(0), _gameEngine.gameWorld.world.GetLength(1), _gameEngine.gameWorld.world);
        }



        //            gameWorldAsWriteableBitmap = getActualGameWorldAsWriteableBitmap(gameWorld.worldHeight, gameWorld.worldWidth, gameWorld.world);
        //             worldAsImage = WriteableBitmap2Image(gameWorldAsWriteableBitmap);

        /// <summary>
        /// return with WriteableBitmap of actual world should I porotect it form null? what if I want represent a fello with more than one pix
        /// </summary>
        /// <param name="worldHeight"></param>
        /// <param name="worldWidth"></param>
        /// <param name="world"></param>
        /// <returns></returns>

        public WriteableBitmap getActualGameWorldAsWriteableBitmap(int worldHeight, int worldWidth, int[,] world)
        {
            var wbmap = new WriteableBitmap(worldHeight, worldWidth, 10, 10, PixelFormats.Bgra32, null);
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
            anImage.Margin = new Thickness(10, 10, 10, 10);
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

        internal void ShowWorld(Grid rootGrid)
        {
            rootGrid.Children.Clear();
            rootGrid.Children.Add(WriteableBitmap2Image(gameWorldAsWriteableBitmap));
        }

        internal void setRootGrid(Grid rootGrid)
        {
            this.rootGrid = rootGrid;
        }
    }
}
