using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;

namespace org.jiechan.service {
    public class RzCookies {

        /// <summary>
        /// 把已有的CookieCollection，+ url关联后，返回CookieContainer
        /// </summary>
        /// <param name="mycookie"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static CookieContainer CookieContainer(CookieCollection mycookie, string url) {
            CookieContainer cookieContainer = new CookieContainer();
            Uri uri = new Uri(url);
            foreach (Cookie cookie in mycookie) {
                bool flag = ("." + uri.Host).Contains(cookie.Domain);
                bool flag2 = uri.PathAndQuery.Contains(cookie.Path);
                if (flag && flag2) {
                    cookieContainer.Add(new Cookie(cookie.Name, cookie.Value, cookie.Path, uri.Host));
                }
            }
            return cookieContainer;
        }

        public static void UpCookie(ref CookieCollection mycookie, string url, string headerCookies, CookieCollection newcookie) {
            if (headerCookies == null) {
                headerCookies = "";
            }



            List<RzModelCookie> tmp = new List<RzModelCookie>();

            for (int i = 0; i < mycookie.Count; i++) {
                RzModelCookie rzcookie = new RzModelCookie();
                rzcookie.CookieName = mycookie[i].Name;
                rzcookie.Value = mycookie[i].Value;
                rzcookie.Path = mycookie[i].Path;
                rzcookie.Domain = mycookie[i].Domain;
                rzcookie.Expires = mycookie[i].Expires;

                tmp.Add(rzcookie);
            }

            ArrayList arrayList = strtocookie(url, headerCookies);
            for (int i = 0; i < arrayList.Count; i++) {

                Cookie cookie = (Cookie)arrayList[i];

                RzModelCookie current = tmp.Find(delegate(RzModelCookie c) {
                    return c.CookieName == cookie.Name && c.Value == cookie.Value;
                });

                if (current != null) {
                    tmp.Remove(current);
                }

                RzModelCookie current_todel = tmp.Find(delegate(RzModelCookie c) {
                    return c.CookieName == cookie.Name && c.Value.ToLower() == "deleted";
                });
                if (current_todel != null) {
                    tmp.Remove(current_todel);
                }

                RzModelCookie rzcookie = new RzModelCookie();
                rzcookie.CookieName = cookie.Name;
                rzcookie.Value = cookie.Value;
                rzcookie.Path = cookie.Path;
                rzcookie.Domain = cookie.Domain;
                rzcookie.Expires = cookie.Expires;

                tmp.Add(rzcookie);
            }

            if (newcookie.Count > 0) {
                foreach (Cookie cookie in newcookie) {

                    RzModelCookie current = tmp.Find(delegate(RzModelCookie c) {
                        return c.CookieName == cookie.Name && c.Value == cookie.Value;
                    });

                    if (current != null) {
                        tmp.Remove(current);
                    }

                    RzModelCookie current_todel = tmp.Find(delegate(RzModelCookie c) {
                        return c.CookieName == cookie.Name && c.Value.ToLower() == "deleted";
                    });
                    if (current_todel != null) {
                        tmp.Remove(current_todel);
                    }

                    RzModelCookie rzcookie = new RzModelCookie();
                    rzcookie.CookieName = cookie.Name;
                    rzcookie.Value = cookie.Value;
                    rzcookie.Path = cookie.Path;
                    rzcookie.Domain = cookie.Domain;
                    rzcookie.Expires = cookie.Expires;

                    tmp.Add(rzcookie);
                }
            }

            mycookie = new CookieCollection();
            for (int i = 0; i < tmp.Count; i++) {
                if (tmp[i].Value != "deleted") {
                    Cookie cc = new Cookie();
                    cc.Name = tmp[i].CookieName;
                    cc.Value = tmp[i].Value;
                    cc.Path = tmp[i].Path;
                    cc.Domain = tmp[i].Domain;
                    cc.Expires = tmp[i].Expires;
                    mycookie.Add(cc);
                }
            }

        }

        //         public static void UpCookie(ref CookieCollection mycookie, string url, string headerCookies) {
        //             if (headerCookies == null) {
        //                 headerCookies = "";
        //             }
        //             ArrayList arrayList = strtocookie(url, headerCookies);
        //             for (int i = 0; i < arrayList.Count; i++) {
        //                 Cookie cookie = (Cookie)arrayList[i];
        //                 mycookie.Add(cookie);
        //             }
        //         }

        //public static void UpCookie(string url, string setCookie) {
        //    if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(setCookie)) {
        //        return;
        //    }
        //    Uri uri = new Uri(url);
        //    for (int i = 0; i < setCookie.Split(';').Length; i++) {
        //        Cookie cookie = new Cookie();
        //        cookie.Domain = "AAAA";
        //        string cookeValue = setCookie.Split(';')[i];
        //        if (cookeValue.Contains("=")) {
        //            string cookieName = ReverseByArray(ReverseByArray(cookeValue.Split('=')[0].Trim()).Trim());
        //            string cookieValue = "";
        //            for (int k = 1; k < cookeValue.Split('=').Length; k++) {
        //                if (k == cookeValue.Split('=').Length - 1) {
        //                    cookieValue += cookeValue.Split('=')[k];
        //                } else {
        //                    cookieValue = cookieValue + cookeValue.Split('=')[k] + "=";
        //                }
        //            }
        //            if (cookieName.ToLower() == "domain") {
        //                if (cookieValue.Substring(0, 1) != ".") {
        //                    cookie.Domain = "." + cookieValue;
        //                } else {
        //                    cookie.Domain = cookieValue;
        //                }
        //            } else {
        //                if (cookieName.ToLower() == "path") {
        //                    cookie.Path = cookieValue;
        //                } else {
        //                    if (cookieName.ToLower() == "version") {
        //                        cookie.Version = Convert.ToInt32(cookieValue);
        //                    } else {
        //                        if (cookieName.ToLower() != "expires" && cookieName.ToLower() != "max-age") {
        //                            cookie.Name = cookieName;
        //                            cookie.Value = cookieValue;
        //                        }
        //                    }
        //                }
        //            }

        //        }
        //        if (cookie.Domain == "AAAA") {
        //            cookie.Domain = uri.Host;
        //        }
        //        if (cookie.Name != "") {
        //            try {
        //                _cookies.Add(cookie);
        //            } catch {
        //            }
        //        }
        //    }


        //}

        //public static void UpCookie(string url, string setCookie, CookieCollection responseCookies) {
        //    if (setCookie == null) {
        //        setCookie = "";
        //    }
        //    ArrayList arrayList = strtocookie(url, setCookie);
        //    for (int i = 0; i < arrayList.Count; i++) {
        //        Cookie cookie = (Cookie)arrayList[i];
        //        _cookies.Add(cookie);
        //    }

        //    if (responseCookies != null && responseCookies.Count > 0) {
        //        foreach (Cookie cookie2 in responseCookies) {
        //            _cookies.Add(cookie2);
        //        }
        //    }
        //}


        private static string ReverseByArray(string original) {
            char[] array = original.ToCharArray();
            Array.Reverse(array);
            return new string(array);
        }

        private static ArrayList strtocookie(string url, string str) {
            ArrayList arrayList = new ArrayList();
            Uri uri;

            try {
                uri = new Uri(url);
            } catch {
                return new ArrayList();
            }

            str = str.Replace(", ", "  ");
            if (str.Contains(",")) {
                string[] array = str.Split(',');
                for (int i = 0; i < array.Length; i++) {
                    string cookies_str = array[i];
                    if (cookies_str.Contains(";")) {
                        for (int j = 0; j < cookies_str.Split(';').Length; j++) {
                            Cookie cookie = new Cookie();
                            cookie.Domain = "AAAA";
                            string cookies_tmp = cookies_str.Split(';')[j];
                            if (cookies_tmp.Contains("=")) {
                                string tmp_name = ReverseByArray(ReverseByArray(cookies_tmp.Split('=')[0].Trim()).Trim());
                                string tmp_value = "";
                                for (int k = 1; k < cookies_tmp.Split('=').Length; k++) {
                                    if (k == cookies_tmp.Split('=').Length - 1) {
                                        tmp_value += cookies_tmp.Split('=')[k];
                                    } else {
                                        tmp_value = tmp_value + cookies_tmp.Split('=')[k] + "=";
                                    }
                                }
                                if (tmp_name.ToLower() == "domain") {
                                    if (tmp_value.Substring(0, 1) != ".") {
                                        cookie.Domain = "." + tmp_value;
                                    } else {
                                        cookie.Domain = tmp_value;
                                    }
                                } else {
                                    if (tmp_name.ToLower() == "path") {
                                        cookie.Path = tmp_value;
                                    } else {
                                        if (tmp_name.ToLower() == "version") {
                                            cookie.Version = Convert.ToInt32(tmp_value);
                                        } else {
                                            if (tmp_name.ToLower() != "expires" && tmp_name.ToLower() != "max-age") {
                                                cookie.Name = tmp_name;
                                                cookie.Value = tmp_value;
                                            }
                                        }
                                    }
                                }
                            }
                            if (cookie.Domain == "AAAA") {
                                cookie.Domain = "." + uri.Host;
                                //cookie.Domain = uri.Host;
                            }
                            if (cookie.Name != "") {
                                try {
                                    arrayList.Add(cookie);
                                } catch {
                                }
                            }
                        }

                    }
                }
            } else {
                string cookie_str = str;
                if (cookie_str.Contains(";")) {
                    for (int l = 0; l < cookie_str.Split(';').Length; l++) {
                        Cookie cookie = new Cookie();
                        cookie.Domain = "AAAA";
                        string cookie_tmp = cookie_str.Split(';')[l];
                        if (cookie_tmp.Contains("=")) {
                            string tmp_name = ReverseByArray(ReverseByArray(cookie_tmp.Split('=')[0].Trim()).Trim());
                            string tmp_value = "";
                            for (int m = 1; m < cookie_tmp.Split('=').Length; m++) {
                                if (m == cookie_tmp.Split('=').Length - 1) {
                                    tmp_value += cookie_tmp.Split('=')[m];
                                } else {
                                    tmp_value = tmp_value + cookie_tmp.Split('=')[m] + "=";
                                }
                            }
                            if (tmp_name.ToLower() == "domain") {
                                if (tmp_value.Substring(0, 1) != ".") {
                                    cookie.Domain = "." + tmp_value;
                                } else {
                                    cookie.Domain = tmp_value;
                                }
                            } else {
                                if (tmp_name.ToLower() == "path") {
                                    cookie.Path = tmp_value;
                                } else {
                                    if (tmp_name.ToLower() == "version") {
                                        cookie.Version = Convert.ToInt32(tmp_value);
                                    } else {
                                        if (tmp_name.ToLower() != "expires" && tmp_name.ToLower() != "max-age") {
                                            cookie.Name = tmp_name;
                                            cookie.Value = tmp_value;
                                        }
                                    }
                                }
                            }
                        }
                        if (cookie.Domain == "AAAA") {
                            cookie.Domain = "." + uri.Host;
                            //cookie.Domain = uri.Host;
                        }
                        if (cookie.Name != "") {
                            try {
                                arrayList.Add(cookie);
                            } catch {
                            }
                        }
                    }

                }
            }
            return arrayList;
        }

    }



    public class RzModelCookie {

        public string CookieName {
            get;
            set;
        }

        public string Value {
            get;
            set;
        }
        public string Path {
            get;
            set;
        }

        public string Domain {
            get;
            set;
        }

        public DateTime Expires {
            get;
            set;
        }


    }
}