using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;
using System.Web;
using ComponentFactory.Krypton.Toolkit;
using org.jiechan.service.Balloon;

namespace org.jiechan.service {
    /// <summary>
    /// 输出类
    /// </summary>
    public static class EchoHelper {

        #region 控制台直接输出
        public static bool IsShow = true;
        public static void Hide() {
            ParenthWnd = FindWindow(null, Console.Title);
            IsShow = false;
            ShowWindow(ParenthWnd, 0);
        }

        public static void Show() {
            ParenthWnd = FindWindow(null, Console.Title);
            IsShow = true;
            ShowWindow(ParenthWnd, 5);
        }


        public static void Echo(string content) {
            Echo(content, "", EchoType.淡蓝信息);
        }
        public static void Echo(string content, EchoType echo) {
            Echo(content, "", echo);
        }


        /// <summary>
        /// 输出控制台字符
        /// </summary>
        /// <param name="content">正文内容</param>
        /// <param name="title">标签</param>
        /// <param name="echo">输出方式</param>
        public static void Echo(string content, string title, EchoType echo) {
            if (string.IsNullOrEmpty(content)) {
                return;
            }
            content = content.Replace("\n", "").Replace("\r", "");
            string strall = content;
            content = HttpUtility.UrlDecode(content);
            title = HttpUtility.UrlDecode(title);

            lock ("我锁") {
                switch (echo) {
                    case EchoType.淡蓝信息: {
                            content = StringHelper.SubString(content.Replace("\n", ""), 0, 80).Replace("【", "[").Replace("】", "]");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write(DateTime.Now.ToString("MM-dd HH:mm:ss") + "|");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write("【" + content + "】" + Environment.NewLine);
                            LogHelper.GetInstance().CreateLog("普通信息", title + " " + strall, EchoType.淡蓝信息);
                            break;
                        }
                    case EchoType.绿色信息: {
                            content = StringHelper.SubString(content.Replace("\n", ""), 0, 80).Replace("【", "[").Replace("】", "]");
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(DateTime.Now.ToString("MM-dd HH:mm:ss") + "|");
                            title = string.IsNullOrEmpty(title) ? "任务信息" : title;
                            Console.Write(string.Format("【{0}】：{1}", title, content + Environment.NewLine));
                            LogHelper.GetInstance().CreateLog("任务信息", title + " " + strall, EchoType.绿色信息);
                            break;
                        }
                    case EchoType.红色信息: {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(DateTime.Now.ToString("MM-dd HH:mm:ss") + "|");
                            title = string.IsNullOrEmpty(title) ? "错误信息" : title;
                            Console.Write(string.Format("【{0}】：{1}", title, content + Environment.NewLine));
                            LogHelper.GetInstance().CreateLog("错误信息", title + " " + strall, EchoType.红色信息);
                            break;
                        }
                    case EchoType.异常信息: {
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.Write(DateTime.Now.ToString("MM-dd HH:mm:ss") + "|");
                            title = string.IsNullOrEmpty(title) ? "异常信息" : title;

                            Console.Write(string.Format("【{0}】：{1}", title, content + Environment.NewLine));
                            LogHelper.GetInstance().CreateLog("异常信息", title + " " + strall, EchoType.异常信息);
                            break;
                        }
                }
            }
        }

        public static void EchoStart(string info) {
            EchoHelper.Echo("===========" + info + "=======================", "【分隔符】", EchoHelper.EchoType.淡蓝信息);
        }

        public static void EchoEnd(string info) {
            EchoHelper.Echo("===========" + info + "=======================", "【分隔符】", EchoHelper.EchoType.淡蓝信息);
        }

        public static void EchoStart() {
            EchoHelper.Echo("=========================================start=========================================", "【分隔符】", EchoHelper.EchoType.淡蓝信息);
        }

        public static void EchoEnd() {
            EchoHelper.Echo("========================================= end =========================================", "【分隔符】", EchoHelper.EchoType.淡蓝信息);
        }

        public static string EchoException(Exception ex) {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(DateTime.Now.ToString("MM-dd HH:mm:ss") + "|");
            Console.Write(string.Format("【{0}】：{1}", "系统异常", ex.Message + Environment.NewLine));
            LogHelper.GetInstance().CreateLog("异常信息", ex.Message + Environment.NewLine + ex.Source + Environment.NewLine + ex.StackTrace, EchoType.异常信息);
            return ex.Message;
        }

        public static string EchoExceptionNOShow(Exception ex) {
            LogHelper.GetInstance().CreateLog("异常信息", ex.Message + Environment.NewLine + ex.Source + Environment.NewLine + ex.StackTrace, EchoType.异常信息);
            return ex.Message;
        }



        public enum EchoType {
            淡蓝信息,
            绿色信息,
            红色信息,
            异常信息,
        }

        #endregion

        #region 弹出各种对话框
        public static DialogResult ShowDialog(string str, MessageType m) {
            switch (m) {
                case MessageType.警告:
                    return KryptonMessageBox.Show(str, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                case MessageType.错误:
                    return KryptonMessageBox.Show(str, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                default:
                    return KryptonMessageBox.Show(str, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 返回yes，no。
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static bool ShowDialog(string title, string content) {
            return KryptonMessageBox.Show(content, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        /// <summary>
        /// 返回的是 Ok，Cancel
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static DialogResult ShowDialog2(string title, string content) {
            return KryptonMessageBox.Show(content, title, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
        }
        /// <summary>
        /// 这里定义一个弹出类型的枚举类.比输入1，2，4，5这样的方式更好,更直观.
        /// 
        /// </summary>
        public enum MessageType {
            提示,
            警告,
            错误,
        }
        #endregion

        #region 控件界面上输出。
        /// <summary>
        /// 初始化气泡
        /// </summary>
        private static BalloonHelp Balloon;
        private static IntPtr ParenthWnd = new IntPtr(0);
        [DllImport("User32.dll")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        private static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);
        [DllImport("user32.dll")]
        private static extern bool IsWindow(IntPtr hWND);
        [DllImport("User32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, string lParam);
        [DllImport("User32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int type);


        /// <summary>
        /// 显示气泡
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="text">正文</param>
        /// <param name="ctr">控件</param>
        public static void ShowBalloon(string title, string text, Control ctr) {
            Balloon = new BalloonHelp();
            Balloon.Caption = title;
            Balloon.Content = text;
            Balloon.Icon = SystemIcons.Information;
            Balloon.ShowBalloon(ctr);

        }

        #endregion
    }
}

