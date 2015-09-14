using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MgsVModsMananer
{
    public static class Common
    {
        public static string RQToolPath = AppDomain.CurrentDomain.BaseDirectory + "MGSV_QAR_Tool.exe";

        public static string RQToolDicPath = AppDomain.CurrentDomain.BaseDirectory + "QAR_Tool\\";

        public static string Data1FilePath = "master\\data1.dat";

        public static string Data1InfFilePath = "master\\data1.inf";

        public static string MasterDicPath = (AppDomain.CurrentDomain.BaseDirectory + "master\\").Replace('/', '\\');

        public static string BackupDicPath = (AppDomain.CurrentDomain.BaseDirectory + "backups\\").Replace('/', '\\');

        public static Dictionary<string, ModItemControl> Modfiles = new Dictionary<string, ModItemControl>();

        public static Dictionary<string, ModItemControl> ModSelectedfiles = new Dictionary<string, ModItemControl>();
    }
}
