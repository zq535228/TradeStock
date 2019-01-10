using System;
using System.Collections.Generic;
using System.Text;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using System.Net;
using System.Collections;

namespace org.jiechan.service {
    public class IPHelper {



        public static string GetIP() {
            string re = "";
            string referer = "http://www.ip.cn/";
            CookieCollection cookie = new CookieCollection();
            string html = new RzHttp().httpGET(referer, ref cookie);

            string regex = "<code>(.*?)</code>";
            string tmp = RegexHelper.getMatch(html, regex, 1);
            if (IsRightIP(tmp)) {
                re = tmp;
            }
            return re;
        }

        public static string GetLocation() {
            string referer = "http://www.ip.cn/";
            CookieCollection cookie = new CookieCollection();
            string html = new RzHttp().httpGET(referer, ref cookie);

            string regex = "来自：(.*?)</p>";
            string tmp = RegexHelper.getMatch(html, regex, 1);
            return tmp;
        }

        public static bool IsAOK(string domain) {
            string hostname = Dns.GetHostName();//得到本机名   
            IPHostEntry localhost = Dns.GetHostEntry(hostname);
            ArrayList al = new ArrayList();
            for (int i = 0; i < localhost.AddressList.Length; i++) {
                al.Add(localhost.AddressList[i].ToString());
            }

            string ip = GetIP();

            bool re = false;
            try {
                IPAddress[] IPs = Dns.GetHostAddresses(domain);
                for (int i = 0; i < IPs.Length; i++) {
                    if (al.Contains(IPs[i].ToString()) || IPs[i].ToString() == ip) {
                        re = true;
                        break;
                    }
                }

            } catch (System.Exception ex) {
                EchoHelper.EchoException(ex);
            }

            return re;
        }

        public static ArrayList GetIP(string domain) {
            ArrayList res = new ArrayList();
            IPAddress[] IPs = Dns.GetHostAddresses(domain);
            for (int i = 0; i < IPs.Length; i++) {
                res.Add(IPs[i].ToString());
            }
            return res;
        }

        public void test() {
            string domain = "www.renzhe.org";
            string ip = "127.0.0.1";
            ArrayList sts = GetIP(domain);

        }

        /// <summary>
        /// 判断是否为正确的IP地址
        /// </summary>
        /// <param name="strIPadd">需要判断的字符串</param>
        /// <returns>true = 是 false = 否</returns>
        public static bool IsRightIP(string strIPadd) {
            //利用正则表达式判断字符串是否符合IPv4格式
            if (Regex.IsMatch(strIPadd, "[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}")) {
                //根据小数点分拆字符串
                string[] ips = strIPadd.Split('.');
                if (ips.Length == 4 || ips.Length == 6) {
                    //如果符合IPv4规则
                    if (System.Int32.Parse(ips[0]) < 256 && System.Int32.Parse(ips[1]) < 256 & System.Int32.Parse(ips[2]) < 256 & System.Int32.Parse(ips[3]) < 256)
                        //正确
                        return true;
                    //如果不符合
                    else
                        //错误
                        return false;
                } else
                    //错误
                    return false;
            } else
                //错误
                return false;
        }

        /// <summary>
        /// 尝试Ping指定IP是否能够Ping通
        /// </summary>
        /// <param name="strIP">指定IP</param>
        /// <returns>true 是 false 否</returns>
        public static bool IsPingIP(string strIP) {
            try {
                //创建Ping对象
                Ping ping = new Ping();
                //接受Ping返回值
                PingReply reply = ping.Send(strIP, 1000);
                if (null != reply && null != reply.Address && reply.RoundtripTime < 1000) {
                    return true;
                }
                //Ping通
                return false;
            } catch {
                //Ping失败
                return false;
            }
        }


    }
}
