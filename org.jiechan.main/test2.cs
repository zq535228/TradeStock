using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace org.jiechan.main {
    class test2 {
        /// <summary>
        /// FindWindow获取窗口标题的句柄
        /// </summary>
        /// <param name="lpClassName"></param>
        /// <param name="lpWindowName"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        public static extern int FindWindow(string lpClassName, string lpWindowName);

        /// <summary>
        /// 获得客户区矩形
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="lpRect"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int GetClientRect(IntPtr hWnd, out RECT lpRect);

        /// <summary>
        /// 设置为活动窗体
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "SetForegroundWindow")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        public const int WM_KEYDOWN = 0x0100;
        public const int VK_RETURN = 0x0D;
        public const int WM_KEYUP = 0x0101;
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="message"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        [DllImport("user32.dll ", CharSet = CharSet.Unicode, EntryPoint = "SendMessage")]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// 矩形结构
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }


        public test2() {

            int hwnd = FindWindow(null, "网上股票交易系统5.0");
            IntPtr p = new IntPtr(hwnd);
            if (p == IntPtr.Zero)
                return;
            //设置为活动窗体，防止被其他窗口挡住
            SetForegroundWindow(p);
            RECT rc;
            GetClientRect(p, out rc);
            //迅雷新建任务窗口大小 466 x 457
            //if (rc.right == 466 && rc.bottom == 457) {
                //获取不了立即下载按钮，就用回车键下载。
                SendMessage(p, WM_KEYDOWN, new IntPtr(VK_RETURN), new IntPtr(0));
                SendMessage(p, WM_KEYUP, new IntPtr(VK_RETURN), new IntPtr(0));
            //}  
        
        }

    }
}
