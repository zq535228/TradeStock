using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Web.Security;

namespace org.jiechan.service {

    public class MD5Helper {

        /// <summary>
        /// 获取一个MD5的16位加密字符串.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string MD5(string str) {
            string md5 = MD5(str, 16);
            return md5;
        }


        /// <summary>
        /// 生成MD5加密字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="code">8,16,32</param>
        /// <returns></returns>
        public static string MD5(string str, int code) {
            string md5 = "";
            if (code == 16) {
                md5 = FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").ToLower().Substring(8, 16);
            } else if (code == 8) {
                md5 = FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").ToLower().Substring(8, 8);
            } else {
                md5 = FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").ToLower();
            }
            return md5;
        }


        public static string GenerateKey() {
            DESCryptoServiceProvider desCrypto = (DESCryptoServiceProvider)DESCryptoServiceProvider.Create();
            return ASCIIEncoding.ASCII.GetString(desCrypto.Key);
        }

        public static string MD5Encrypt(string pToEncrypt, string sKey) {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.Default.GetBytes(pToEncrypt);
            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray()) {
                ret.AppendFormat("{0:X2}", b);
            }
            ret.ToString();
            return ret.ToString();
        }

        ///MD5解密
        public static string MD5Decrypt(string pToDecrypt, string sKey) {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();

            byte[] inputByteArray = new byte[pToDecrypt.Length / 2];
            for (int x = 0; x < pToDecrypt.Length / 2; x++) {
                int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
                inputByteArray[x] = (byte)i;
            }

            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            return System.Text.Encoding.Default.GetString(ms.ToArray());
        }


        public static string EncodeSHA1(string Source_String) {
            byte[] StrRes = Encoding.Default.GetBytes(Source_String);
            HashAlgorithm iSHA = new SHA1CryptoServiceProvider();
            StrRes = iSHA.ComputeHash(StrRes);
            StringBuilder EnText = new StringBuilder();
            foreach (byte iByte in StrRes) {
                EnText.AppendFormat("{0:x2}", iByte);
            }
            return EnText.ToString();
        }

        public static string EncodeSHA2(string Source_String) {
            if (!String.IsNullOrEmpty(Source_String)) {
                return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(Source_String, "SHA1").ToLower();
            } else {
                return string.Empty;
            }
        }


        public static string GetSHA1(string str) {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in System.Security.Cryptography.SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(str))) {
                sb.Append(b.ToString("X2"));
            }
            return sb.ToString();
        }

        public static string EncodeBase64(string code) {
            string encode = "";
            try {
                byte[] bytes = Encoding.GetEncoding(Encoding.UTF8.CodePage).GetBytes(code);
                encode = Convert.ToBase64String(bytes);
            } catch {
                encode = code;
            }
            return encode;
        }


        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="code">密文</param>
        /// <returns>明文</returns>
        public static string DecodeBase64(string code) {
            string decode = "";
            try {
                byte[] bytes = Convert.FromBase64String(code);
                decode = Encoding.GetEncoding(Encoding.UTF8.CodePage).GetString(bytes);
            } catch {
                decode = code;
            }
            return decode;
        }


    }

}
