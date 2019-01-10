using System;
using System.Collections.Generic;
using org.jiechan.service.cn.org.renzhe.u.member;
using org.jiechan.service.cn.org.renzhe.u.shopx3;

namespace org.jiechan.service {
    public class WebServHelper {

        private Member member;
        private ShopX3 shop;
        private int retry = 0;

        public WebServHelper() {
            retry = 0;
            System.Net.ServicePointManager.Expect100Continue = false;
            if (member == null) {
                member = new Member();
                member.Timeout = 50000;
                member.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:5.0) Gecko/20100101 Firefox/5.0";
            }

            if (shop == null) {
                shop = new ShopX3();
                shop.Timeout = 50000;
                shop.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:5.0) Gecko/20100101 Firefox/5.0";
            }
        }

        /// <summary>
        /// 用忍者X4的方式来验证用户登录。
        /// </summary>
        /// <param name="modelstr">序列化加密后的member</param>
        /// <returns>序列化加密后的member</returns>
        public byte[] ValidateMysql(byte[] modelstr) {
            byte[] str = new byte[1];
            try {

                str = member.ValidateMysql(modelstr);
            } catch (Exception ex) {
                ++retry;

                if (retry < -3) {
                    this.ValidateMysql(modelstr);
                }
                if (ex.Message.Contains("无法解析此远程名称") || ex.Message.Contains("The remote name could not be resolved")) {
                    EchoHelper.Echo("请检查您的网络，可以通过ping命令查看当前网站状态：ping x3.renzhe.org！", "网络不通", EchoHelper.EchoType.红色信息);
                }
                EchoHelper.EchoException(ex);
            }
            return str;


        }

        /// <summary>
        /// 用户验证心跳方法。
        /// </summary>
        public void MemberDoit() {
            member.DoitAsync();
        }

        /// <summary>
        /// 获取市场中的所有的抓取模块。
        /// </summary>
        /// <returns></returns>
        public ModelPick[] GetAllPickModules() {
            ModelPick[] slist = new ModelPick[] { };
            try {
                slist = shop.GetAllPickModules();
            } catch (Exception ex) {
                ++retry;
                if (retry < -3) {
                    this.shop.GetAllPickModules();
                }
                EchoHelper.EchoException(ex);
            }
            return slist;
        }

        /// <summary>
        /// 上传本地的抓取模块到云端
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fileClassStr"></param>
        /// <param name="mtype"></param>
        /// <returns></returns>
        public bool UploadClassStr(string fileName, string fileClassStr, mType mtype) {
            bool res = false;
            try {
                shop.UploadClassStr(fileName, fileClassStr, mtype);
            } catch (Exception ex) {
                ++retry;
                if (retry < -3) {
                    this.shop.UploadClassStr(fileName, fileClassStr, mtype);
                }
            }

            return res;

        }

        /// <summary>
        /// 获取抓取模块，也就是下载抓取模块。
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="mtype"></param>
        /// <returns></returns>
        public string GetClassStr(string fileName, mType mtype) {
            string res = "";
            try {
                res = shop.GetClassStr(fileName, mtype);
            } catch (Exception ex) {
                ++retry;
                if (retry < -3) {
                    this.shop.GetClassStr(fileName, mtype);
                }
                EchoHelper.EchoException(ex);
            }
            return res;
        }

        /// <summary>
        /// 删除模块
        /// </summary>
        /// <param name="classMemberObj"></param>
        /// <param name="fileName"></param>
        /// <param name="mtype"></param>
        /// <returns></returns>
        public bool Delete(string classMemberObj, string fileName, mType mtype) {
            bool res = false;
            try {
                res = shop.Delete(classMemberObj, fileName, mtype);
            } catch (Exception ex) {
                ++retry;
                if (retry < -3) {
                    this.shop.Delete(classMemberObj, fileName, mtype);
                }
                EchoHelper.EchoException(ex);
            }
            return res;


        }

        /// <summary>
        /// 重命名
        /// </summary>
        /// <param name="classMemberObj"></param>
        /// <param name="fileName"></param>
        /// <param name="fileNewName"></param>
        /// <param name="mtype"></param>
        /// <returns></returns>
        public bool ReName(string classMemberObj, string fileName, string fileNewName, mType mtype) {
            bool res = false;
            try {
                res = shop.ReName(classMemberObj, fileName, fileNewName, mtype);
            } catch (Exception ex) {
                ++retry;
                if (retry < -3) {
                    this.shop.ReName(classMemberObj, fileName, fileNewName, mtype);
                }
                EchoHelper.EchoException(ex);
            }
            return res;
        }

        /// <summary>
        /// 商店的心跳方法
        /// </summary>
        public void ShopX3Doit() {
            shop.doitAsync();
        }

    }
}
