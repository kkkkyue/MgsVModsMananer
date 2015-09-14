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
using ICSharpCode.SharpZipLib.Zip;

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
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "mods"))
            {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "mods");
            }

            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "backups"))
            {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "backups");
            }

            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + "mgsvtpp.exe"))
            {
                MessageBox.Show("请放在游戏根目录");
                App.Current.Shutdown();
            }
            modlistBox.SelectionChanged += ModlistBox_SelectionChanged;
            List<string> modlist = reLoadModList();

            


            //MODConfigHelper.SaveConfig(AppDomain.CurrentDomain.BaseDirectory + "config.json", new Models.ModConfigModel() { Name = "S rank", Descript = "RANK S",Author="kkkkyue" });

            //Cmd cmd = new Cmd();
            //cmd.RunProgram(Common.RQToolPath, Common.Data1FilePath + " -r");
           // var t = ZipHelper.UnZip(modlist[0], AppDomain.CurrentDomain.BaseDirectory + "~temp");
        }

        private List<string> reLoadModList()
        {
            modlistBox.Items.Clear();
            var modlist = Directory.EnumerateFiles(AppDomain.CurrentDomain.BaseDirectory + "mods").ToList();

            foreach (var item in modlist)
            {
                ICSharpCode.SharpZipLib.Zip.ZipFile zip = new ICSharpCode.SharpZipLib.Zip.ZipFile(item);

                var enrty = zip.FindEntry("config.json", true);
                if (enrty >= 0)
                {
                    var config = MODConfigHelper.LoadConfig(zip.GetInputStream(enrty));
                    ModItemControl boxitem = new ModItemControl();
                    boxitem.ItemName = config.Name;
                    //boxitem.IsRed = true;
                    // ListBoxItem boxitem = new ListBoxItem();
                    // boxitem.Content = config.Name;
                    boxitem.Tag = config;
                    boxitem.DataContext = item;
                    modlistBox.Items.Add(boxitem);
                }
            }

            return modlist;
        }

        private void ModlistBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ModConfigModel config = ((ModItemControl)modlistBox.SelectedItem).Tag as ModConfigModel;
            desTextBlock.Text = config.Descript;
            modNamelabel.Content = config.Name;
           // throw new NotImplementedException();
        }

        private void makeModButton_Click(object sender, RoutedEventArgs e)
        {
            MakeModWindow mmw = new MakeModWindow();
            mmw.ShowDialog();
            //reLoadModList();
        }

        void BackupFiles()
        {
            foreach (var path in Common.Modfiles)
            {
                if (!File.Exists(Common.BackupDicPath + path.Key))
                {
                    FileInfo fi = new FileInfo(Common.MasterDicPath + path.Key);
                    //fi.
                    string backuppath = (Common.BackupDicPath + path.Key);
                    backuppath = backuppath.Remove(backuppath.LastIndexOf('/'));
                    if (!Directory.Exists(backuppath))
                    {
                        Directory.CreateDirectory(backuppath);
                    }
                    if (File.Exists(Common.MasterDicPath + path.Key))
                    {
                        File.Copy(Common.MasterDicPath + path.Key, Common.BackupDicPath + path.Key);
                    }
                    
                }
                
            }
            
        }

        private void applyButton_Click(object sender, RoutedEventArgs e)
        {
            if (Common.ModSelectedfiles.Count==0)
            {
                MessageBox.Show("请选择MOD");
                return;
            }
            
            Cmd cmd = new Cmd();
            
            foreach (var path in Common.ModSelectedfiles)
            {
                FastZip fz = new FastZip();
                fz.ExtractZip(path.Key, AppDomain.CurrentDomain.BaseDirectory + "~temp","");
            }
            File.Delete(AppDomain.CurrentDomain.BaseDirectory + "~temp\\config.json");
            DirectoryInfo di = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "~temp");
            var dics=di.GetDirectories();

            foreach(var item in dics)
            {
                cmd.RunProgram(Common.RQToolPath, "master\\"+item.Name + ".dat -r");
            }
            BackupFiles();

            CopyDirectory(AppDomain.CurrentDomain.BaseDirectory + "~temp",Common.MasterDicPath);
            Directory.Delete(AppDomain.CurrentDomain.BaseDirectory + "~temp",true);
            foreach (var item in dics)
            {
                cmd.RunProgram(Common.RQToolPath, "master\\" + item.Name + ".inf -r");
                Directory.Delete(Common.MasterDicPath + item.Name, true);
                File.Delete(Common.MasterDicPath + item.Name + ".inf");
            }
            MessageBox.Show("MOD成功");

        }

        static void CopyDirectory(string srcDir, string tgtDir)
        {
            DirectoryInfo source = new DirectoryInfo(srcDir);
            DirectoryInfo target = new DirectoryInfo(tgtDir);

            if (target.FullName.StartsWith(source.FullName.EndsWith("\\") ? source.FullName : (source.FullName + "\\"), StringComparison.CurrentCultureIgnoreCase))
            {
                throw new Exception("父目录不能拷贝到子目录！");
            }

            if (!source.Exists)
            {
                return;
            }

            if (!target.Exists)
            {
                target.Create();
            }

            FileInfo[] files = source.GetFiles();

            for (int i = 0; i < files.Length; i++)
            {
                File.Copy(files[i].FullName, target.FullName + @"\" + files[i].Name, true);
            }

            DirectoryInfo[] dirs = source.GetDirectories();

            for (int j = 0; j < dirs.Length; j++)
            {
                CopyDirectory(dirs[j].FullName, target.FullName + @"\" + dirs[j].Name);
            }
        }


        private void makeModButton_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void clearbutton_Click(object sender, RoutedEventArgs e)
        {
            Cmd cmd = new Cmd();
            DirectoryInfo di = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "backups");
            var dics = di.GetDirectories();

            foreach (var item in dics)
            {
                cmd.RunProgram(Common.RQToolPath, "master\\" + item.Name + ".dat -r");
            }


            CopyDirectory(AppDomain.CurrentDomain.BaseDirectory + "backups", Common.MasterDicPath);
            foreach (var item in dics)
            {
                cmd.RunProgram(Common.RQToolPath, "master\\" + item.Name + ".inf -r");
                Directory.Delete(Common.MasterDicPath + item.Name, true);
                File.Delete(Common.MasterDicPath + item.Name + ".inf");
            }
            MessageBox.Show("还原成功");
        }
    }
}
