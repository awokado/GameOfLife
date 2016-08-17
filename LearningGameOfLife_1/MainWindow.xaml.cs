using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using System.IO;
using Microsoft.Win32.SafeHandles;
using System.Windows.Interop;

namespace LearningGameOfLife_1
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public GameEngine gameEngine;
        public GraphicEngine graphicEngine;
        private const UInt32 StdOutputHandle = 0xFFFFFFF5;

        public MainWindow()
        {
            InitializeComponent();

            gameEngine = GameEngine.GEInstance;
            gameEngine.InitTheworld(580, 580, 99, true);

            graphicEngine = GraphicEngine.GraphicEngineInstance;

            graphicEngine.setHandler(this);
            LogConsole.WriteLine("przed view");
            graphicEngine.initView();



            graphicEngine.setRootGrid(rootGrid);
            graphicEngine.ShowWorld(rootGrid);

            //IntPtr windowHandle = new WindowInteropHelper(this).Handle;
            //HwndSource source = (HwndSource)HwndSource.FromVisual(this);
            // http://blog.scottlogic.com/2012/04/20/everything-you-wanted-to-know-about-databinding-in-wpf-silverlight-and-wp7-part-two.html

        }


    }
}
