using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

namespace MgsVModsMananer
{
    /// <summary>
    /// MakeModWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MakeModWindow : Window
    {
        public MakeModWindow()
        {
            InitializeComponent();
        }

        private void dicButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            
            fbd.ShowDialog();
            if (fbd.SelectedPath != string.Empty)
                this.dictextBox.Text = fbd.SelectedPath;

        }

        private void makeButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(dictextBox.Text) || string.IsNullOrEmpty(nameTextBox.Text.Trim())|| string.IsNullOrEmpty(desTextBox.Text.Trim()) || string.IsNullOrEmpty(authorTextBox.Text.Trim()) )
            {
                MessageBox.Show("缺少必填项");
                return;
            }

            if(File.Exists(AppDomain.CurrentDomain.BaseDirectory + "mods//" + nameTextBox.Text.Trim() + ".zip"))
            {
                MessageBox.Show("已存在同名文件");
                return;
            }

            Models.ModConfigModel config = new Models.ModConfigModel() { Name = nameTextBox.Text.Trim(), Descript = desTextBox.Text.Trim(), Author = authorTextBox.Text.Trim() };
            MODConfigHelper.SaveConfig(dictextBox.Text + "//config.json", config);
            System.IO.DirectoryInfo dinfo = new System.IO.DirectoryInfo(dictextBox.Text);
            if (!Directory.Exists(dictextBox.Text))
                return;
            FastZip fz = new FastZip();
            fz.CreateZip(AppDomain.CurrentDomain.BaseDirectory + "mods//" + nameTextBox.Text.Trim() + ".zip", dictextBox.Text, true, "");

            MessageBox.Show("制作完成,文件在MODS文件夹下,重启程序加载MOD");
        }
    }
}
