using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic;
using org.jiechan.service;
using System.Threading;


namespace org.jiechan.main {
    public class test {

        public delegate bool lpEnumFunc(IntPtr hWnd, int lParam);

        [DllImport("user32", CharSet = CharSet.Auto, EntryPoint = "EnumWindows", SetLastError = true)]
        public static extern int EnumWindowsAPI(lpEnumFunc funcCallBack, int lParam);
        [DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetLastError();
        [DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int SetThreadExecutionState(int EXECUTION_STATE);
        [DllImport("user32", CharSet = CharSet.Auto, EntryPoint = "IsWindowVisible", SetLastError = true)]
        public static extern bool IsWindowVisibleAPI(IntPtr hWnd);
        [DllImport("user32", CharSet = CharSet.Auto, EntryPoint = "IsWindow", SetLastError = true)]
        public static extern bool IsWindowAPI(IntPtr hWnd);
        [DllImport("user32", CharSet = CharSet.Auto, EntryPoint = "GetWindowLong", SetLastError = true)]
        public static extern int GetWindowLongAPI(IntPtr hWnd, int nIndex);
        [DllImport("user32", CharSet = CharSet.Auto, EntryPoint = "GetWindowText", SetLastError = true)]
        public static extern int GetWindowTextAPI(IntPtr hWnd, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpString, int cch);
        [DllImport("user32", CharSet = CharSet.Auto, EntryPoint = "SetWindowText", SetLastError = true)]
        public static extern int SetWindowTextAPI(IntPtr hwnd, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpString);
        [DllImport("user32", CharSet = CharSet.Auto, EntryPoint = "GetWindow", SetLastError = true)]
        public static extern IntPtr GetWindowAPI(IntPtr hWnd, int wCmd);
        [DllImport("user32", CharSet = CharSet.Auto, EntryPoint = "FindWindow", SetLastError = true)]
        public static extern IntPtr FindWindowAPI([MarshalAs(UnmanagedType.VBByRefStr)] ref string lpClassName, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpWindowName);
        [DllImport("user32", CharSet = CharSet.Auto, EntryPoint = "FindWindowEx", SetLastError = true)]
        public static extern IntPtr FindWindowExAPI(IntPtr hwndParent, IntPtr hwndChildAfter, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpszClass, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpszWindow);
        [DllImport("user32", CharSet = CharSet.Auto, EntryPoint = "SetForegroundWindow", SetLastError = true)]
        public static extern int SetForegroundWindowAPI(IntPtr hWnd);
        [DllImport("user32", CharSet = CharSet.Auto, EntryPoint = "GetForegroundWindow", SetLastError = true)]
        public static extern IntPtr GetForegroundWindowAPI();
        [DllImport("user32", CharSet = CharSet.Auto, EntryPoint = "GetWindowThreadProcessId", SetLastError = true)]
        public static extern int GetWindowThreadProcessIdAPI(int hwnd, ref int lpdwProcessId);
        [DllImport("kernel32", CharSet = CharSet.Auto, EntryPoint = "OpenProcess", SetLastError = true)]
        public static extern int OpenProcessAPI(int dwDesiredAccess, bool blnInheritHandle, int dwProcId);
        [DllImport("kernel32", CharSet = CharSet.Auto, EntryPoint = "VirtualAllocEx", SetLastError = true)]
        public static extern int VirtualAllocExAPI(int hProcess, int lpAddress, int dwSize, int flAllocationType, int flProtect);
        [DllImport("kernel32", CharSet = CharSet.Auto, EntryPoint = "VirtualFreeEx", SetLastError = true)]
        public static extern int VirtualFreeExAPI(int hProcess, int lpAddress, int dwSize, int dwFreeType);
        [DllImport("kernel32", CharSet = CharSet.Auto, EntryPoint = "CloseHandle", SetLastError = true)]
        public static extern int CloseHandleAPI(int hObject);
        [DllImport("psapi", CharSet = CharSet.Auto, EntryPoint = "GetModuleFileNameEx", SetLastError = true)]
        public static extern int GetModuleFileNameExAPI(IntPtr hProcess, IntPtr hModule, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpFileName, int nSize);
        [DllImport("user32", CharSet = CharSet.Auto, EntryPoint = "PostMessage", SetLastError = true)]
        public static extern int PostMessageByNum(IntPtr hWnd, int wMsg, int wParam, int lParam);
        [DllImport("user32", CharSet = CharSet.Auto, EntryPoint = "PostMessage", SetLastError = true)]
        public static extern int PostMessageByStr(IntPtr hWnd, int wMsg, int wParam, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lParam);
        [DllImport("user32", CharSet = CharSet.Auto, EntryPoint = "SendMessage", SetLastError = true)]
        public static extern int SendMessageByNum(IntPtr hWnd, int wMsg, int wParam, int lParam);
        [DllImport("user32", CharSet = CharSet.Auto, EntryPoint = "SendMessage", SetLastError = true)]
        public static extern int SendMessageByStr(IntPtr hWnd, int wMsg, int wParam, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lParam);
        [DllImport("user32", CharSet = CharSet.Auto, EntryPoint = "SetCursorPos", SetLastError = true)]
        public static extern int SetCursorPosAPI(int x, int y);
        [DllImport("user32", CharSet = CharSet.Auto, EntryPoint = "mouse_event", SetLastError = true)]
        public static extern void Mouse_EventAPI(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);
        [DllImport("user32", CharSet = CharSet.Auto, EntryPoint = "SetWindowPos", SetLastError = true)]
        public static extern int SetWindowPosAPI(IntPtr hWnd, int hWndInsertAfter, int x, int y, int cx, int cy, int wFlags);
        [DllImport("user32", CharSet = CharSet.Auto, EntryPoint = "ReleaseCapture", SetLastError = true)]
        public static extern int ReleaseCaptureAPI();
        [DllImport("user32", CharSet = CharSet.Auto, EntryPoint = "GetParent", SetLastError = true)]
        public static extern IntPtr GetParentAPI(IntPtr hWnd);
        public static bool EnumVisibleWindowsProc(IntPtr hWnd, int lParam) {
            checked {
                if (IsWindowVisibleAPI(hWnd)) {
                    Array.Resize<IntPtr>(ref wwwwww, wwwwww.Length + 1);
                    wwwwww[wwwwww.Length - 1] = hWnd;
                }
                return true;
            }
        }
        private static IntPtr[] wwwwww = new IntPtr[0];

        public static string GetWindowText(IntPtr hWnd) {
            string text = new string('\0', 256);

            GetWindowTextAPI(hWnd, ref text, Strings.Len(text));


            return text.Trim(new char[]
			{
				'\0'
			});
        }
        public static string GetWindowText(IntPtr hWnd, bool isBySendMsg = false) {
            string text = new string('\0', 256);
            if (!isBySendMsg) {
                GetWindowTextAPI(hWnd, ref text, Strings.Len(text));
            } else {
                SendMessageByStr(hWnd, 13, Strings.Len(text), ref text);
            }
            return text.Trim(new char[]
	{
		'\0'
	});
        }

        public static IntPtr[] EnumVisibleWindows() {
            Array.Resize<IntPtr>(ref wwwwww, 0);
            EnumWindowsAPI(new lpEnumFunc(EnumVisibleWindowsProc), 0);
            return wwwwww;
        }
        public static bool IsWindowVisible(IntPtr hWnd) {
            return IsWindowVisibleAPI(hWnd);
        }
        public static bool IsClipboardBusy = false;

        private static string h() {
            // 			Thread thread = new Thread(new ThreadStart("4100"));
            // 			thread.SetApartmentState(ApartmentState.STA);
            // 			thread.Start();
            // 			thread.Join();
            return "";
        }

        public static void PostMsgWMCmd(IntPtr hWnd, int wParam, int SleepTime) {
            int arg_0B_1 = 273;
            string text = null;
            PostMessageByStr(hWnd, arg_0B_1, wParam, ref text);
            Thread.Sleep(SleepTime);
        }


        public static string GetCVirtualGridCtrlText(object Form, IntPtr hWndList) {
            if (IsClipboardBusy) {
                return "";
            }


            IsClipboardBusy = true;
            string result;
            try {
                string a_ = h();
                PostMsgWMCmd(hWndList, 57634, 10);
                string text = h();
                //Module1.a(RuntimeHelpers.GetObjectValue(Form), a_);
                IsClipboardBusy = false;
                result = text;
            } catch (Exception e) {
                // 				ProjectData.SetProjectError(expr_42);
                // 				Module1.IsClipboardBusy = false;
                // 				result = "";
                // 				ProjectData.ClearProjectError();
            }
            return "";
        }

        public test() {
            IntPtr[] iwwww = EnumVisibleWindows();
            // IntPtr intPtr = FindWindowExAPI(hCurrent32770Window, arg_1B_1, ref text, ref text2);
            for (int i = 0; i < iwwww.Length; i++) {
                IntPtr intPtr = iwwww[i];
                if (IsWindowVisible(intPtr)) {
                    string windowText = GetWindowText(intPtr, false);
                    if (windowText.Contains("网上股票交易系统5.0")) {
                        EchoHelper.Echo("找到了" + intPtr, "", EchoHelper.EchoType.异常信息);
                        
                    }
                    EchoHelper.Echo(windowText, "debug", EchoHelper.EchoType.普通信息);
                }

            }

        }
    }
}




