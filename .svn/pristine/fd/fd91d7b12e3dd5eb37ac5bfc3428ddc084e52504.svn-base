using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace org.jiechan.service {
    public class FastVerCode {

        [DllImport("FastVerCode.dll")]
        private static extern string RecYZM(string strYZMPath , string strVcodeUser , string strVcodePass);
        [DllImport("FastVerCode.dll")]
        private static extern void ReportError(string strVcodeUser , string strDaMaWorker);

        [DllImport("FastVerCode.dll")]
        private static extern string RecByte(byte[] b , int len , string strVcodeUser , string strVcodePass);

        [DllImport("FastVerCode.dll")]
        private static extern string GetUserInfo(string strVcodeUser , string strVcodePass);

        public static string GetVerCode(byte[] imgbyte , string uname , string upass) {
            string re = "";
            try {
                re = RecByte(imgbyte , imgbyte.Length , uname , upass);
            } catch(Exception ex) {
                EchoHelper.EchoException(ex);
            }
            return re;
        }

        public static string GetLeftNum(string uname , string upass) {
            string re = "";
            try {
                re = GetUserInfo(uname , upass);
            } catch(Exception ex) {
                EchoHelper.EchoException(ex);
            }
            return re;

        }


    }
}
