using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace org.jiechan.service {
    public class PathHelper {

        public static string Desktop {
            get {
                return Environment.GetFolderPath(Environment.SpecialFolder.Desktop);//桌面路径
            }
        }

        public static string StartupPath {
            get { return Application.StartupPath; }
        }

        public static string TaskPath {
            get {
                if (!FilesHelper.DirectoryExist(StartupPath + "\\Task")) {
                    FilesHelper.CreateDirectory(StartupPath + "\\Task");
                }
                return StartupPath + "\\Task";
            }
        }

        public static string TempPath {
            get {
                if (!FilesHelper.DirectoryExist(StartupPath + "\\Temp")) {
                    FilesHelper.CreateDirectory(StartupPath + "\\Temp");
                }

                return StartupPath + "\\Temp";
            }
        }

        public static string ModulePath {
            get {
                if (!FilesHelper.DirectoryExist(StartupPath + "\\Module")) {
                    FilesHelper.CreateDirectory(StartupPath + "\\Module");
                }
                return StartupPath + "\\Module";
            }
        }

        public static string LogPath {
            get {
                if (!FilesHelper.DirectoryExist(StartupPath + "\\Log")) {
                    FilesHelper.CreateDirectory(StartupPath + "\\Log");
                }
                return StartupPath + "\\Log";
            }
        }

        /// <summary>
        /// 应用程序的\\Config路径,后面没有\\
        /// </summary>
        public static string ConfigPath {
            get {
                if (!FilesHelper.DirectoryExist(StartupPath + "\\Config")) {
                    FilesHelper.CreateDirectory(StartupPath + "\\Config");
                }
                return StartupPath + "\\Config";
            }
        }

        /// <summary>
        /// 给数据db文件，定位，返回例如： F:\x4\build\Config\7fa19f67d27b39d0.db
        /// </summary>
        public static string WinServDbPathFile(string filename) {
            if (filename.ToLower().Contains(".db")) {
                return PathHelper.ConfigPath + "\\" + filename;
            } else {
                return PathHelper.ConfigPath + "\\" + filename + ".db";
            }

        }


        /// <summary>
        /// 16位的db文件，定义用于，服务端的db储存。
        /// </summary>
        public static string IpDbPathFile16(string ip) {
            return PathHelper.ConfigPath + "\\" + MD5Helper.MD5(ip) + ".db";
        }

        /// <summary>
        /// 8位的db文件，定义用于，客户端的db储存。
        /// </summary>
        public static string WinServDbPathFile8() {
            return PathHelper.ConfigPath + "\\" + HardWare.getHardCode16() + ".db";
        }


        public static string SentencePath {
            get {
                if (!FilesHelper.DirectoryExist(StartupPath + "\\Sentence")) {
                    FilesHelper.CreateDirectory(StartupPath + "\\Sentence");
                }
                return StartupPath + "\\Sentence";
            }
        }
    }
}
