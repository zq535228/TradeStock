using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace org.jiechan.service {
    public class SiteInfoBuild {
        #region 返回站点关键词组合的各种信息

        public static string getSiteName(string desc) {
            string re = "";
            ArrayList al = getAllKeys(desc);

            if (al.Count < 4) {
                re = "关键词太少了。";
                return re;
            }

            if (al.Count > 30) {
                int i = StringHelper.getRandNextNum(al.Count);
                int j = StringHelper.getRandNextNum(al.Count);
                int k = StringHelper.getRandNextNum(al.Count);

                re = al[i] + "_" + al[j] + "_" + al[k];
            } else {
                re = al[0] + "_" + al[1] + "_" + al[2];
            }

            return re;
        }


        public static string getSiteKeys(string desc) {
            string re = "";
            ArrayList al = getAllKeys(desc);

            if (al.Count < 4) {
                re = "关键词太少了。";
                return re;
            }

            if (al.Count > 30) {

                int i = StringHelper.getRandNextNum(al.Count);
                int j = StringHelper.getRandNextNum(al.Count);
                int k = StringHelper.getRandNextNum(al.Count);
                int p = StringHelper.getRandNextNum(al.Count);
                re = al[i] + "," + al[j] + "," + al[k] + "," + al[p];

            } else {
                re = al[0] + "," + al[1] + "," + al[2];
            }

            return re;
        }

        public static string getSiteKeys(string desc, int num) {
            string re = "";
            ArrayList al = getAllKeys(desc);

            if (al.Count < 4) {
                re = "关键词太少了。";
                return re;
            }

            for (int i = 0; i < num; i++) {
                re += al[i] + ",";
            }

            return re.TrimEnd(',');
        }


        public static string getSiteDesc(string desc) {
            string re = "";
            ArrayList aldesc = new ArrayList();
            string re1 = "本网站是以【k1】、【k2】、【k3】为宗旨的站点，主要面向【k4】群体，网站的一些比较特色的功能，用户能在网站里找到【k5】、【k6】等信息介绍。";
            string re2 = "本网站通明的火把下把【k1】、【k2】、【k3】，映出了那人的容颜，虽是【k4】身着南阳军服，可模样却是女子清灵的模样【k5】、【k6】，身上绕了几环的绳索，双手背紧紧勒在身后，想挣扎却异常的无力：“将军，救我！”无法挣脱的女子，只得大声地对城下呼救。";
            string re3 = "【k1】、【k2】、【k3】娇小的将官身躯微微地颤了又颤，【k4】,根本不曾给萧晴通风报信，虽然她贵为萧晴国的公主，【k5】、【k6】、可是她没有做，她不曾出卖过南阳，因为，她爱上了南阳的大将军，更是听信了对方花言巧语的哄骗，他说过，他没有妻妾，他说过，只要战争稍缓，他便会携她去萧晴国求亲，他还说过……如今，怎么一切都全然不作数了，怎么……";
            string re4 = "【k1】、【k2】、【k3】果不其然，同样的身披金甲，【k4】可是异于那日城墙上的铁马金戈：“公主，您是在唤臣吗？”【k5】、【k6】银色面具下露出的依旧是那双风情万种的桃花眼，只是如今他的双眼暗淡无光，早已被疲惫和绝望磨光了菱角。";
            aldesc.Add(re1);
            aldesc.Add(re2);
            aldesc.Add(re3);
            aldesc.Add(re4);

            re = ArrayHelper.getRandOneFromArray(aldesc);

            ArrayList al = getAllKeys(desc);

            int i = StringHelper.getRandNextNum(al.Count);
            int j = StringHelper.getRandNextNum(al.Count);
            int k = StringHelper.getRandNextNum(al.Count);
            int o = StringHelper.getRandNextNum(al.Count);
            int p = StringHelper.getRandNextNum(al.Count);
            int q = StringHelper.getRandNextNum(al.Count);

            re = re.Replace("【k1】", al[i].ToString());
            re = re.Replace("【k2】", al[j].ToString());
            re = re.Replace("【k3】", al[k].ToString());
            re = re.Replace("【k4】", al[o].ToString());
            re = re.Replace("【k5】", al[p].ToString());
            re = re.Replace("【k6】", al[q].ToString());

            return re;
        }

        public static ArrayList getCategoryKeys(string desc) {
            ArrayList al = getAllKeys(desc);

            ArrayList re = new ArrayList();

            if (al.Count > 30) {
                for (int i = 0; i < 10; i++) {
                    re.Add(ArrayHelper.getRandOneFromArray(al));
                }
            } else {
                for (int i = 0; i < al.Count; i++) {
                    re.Add(al[i].ToString());
                }

            }
            return re;
        }

        #region 获取所有的Keys，返回ArrayList
        private static ArrayList getAllKeys(string desc) {
            ArrayList al = new ArrayList();

            if (desc.Contains("\n")) {
                string[] sts = desc.Split('\n');
                for (int i = 0; i < sts.Length; i++) {
                    al.Add(sts[i].Trim());
                }
            } else {
                string[] sts = desc.Split(',');
                for (int i = 0; i < sts.Length; i++) {
                    al.Add(sts[i].Trim());
                }
            }
            return al;

        }
        #endregion

        #endregion

    }
}
