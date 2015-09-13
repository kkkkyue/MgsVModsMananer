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
using System.IO;

namespace MgsVModsMananer
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            init();
        }

        void init()
        {
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory+"mods"))
            {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory+"mods");
            }
            var modlist=Directory.EnumerateDirectories(AppDomain.CurrentDomain.BaseDirectory + "mods").ToList();

            Cmd cmd = new Cmd();
            cmd.RunProgram(Common.RQToolPath, Common.DatFilePath+" - r");
        }
    }
}
