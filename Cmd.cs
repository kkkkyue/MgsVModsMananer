using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MgsVModsMananer
{
    /// <summary>
    /// Cmd 的摘要说明。
    /// </summary>
    public class Cmd
    {
        private Process proc = null;
        /// <summary>
        /// 构造方法
        /// </summary>
        public Cmd()
        {
            proc = new Process();
        }
        /// <summary>
        /// 执行CMD语句
        /// </summary>
        /// <param name="cmd">要执行的CMD命令</param>
        public string RunCmd(string cmd)
        {
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.FileName = "cmd.exe";
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.RedirectStandardInput = true;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.Start();
            proc.StandardInput.WriteLine(cmd);
            proc.StandardInput.WriteLine("exit");
            string outStr = proc.StandardOutput.ReadToEnd();
            proc.Close();
            return outStr;
        }
        /// <summary>
        /// 打开软件并执行命令
        /// </summary>
        /// <param name="programName">软件路径加名称（.exe文件）</param>
        /// <param name="cmd">要执行的命令</param>
        public void RunProgram(string programName,string arguments)
        {
            Process proc = new Process();
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.FileName = programName;
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.RedirectStandardInput = true;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.Arguments = arguments;
            //proc.i
            proc.OutputDataReceived += Proc_OutputDataReceived;
            proc.Exited += Proc_Exited;
            proc.Start();
            proc.BeginOutputReadLine();
            proc.WaitForExit();


        }

        private void Proc_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
           // throw new NotImplementedException();
        }

        private void Proc_Exited(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 打开软件
        /// </summary>
        /// <param name="programName">软件路径加名称（.exe文件）</param>
        public void RunProgram(string programName)
        {
            this.RunProgram(programName, "");
        }
    }

}
