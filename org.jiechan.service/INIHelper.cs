using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Windows.Forms;

namespace org.jiechan.service {

    public class INIHelper {

        public static string GetStr(string Section, string Key) {
            INIHelper ini = new INIHelper(Application.StartupPath + "\\Config\\Setup.ini");
            string re = ini.re(Section, Key);
            return re;
        }

        public static bool GetBool(string Section, string Key) {
            INIHelper ini = new INIHelper(Application.StartupPath + "\\Config\\Setup.ini");
            bool re = false;
            try {
                re = Convert.ToBoolean(ini.re(Section, Key));
            } catch {
            }

            return re;
        }

        public static int GetInt32(string Section, string Key) {
            INIHelper ini = new INIHelper(Application.StartupPath + "\\Config\\Setup.ini");
            int re = 0;
            try {
                re = Convert.ToInt32(ini.re(Section, Key));
            } catch {
            }

            return re;
        }

        public static void Set(string Section, string Key, string Value) {
            INIHelper ini = new INIHelper(Application.StartupPath + "\\Config\\Setup.ini");
            ini.up(Section, Key, Value);
        }


        public string inipath;
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="INIPath">文件路径</param>
        public INIHelper(string INIPath) {
            inipath = INIPath;
        }
        /// <summary>
        /// 写入INI文件
        /// </summary>
        /// <param name="Section">项目名称(如 [TypeName] )</param>
        /// <param name="Key">键</param>
        /// <param name="Value">值</param>
        public void up(string Section, string Key, string Value) {
            WritePrivateProfileString(Section, Key, StringHelper.EncryptData(Value), this.inipath);
        }
        /// <summary>
        /// 读出INI文件
        /// </summary>
        /// <param name="Section">项目名称(如 [TypeName] )</param>
        /// <param name="Key">键</param>
        public string re(string Section, string Key) {
            string re = "";
            try {
                StringBuilder temp = new StringBuilder(50000);
                int i = GetPrivateProfileString(Section, Key, "", temp, 50000, this.inipath);
                re = StringHelper.DecryptData(temp.ToString());
            } catch { }

            return re;
        }
        /// <summary>
        /// 验证文件是否存在
        /// </summary>
        /// <returns>布尔值</returns>
        public bool ExistINIFile() {
            return File.Exists(inipath);
        }


    }
}
