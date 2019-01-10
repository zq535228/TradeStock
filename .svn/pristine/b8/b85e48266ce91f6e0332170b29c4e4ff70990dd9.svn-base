using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace org.jiechan.service {
    public class ProcessHelper {

        /// <summary>
        /// 使用默认的浏览器,打开路径,可以是本地文件夹
        /// </summary>
        /// <param name="pathUrl"></param>
        public static void openUrl(string pathUrl) {
            if (!string.IsNullOrEmpty(pathUrl)) {
                try {
                    System.Diagnostics.Process.Start(pathUrl);
                } catch { }
            }

        }

        /// <summary>
        /// 调用exe程序，并传递参数。
        /// 参照文档地址：http://www.cnblogs.com/nba4523/archive/2009/07/04/1516753.html
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static bool StartProcess(string filename, string[] args) {
            try {
                string s = "";
                foreach (string arg in args) {
                    s = s + arg + " ";
                }
                s = s.Trim();
                Process myprocess = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo(filename, s);
                myprocess.StartInfo = startInfo;
                myprocess.StartInfo.UseShellExecute = false;
                myprocess.Start();
                return true;
            } catch (Exception ex) {
                EchoHelper.EchoException(ex);
            }
            return false;
        }

    }
}
