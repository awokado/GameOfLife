using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Runtime.Inter‌​opServices.WindowsRun‌​time;
using System.IO;

namespace LearningGameOfLife_1
{
    /** Singleton class, main game operator **/
    public class GameEngine
    {
        private static GameEngine geInstance;
        
        private int[,] worldArray;


        private GameEngine()
        {
            LogConsole.WriteLine("Game engine constructor");
            InitTheworld(10, 10,0);
        }


        public static GameEngine GEInstance
        {
            get { return geInstance ?? (geInstance = new GameEngine()); }
            set { }
        }

        public void InitTheworld(int height, int width, int aliveProb)
        {
            Random rnd = new Random();
            worldArray = new int[height,width];
            worldArray[0, 0] = 1;

            for (int i = 0; i < worldArray.GetLength(0); i++)
            {
                for (int j = 0; j < worldArray.GetLength(1); j++)
                {
                    if(rnd.Next(0,100) < aliveProb) worldArray[i, j] = 0; 
                    else worldArray[i, j] = 1;

                }
            }



            int a;

            for (int i = 0; i < worldArray.GetLength(0); i++)
            {
                for (int j = 0; j < worldArray.GetLength(1); j++)
                {
                    //Console.Write(worldArray[i, j]);
                }
                //Console.Write("\n");
            }


            //https://msdn.microsoft.com/en-us/library/system.windows.media.imaging.bitmappalette(v=vs.110).aspx
            //http://www.i-programmer.info/programming/wpf-workings/527-writeablebitmap.html?start=1

            BitmapImage b = new BitmapImage();

            var wbmap = new WriteableBitmap(height,width, 30,30, PixelFormats.Bgra32,null);
            byte[] pixels = new byte[wbmap.PixelHeight * wbmap.PixelWidth *wbmap.Format.BitsPerPixel / 8];
            //The order of the bytes is Blue, Green, Red and Alpha.
            pixels[0] = 0xff;
            pixels[1] = 0x00;
            pixels[2] = 0x00;
            pixels[3] = 0xff;

            pixels[40] = 0xff;
            pixels[41] = 0x00;
            pixels[42] = 0x00;
            pixels[43] = 0xff;

            pixels[396] = 0xff;
            pixels[397] = 0x00;
            pixels[398] = 0x00;
            pixels[399] = 0xff;

            

            int poz = 0;
            for (int i = 0; i < worldArray.GetLength(0); i++)
            {
                for (int j = 0; j < worldArray.GetLength(1); j++)
                {
                    if (rnd.Next(0, 100) < aliveProb) worldArray[i, j] = 0;
                    else
                    {
                        worldArray[i, j] = 1;
                        poz = (i*4 + j * 16);
                        //Console.WriteLine(poz);
                        //Console.Write(i);
                        //Console.WriteLine(j);
                        Console.WriteLine("- " + i + " " + j + " " + poz);
                        pixels[poz + 0] = 0xff;
                        pixels[poz + 1] = 0x00;
                        pixels[poz + 2] = 0x00;
                        pixels[poz + 3] = 0xff;
                    }

                }
            }
            

            wbmap.WritePixels(new Int32Rect(0, 0,wbmap.PixelWidth,wbmap.PixelHeight),pixels,wbmap.PixelWidth * wbmap.Format.BitsPerPixel/8,0);


            Console.WriteLine(pixels.Length);
            CreateThumbnail("test.jpg", wbmap);


            //b.BeginInit();
            //b.UriSource = new Uri("c:\\plus.png");
            //b.EndInit();


        }




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
