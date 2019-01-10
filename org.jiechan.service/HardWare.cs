using System;
using System.Management;

namespace org.jiechan.service {
    public class HardWare {

        /// <summary>
        /// 返回硬盘的编码信息，例如：服务端的可以用 wser.FilePathInfo = HardWare.getHardCode8()+".db";
        /// </summary>
        /// <returns></returns>
        public static string getHardCode8() {

            string code = "";
            string tmp = FilesHelper.Read_File(PathHelper.TempPath + "\\hardcode2.txt");
            if (string.IsNullOrEmpty(tmp)) {
                code = HardWare.GetCpuID() + HardWare.GetHardID();
                code = MD5Helper.MD5(code, 8);
                FilesHelper.Write_File(PathHelper.TempPath + "\\hardcode2.txt", code);
            } else {
                code = tmp;
            }
            return code;
        }

        /// <summary>
        /// 返回硬盘的编码信息，例如：客户端的可以用 HardWare.getHardCode16()+".db";
        /// </summary>
        /// <returns></returns>
        public static string getHardCode16() {

            string code = "";
            string tmp = FilesHelper.Read_File(PathHelper.TempPath + "\\hardcode2.txt");
            if (string.IsNullOrEmpty(tmp)) {
                code = HardWare.GetCpuID() + HardWare.GetHardID();
                code = MD5Helper.MD5(code, 16);
                FilesHelper.Write_File(PathHelper.TempPath + "\\hardcode2.txt", code);
            } else {
                code = tmp;
            }
            return code;
        }


        public static string GetHardID() {
            String HDid = "";
            ManagementClass mc = new ManagementClass("Win32_DiskDrive");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc) {
                HDid = (string)mo.Properties["Model"].Value;
            }
            return HDid;
        }

        public static String GetCpuID() {
            try {
                ManagementClass mc = new ManagementClass("Win32_Processor");
                ManagementObjectCollection moc = mc.GetInstances();

                String strCpuID = null;
                foreach (ManagementObject mo in moc) {
                    strCpuID = mo.Properties["ProcessorId"].Value.ToString();
                    break;
                }
                return strCpuID;
            } catch {
                return "";
            }

        }


        public static string GetCpuID_4A() {
            string str = GetCpuID();
            str = str.ToUpper().Substring(0, 4);
            return str;
        }

        public static string GetCpuID_3A() {
            string str = GetCpuID();
            str = str.ToUpper().Substring(0, 3);
            return str;
        }

    }
}
