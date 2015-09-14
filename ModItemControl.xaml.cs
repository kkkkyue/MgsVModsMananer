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

namespace MgsVModsMananer
{
    /// <summary>
    /// ModItemControl.xaml 的交互逻辑
    /// </summary>
    public partial class ModItemControl : UserControl
    {
        

        Label itemLable;

        public ModItemControl()
        {
            itemLable = new Label();
            InitializeComponent();
            checkBox.Content = itemLable;
            itemLable.VerticalAlignment = VerticalAlignment.Center;
            itemLable.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
        }

        public bool IsRed
        {
            set
            {
                if (value)
                {
                    itemLable.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                }
                else
                {
                    itemLable.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                }
            }
        }
        public object ItemName
        {
            get
            {
                return itemLable.Content;
            }

            set
            {
                itemLable.Content = value;
            }
        }
    }
}
