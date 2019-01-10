using System;
using System.Net;
using System.Text;
using org.jiechan.service;
using System.Threading;

namespace org.jiechan.service {
    public class SeoHelper {


        public static bool isWWW(string url) {
            bool re = false;
            try {
                if (url.Trim().Length > 0) {
                    if (new Uri(url).Host.Contains("www.") || (new Uri(url).Host.Split('.').Length < 3 && new Uri(url).Host.Split('.').Length > 1)) {
                        re = true;
                    }
                }
            } catch {
            }
            return re;
        }

        /// <summary>
        /// 获取某域名的IP
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string getIp(string url) {
            string result = "";
            try {
                url = new Uri(url).Host;
                IPAddress[] IPs = Dns.GetHostAddresses(url);

                for (int i = 0; i < IPs.Length; i++) {
                    string ip = IPs[i].ToString();
                    string v_ip = RegexHelper.getMatch(ip, @"[0-9]+\.[0-9]+\.[0-9]+\.[0-9]+");
                    if (!string.IsNullOrEmpty(v_ip)) {
                        result += v_ip + ",";
                    }
                }
                result = result.Substring(0, result.IndexOf(","));
            } catch (Exception) {
                result = "000.000.000.000";
            }
            return result;
        }

        /// <summary>
        ///  搜索引擎查询
        /// </summary>
        public enum EnumSearchEngine {
            Google = 1,
            Baidu = 2,
        }


        /// <summary>
        ///  获取某站点关键字在某搜索引擎的排名
        /// </summary>
        /// <param name="_engine"></param>
        /// <param name="Url"></param>
        /// <param name="KeyWord"></param>
        /// <returns></returns>
        public static string engineKeyWordinfo(EnumSearchEngine _engine, string Url, string KeyWord) {
            string re = "200+";
            string Html = "";
            string searchUrl = "";
            switch (_engine) {
                case EnumSearchEngine.Baidu:
                    EchoHelper.Echo("关键词【" + KeyWord + "】的排名查询中，请稍后...", "", EchoHelper.EchoType.淡蓝信息);
                    searchUrl = "http://tool.seowhy.com/keywords/ajax?query=" + Url + "&time=1367476741&inajax=true";
                    string postData = "keyword=" + KeyWord + "&searchengine=baidu";
                    CookieCollection cookies = new CookieCollection();
                    Html = new RzHttp().httpPost(searchUrl, postData, ref cookies, searchUrl, Encoding.GetEncoding("utf-8"));
                    re = RegexHelper.getMatch(Html, "list\":{\"(.*?)\":", 1);
                    if (string.IsNullOrEmpty(re)) {
                        re = "100+";
                    }
                    break;
                case EnumSearchEngine.Google:
                    break;
            }
            return re;
        }

        private void engineKeyWordinfoTest() {
            engineKeyWordinfo(EnumSearchEngine.Baidu, "www.langmei-bao.info", "浪美包包");
        }

        /// <summary>
        ///  站点收录信息
        /// </summary>
        /// <param name="_engine"></param>
        /// <returns></returns>
        public static int getBaiduNum(EnumSearchEngine _engine, string url) {
            //EchoHelper.Echo("网站【" + url + "】的收录查询中，请稍后...", "", EchoHelper.EchoType.普通信息);
            string regStart = "找到相关结果数";
            string regEnd = "个";
            string siteUrl = "http://www.baidu.com/s?wd=site%3A" + url;

            CookieCollection cookies = new CookieCollection();
            string html = new RzHttp().httpGET(siteUrl, ref cookies, siteUrl);
            string Result = StringHelper.GetMetaString(html, regStart, regEnd, true);
            Result = Result.Replace(",", "");
            int re = 9999;
            try {
                re = Convert.ToInt32(Result);
            } catch {
                re = 0;
            }
            if (html.Contains("没有找到与")) {
                re = 0;
            }
            return re;
        }


        public static int checkDomain(string domain) {
            if (domain.Contains("http://")) {
                domain = new Uri(domain.Trim()).Host.Replace("www.", "");
            } else {
                domain = domain.Trim();
            }
            int re = 0;
            CookieCollection cookies = new CookieCollection();
            string html = new RzHttp().httpGET("http://whoissoft.com/" + domain, ref cookies);
            if (html.Contains("No match for domain")) {
                re = 1;
            }
            return re;
        }

        public static DateTime getDomainExpired(string domain) {
            if (domain.Contains("http://")) {
                domain = new Uri(domain.Trim()).Host.Replace("www.", "");
            } else {
                domain = domain.Trim().Replace("www.", "");
            }
            int l = domain.Split('.').Length;
            if (l > 2) {
                domain = domain.Split('.')[l - 2] + "." + domain.Split('.')[l - 1];
            }
            DateTime re = DateTime.Now.AddDays(-1);
            CookieCollection cookies = new CookieCollection();
            string html = new RzHttp().httpGET("http://tool.chinaz.com/DomainDel/?wd=" + domain, ref cookies);

            //循环5次，去抓取域名信息
            int i = 0;
            while (html.Contains("没有查询到相应的信息.")) {
                html = new RzHttp().httpGET("http://tool.chinaz.com/DomainDel/?wd=" + domain, ref cookies);
                i++;
                if (i > 10) {
                    break;
                }
            }
            if (html.Contains("域名到期时间")) {
                string edate = RegexHelper.getHtmlRegexText(html, "{域名到期时间</td><td class=\"deltd1\">(.*?)</td></tr>}");
                DateTime dt = new DateTime(Convert.ToInt32(edate.Split('-')[0]), Convert.ToInt32(edate.Split('-')[1]), Convert.ToInt32(edate.Split('-')[2]));
                re = dt;
            }

            return re;
        }

        //获取域名是否已经备案
        public static int getDomainISBeiAN(string domain) {
            int re = 0;
            if (domain.Contains("http://")) {
                domain = new Uri(domain.Trim()).Host.Replace("www.", "");
            } else {
                domain = domain.Trim().Replace("www.", "");
            }
            int l = domain.Split('.').Length;
            if (l > 2) {
                domain = domain.Split('.')[l - 2] + "." + domain.Split('.')[l - 1];
            }
            CookieCollection cookies = new CookieCollection();
            string html = new RzHttp().httpGET("http://www.15so.com/baidu/index.html?type=0&kword=" + domain, ref cookies);
            if (html.Contains("查看详细备案信息")) {
                re = 1;
            }
            return re;
        }


        public void testdate() {
            //getAizhanSeo("www.blueidea.com");
            //             IsDomain("www.renzhe.org");
            //             IsDomain("www.renzhe.org.cn");
            //             IsDomain("renzhe.org.cn");
            //             IsDomain("renzhe.org");
        }


        public static SeoMeta getAizhanSeo(string domain) {
            if (!new RzHttp().IsDomain(domain)) {
                EchoHelper.Echo("域名无法识别，请使用域名作为IIS中站点的名字！", "域名无法识别", EchoHelper.EchoType.红色信息);
                return new SeoMeta();
            }
            SeoMeta re = new SeoMeta();
            re.域名 = domain;

            //基本信息填充
            CookieCollection cookies = new CookieCollection();
            string purl = "http://www.aizhan.com/siteall/" + domain + "/";
            string referer = "http://www.aizhan.com/";
            string html = new RzHttp().httpGET(purl, ref cookies, referer);
            string kz = StringHelper.NoHtml(RegexHelper.getMatch(html, "<span id=\"baidu_date\">(.*?)</span>", 1));
            re.百度快照 = kz.Contains("0000") ? "--" : kz;

            string sl = StringHelper.NoHtml(RegexHelper.getMatch(html, "<td id=\"baidu\">(.*?)</td>", 1));
            re.收录数量 = sl.Contains("img") ? "--" : sl;

            string qz = RegexHelper.getMatch(html, "src=\"http://static.aizhan.com/images/brs/(.*?).gif\">", 1);
            re.百度权重 = qz.Contains("data") ? "0" : qz;
            re.出站链接 = RegexHelper.getMatch(html, "出站链接：(.*?)个", 1);

            string gjc = RegexHelper.getMatch(html, "<td id=\"webpage_keywords\">(.*?)</td>", 1);
            re.关键词 = string.IsNullOrEmpty(gjc) ? "--" : gjc; ;

            string token = RegexHelper.getMatch(html, "token=(.*?)\\&s", 1);
            string gendomain = RegexHelper.getMatch(html, "url_domain = '(.*?)';", 1);

            string time = RegexHelper.getMatch(html, "rn:'(.*?)',", 1);

            purl = "http://www.aizhan.com/ajaxAction/backlink1.php?domain=" + domain + "&rn=" + time + "&cc=" + token;
            referer = "http://www.aizhan.com/siteall/" + domain + "/";
            html = new RzHttp().httpGET(purl, ref cookies, referer);
            re.本站反链 = RegexHelper.getMatch(html, "count\":(.*?),\"items", 1);

            purl = "http://whois.aizhan.com/index.php?r=site/searchDomain&_=" + time + "&s=" + gendomain + "&ajax=yes&update=false&field=base";
            referer = "http://whois.aizhan.com/";
            html = new RzHttp().httpGET(purl, ref cookies, referer);
            string jzsj = RegexHelper.getMatch(html, "创建时间.....................: (.*?)</br>", 1);

            if (!string.IsNullOrEmpty(jzsj) && jzsj.Contains("-")) {
                int y = Convert.ToInt32(jzsj.Split('-')[0]);
                int m = Convert.ToInt32(jzsj.Split('-')[1]);
                int d = Convert.ToInt32(jzsj.Split('-')[2]);
                DateTime dt = new DateTime(y, m, d);
                DateTime dtnow = DateTime.Now;
                re.创建时间 = (dtnow - dt).Days.ToString() + "天";
            } else {
                re.创建时间 = "--";
            }


            re.域名到期 = RegexHelper.getMatch(html, "过期时间.....................: (.*?)</br>", 1);

            if (!string.IsNullOrEmpty(re.关键词)) {
                for (int i = 0; i < re.关键词.Split(',').Length; i++) {
                    string tmpkey = re.关键词.Split(',')[i];
                    if (!tmpkey.Contains("--")) {
                        purl = "http://keywords.aizhan.com//ajaxAction/getkeywordpositionKey.php?domain=" + domain + "&wd=" + tmpkey + "&zs=0&id=0&t=" + time + "&token=" + token + "&s=6382?&jsoncallback=jsonp" + time + "123&pp=FFFFF";
                        purl = StringHelper.PostEnCode(purl, Encoding.UTF8);
                        referer = "http://www.aizhan.com/siteall/" + domain + "/";
                        html = new RzHttp().httpGET(purl, ref cookies, referer);
                        string tmp = RegexHelper.getMatch(html, "content\":\"(.*?)\",", 1);
                        tmp = tmp.Contains("100") ? "100+" : tmp;
                        if (tmp.Contains("100+")) {
                            tmp = tmp.Contains(",") ? StringHelper.SubStringNoDDD(tmp, 0, tmp.IndexOf(",")) : tmp;
                            re.关键词排名 += tmpkey + "→" + tmp + "|";
                        } else if (!string.IsNullOrEmpty(tmp)) {
                            tmp = tmp.Contains(",") ? StringHelper.SubStringNoDDD(tmp, 0, tmp.IndexOf(",")) : tmp;
                            re.关键词排名 += tmpkey + "→" + tmp + "|";
                        }

                    }
                }
                if (re.关键词排名 != null) {
                    re.关键词排名 = re.关键词排名.TrimEnd('|');
                }
                if (string.IsNullOrEmpty(re.关键词排名)) {
                    re.关键词排名 = re.关键词;
                }
            }
            return re;
        }

    }

    [Serializable]
    public class SeoMeta {
        public string 域名 { get; set; }
        public string 收录数量 { get; set; }
        public string 百度权重 { get; set; }
        public string 本站反链 { get; set; }
        public string 出站链接 { get; set; }
        public string 创建时间 { get; set; }
        public string 百度快照 { get; set; }
        public string 域名到期 { get; set; }

        public string 关键词 { get; set; }
        public string 关键词排名 { get; set; }
    }

}
