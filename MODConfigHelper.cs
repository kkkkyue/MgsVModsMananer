using MgsVModsMananer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MgsVModsMananer
{
    public class MODConfigHelper
    {
        public static ModConfigModel LoadConfig(string file)
        {
            FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read);
            TextReader tr = new StreamReader(fs, Encoding.Default);
            string str = tr.ReadToEnd();
            fs.Close();
            tr.Close();
            ModConfigModel config = Newtonsoft.Json.JsonConvert.DeserializeObject(str) as ModConfigModel;
            return config;
        }

        public static ModConfigModel LoadConfig(Stream file)
        {
           // FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read);
            TextReader tr = new StreamReader(file, Encoding.Default);
            string str = tr.ReadToEnd();
            //file.Close();
            tr.Close();
            ModConfigModel config = Newtonsoft.Json.JsonConvert.DeserializeObject(str,typeof(ModConfigModel)) as ModConfigModel;
            return config;
        }

        public static void SaveConfig(string file, ModConfigModel config)
        {
            FileStream fs = new FileStream(file, FileMode.Create, FileAccess.Write);
            TextWriter tw = new StreamWriter(fs, Encoding.Default);
            string str=Newtonsoft.Json.JsonConvert.SerializeObject(config);
            tw.Write(str);
            tw.Close();

        }
    }
}
