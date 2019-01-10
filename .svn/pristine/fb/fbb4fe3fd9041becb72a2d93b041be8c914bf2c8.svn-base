using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.jiechan.main.models;
using org.jiechan.service;
using System.Net;
using System.Drawing;

namespace org.jiechan.main.core {

    /// <summary>
    /// 获取股票的详细信息
    /// http://finance.sina.com.cn/realstock/company/sz002739/nc.shtml
    /// </summary>
    public class sinahq {

        private sinahq() {
            co = new CookieCollection();
        }

        private static readonly object locker = new object();
        private static sinahq hq;
        private CookieCollection co;

        public static sinahq GetInstance() {

            if (hq == null) {
                lock (locker) {
                    if (hq == null) {
                        hq = new sinahq();
                    }

                }
            }
            return hq;
        }

        private string attachcode(string code) {
            
            if (code.StartsWith("6")) {
                code = "sh" + code;
            }
            if (code.StartsWith("0")) {
                code = "sz" + code;
            }
            if (code.StartsWith("3")) {
                code = "sz" + code;
            }
            if (code.StartsWith("5")) {
                code = "sh" + code;
            }
            if (code.StartsWith("1")) {
                code = "sz" + code;
            }

            return code;
        }

        public stockmodel stockinfo(string code) {
            stockmodel model = new stockmodel();

            string url = "http://hq.sinajs.cn/list=" + attachcode(code.ToString());
            string html = new RzHttp().WebClient(url);
            string[] ss = html.Split('"');
            string[] ss2 = ss[1].Split(',');

            model.name = ss2[0];
            model.codenum = code;
            model.price = float.Parse(ss2[3]);

            return model;

        }

        /// <summary>
        /// s_sh000001
        /// s_sz399001
        /// s_sz399006
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public stockmodel marketindex(string code) {
            stockmodel model = new stockmodel();

            string url = "http://hq.sinajs.cn/list=" + attachcode(code.ToString());
            string html = new RzHttp().WebClient(url);
            string[] ss = html.Split('"');
            string[] ss2 = ss[1].Split(',');

            model.name = ss2[0];
            //model.codenum = code;
            model.price = float.Parse(ss2[1]);

            return model;

        }


        public Bitmap getbitmap(string code) {
            code = attachcode(code);

            string requestUriString = "http://image.sinajs.cn/newchart/daily/n/" + code + ".gif";
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(requestUriString);
            httpWebRequest.Method = "GET";
            httpWebRequest.Timeout = 2000;
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            return new Bitmap(httpWebResponse.GetResponseStream());
        }


        // 0：”大秦铁路”，股票名字；
        // 1：”27.55″，今日开盘价；
        // 2：”27.25″，昨日收盘价；
        // 3：”26.91″，当前价格；
        // 4：”27.55″，今日最高价；
        // 5：”26.20″，今日最低价；
        // 6：”26.91″，竞买价，即“买一”报价；
        // 7：”26.92″，竞卖价，即“卖一”报价；
        // 8：”22114263″，成交的股票数，由于股票交易以一百股为基本单位，所以在使用时，通常把该值除以一百；
        // 9：”589824680″，成交金额，单位为“元”，为了一目了然，通常以“万元”为成交金额的单位，所以通常把该值除以一万；
        // 10：”4695″，“买一”申请4695股，即47手；
        // 11：”26.91″，“买一”报价；
        // 12：”57590″，“买二”
        // 13：”26.90″，“买二”
        // 14：”14700″，“买三”
        // 15：”26.89″，“买三”
        // 16：”14300″，“买四”
        // 17：”26.88″，“买四”
        // 18：”15100″，“买五”
        // 19：”26.87″，“买五”
        // 20：”3100″，“卖一”申报3100股，即31手；
        // 21：”26.92″，“卖一”报价
        // (22, 23), (24, 25), (26,27), (28, 29)分别为“卖二”至“卖四的情况”
        // 30：”2008-01-11″，日期；
        // 31：”15:05:32″，时间；


    }
}
