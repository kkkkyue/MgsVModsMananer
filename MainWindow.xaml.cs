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
using MgsVModsMananer.Models;

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

            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "backups"))
            {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "backups");
            }

            var modlist=Directory.EnumerateFiles(AppDomain.CurrentDomain.BaseDirectory + "mods").ToList();

            modlistBox.SelectionChanged += ModlistBox_SelectionChanged;

            foreach (var item in modlist)
            {
                ICSharpCode.SharpZipLib.Zip.ZipFile zip = new ICSharpCode.SharpZipLib.Zip.ZipFile(item);
                var enrty = zip.FindEntry("config.json", true);
                if (enrty > 0)
                {
                    var config = MODConfigHelper.LoadConfig(zip.GetInputStream(enrty));
                    ModItemControl boxitem = new ModItemControl();
                    boxitem.ItemName = config.Name;
                    boxitem.IsRed = true;
                   // ListBoxItem boxitem = new ListBoxItem();
                   // boxitem.Content = config.Name;
                    boxitem.Tag = config;
                    modlistBox.Items.Add(boxitem);
                }
            }
            
            var t=ZipHelper.UnZip(modlist[0], AppDomain.CurrentDomain.BaseDirectory + "~temp");

            
            MODConfigHelper.SaveConfig(AppDomain.CurrentDomain.BaseDirectory + "config.json", new Models.ModConfigModel() { Name = "S rank", Descript = "RANK S",Author="kkkkyue" });

            //Cmd cmd = new Cmd();
            //cmd.RunProgram(Common.RQToolPath, Common.DatFilePath + " -r");
        }

        private void ModlistBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ModConfigModel config = ((ModItemControl)modlistBox.SelectedItem).Tag as ModConfigModel;
            desTextBlock.Text = config.Descript;
            modNamelabel.Content = config.Name;
           // throw new NotImplementedException();
        }
    }
}
