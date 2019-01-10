using System;
using System.Collections;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.VisualBasic.MyServices;
using System.Drawing.Drawing2D;
using System.Management;
using System.IO;


namespace org.jiechan.main.core {

    [StandardModule]
    public sealed class comcore {
        public struct RECTAPI {
            public int Left;

            public int Top;

            public int Right;

            public int Bottom;
        }

        public struct TVITEMAPI {
            public int Mask;

            public int hItem;

            public int State;

            public int StateMask;

            public int pszText;

            public int cchTextMax;

            public int iImage;

            public int iSelectedImage;

            public int cChildren;

            public int lParam;
        }

        public struct POINTAPI {
            public int X;

            public int Y;
        }

        public struct LVITEMAPI {
            public int Mask;

            public int iItem;

            public int iSubItem;

            public int State;

            public int StateMask;

            public int pszText;

            public int cchTextMax;

            public int iImage;

            public int lParam;

            public int iIndent;
        }

        public struct LVCOLUMNAPI {
            public int Mask;

            public int int_0;

            public int int_1;

            public int pszText;

            public int cchTextMax;

            public int iSubItem;

            public int iImage;

            public int iOrder;

            public int cxMin;

            public int cxIdeal;
        }

        public delegate bool lpEnumFunc(IntPtr hWnd, int lParam);

        private delegate void Delegate0(string string_0);

        private delegate void Delegate1(Control control_0, string string_0);

        private delegate void Delegate2(Control control_0, Color color_0);

        private delegate void Delegate3(Control control_0, string string_0, Point point_0, bool bool_0, int int_0, bool bool_1);

        public const int GWL_STYLE = -16;

        public const int WS_MINIMIZE = 536870912;

        public const int WS_MAXIMIZE = 16777216;

        public const int WS_DISABLED = 134217728;

        public const int SC_CLOSE = 61536;

        public const int SC_MINIMIZE = 61472;

        public const int SC_MAXIMIZE = 61488;

        public const int SC_RESTORE = 61728;

        public const int WM_SETTEXT = 12;

        public const int WM_GETTEXT = 13;

        public const int WM_KEYDOWN = 256;

        public const int WM_KEYUP = 257;

        public const int WM_LBUTTONDOWN = 513;

        public const int WM_LBUTTONUP = 514;

        public const int WM_LBUTTONDBLCLK = 515;

        public const int WM_RBUTTONDOWN = 516;

        public const int WM_RBUTTONUP = 517;

        public const int WM_RBUTTONDBLCLK = 518;

        public const int WM_COMMAND = 273;

        public const int GW_OWNER = 4;

        public const int MK_LBUTTON = 1;

        public const int MK_RBUTTON = 2;

        public const int MK_SHIFT = 4;

        public const int MK_CONTROL = 8;

        public const int MK_MBUTTON = 16;

        public const int MOUSEEVENTF_MOVE = 1;

        public const int MOUSEEVENTF_LEFTDOWN = 2;

        public const int MOUSEEVENTF_LEFTUP = 4;

        public const int MOUSEEVENTF_RIGHTDOWN = 8;

        public const int MOUSEEVENTF_RIGHTUP = 16;

        public const int MOUSEEVENTF_ABSOLUTE = 32768;

        public const int PROCESS_ALL_ACCESS = 2035711;

        public const int MEM_COMMIT = 4096;

        public const int PAGE_READWRITE = 4;

        public const int MEM_RELEASE = 32768;

        public const int HWND_TOP = 0;

        public const int HWND_BOTTOM = 1;

        public const int HWND_TOPMOST = -1;

        public const int HWND_NOTOPMOST = -2;

        public const int SWP_NOSIZE = 1;

        public const int SWP_SHOWWINDOW = 64;

        public const int WM_NCLBUTTONDOWN = 161;

        public const int WM_NCRBUTTONDOWN = 164;

        public const int HTCAPTION = 2;

        public const int WM_POWERBROADCAST = 536;

        public const int PBT_APMQUERYSUSPEND = 0;

        public const int PBT_APMQUERYSTANDBY = 1;

        public const int ES_SYSTEM_REQUIRED = 1;

        public const int ES_DISPLAY_REQUIRED = 2;

        public const int ES_USER_PRESENT = 4;

        public const int ES_CONTINUOUS = -2147483648;

        public const int BROADCAST_QUERY_DENY = 1112363332;

        public const int WM_SYSCOMMAND = 274;

        public const int SC_SCREENSAVE = 61760;

        public const int SC_MONITORPOWER = 61808;

        private static IntPtr[] intptr_0 = new IntPtr[0];

        private static IntPtr[] intptr_1 = new IntPtr[0];

        public const int TVM_SETITEMHEIGHT = 4379;

        public const int TVM_GETITEMHEIGHT = 4380;

        public const int TV_FIRST = 4352;

        public const int TVM_GETITEMRECT = 4356;

        public const int TVM_GETNEXTITEM = 4362;

        public const int TVM_GETITEM = 4364;

        public const int TVGN_ROOT = 0;

        public const int TVGN_NEXT = 1;

        public const int TVGN_CHILD = 4;

        public const int TVIF_TEXT = 1;

        private const int int_0 = 4608;

        private const int int_1 = 4608;

        private const int int_2 = 4096;

        private const int int_3 = 4127;

        private const int int_4 = 4100;

        private const int int_5 = 4121;

        private const int int_6 = 4141;

        private const int int_7 = 4;

        private const int int_8 = 1;

        private const int int_9 = 1026;

        public static bool IsClipboardBusy = false;

        //交易持仓列表网格的数据
        private static string cvirtualgridctrlText;

        [DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetLastError();

        [DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int SetThreadExecutionState(int EXECUTION_STATE);

        [DllImport("user32", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int EnumWindows(comcore.lpEnumFunc funcCallBack, int lParam);

        [DllImport("user32", CharSet = CharSet.Auto, EntryPoint = "IsWindowVisible", SetLastError = true)]
        public static extern bool IsWindowVisible_1(IntPtr hWnd);

        [DllImport("user32", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool IsWindow(IntPtr hWnd);

        [DllImport("user32", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32", CharSet = CharSet.Auto, EntryPoint = "GetWindowText", SetLastError = true)]
        public static extern int GetWindowText_1(IntPtr hWnd, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpString, int int_10);

        [DllImport("user32", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int SetWindowText(IntPtr hwnd, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpString);

        [DllImport("user32", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetWindow(IntPtr hWnd, int wCmd);

        [DllImport("user32", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr FindWindow([MarshalAs(UnmanagedType.VBByRefStr)] ref string lpClassName, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpWindowName);

        [DllImport("user32", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpszClass, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpszWindow);

        [DllImport("user32", CharSet = CharSet.Auto, EntryPoint = "SetForegroundWindow", SetLastError = true)]
        public static extern int SetForegroundWindow_1(IntPtr hWnd);

        [DllImport("user32", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetWindowThreadProcessId(int hwnd, ref int lpdwProcessId);

        [DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int OpenProcess(int dwDesiredAccess, bool blnInheritHandle, int dwProcId);

        [DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int VirtualAllocEx(int hProcess, int lpAddress, int dwSize, int flAllocationType, int flProtect);

        [DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int WriteProcessMemory(int hProcess, int lpBaseAddress, ref comcore.TVITEMAPI lpBuffer, int nSize, int lpNumberOfBytesWritten);

        [DllImport("kernel32", CharSet = CharSet.Auto, EntryPoint = "WriteProcessMemory", SetLastError = true)]
        public static extern int WriteProcessMemory_1(int hProcess, int lpBaseAddress, ref comcore.LVITEMAPI lpBuffer, int nSize, int lpNumberOfBytesWritten);

        [DllImport("kernel32", CharSet = CharSet.Auto, EntryPoint = "WriteProcessMemory", SetLastError = true)]
        public static extern int WriteProcessMemory_2(int hProcess, int lpBaseAddress, ref comcore.LVCOLUMNAPI lpBuffer, int nSize, int lpNumberOfBytesWritten);

        [DllImport("kernel32", CharSet = CharSet.Auto, EntryPoint = "WriteProcessMemory", SetLastError = true)]
        public static extern int WriteProcessMemory_3(int hProcess, int lpBaseAddress, ref comcore.RECTAPI lpBuffer, int nSize, int lpNumberOfBytesWritten);

        [DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int ReadProcessMemory(int hProcess, int lpBaseAddress, byte[] lpBuffer, int nSize, int lpNumberOfBytesWritten);

        [DllImport("kernel32", CharSet = CharSet.Auto, EntryPoint = "ReadProcessMemory", SetLastError = true)]
        public static extern int ReadProcessMemory_1(int hProcess, int lpBaseAddress, ref comcore.RECTAPI lpBuffer, int nSize, int lpNumberOfBytesWritten);

        [DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int VirtualFreeEx(int hProcess, int lpAddress, int dwSize, int dwFreeType);

        [DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int CloseHandle(int hObject);

        [DllImport("psapi", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetModuleFileNameEx(IntPtr hProcess, IntPtr hModule, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpFileName, int nSize);

        [DllImport("user32", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int PostMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        [DllImport("user32", CharSet = CharSet.Auto, EntryPoint = "PostMessage", SetLastError = true)]
        public static extern int PostMessage_1(IntPtr hWnd, int wMsg, int wParam, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lParam);

        [DllImport("user32", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        [DllImport("user32", CharSet = CharSet.Auto, EntryPoint = "SendMessage", SetLastError = true)]
        public static extern int SendMessage_1(IntPtr hWnd, int wMsg, int wParam, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lParam);

        [DllImport("user32", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int SetCursorPos(int int_10, int int_11);

        [DllImport("user32", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern void mouse_event(int dwFlags, int int_10, int int_11, int cButtons, int dwExtraInfo);

        [DllImport("user32", CharSet = CharSet.Auto, EntryPoint = "GetWindowRect", SetLastError = true)]
        public static extern int GetWindowRect_1(IntPtr hWnd, ref comcore.RECTAPI lpRect);

        [DllImport("user32", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int ClientToScreen(IntPtr hWnd, ref comcore.POINTAPI lpPoint);

        [DllImport("user32", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int int_10, int int_11, int int_12, int int_13, int wFlags);

        [DllImport("user32", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int ReleaseCapture();

        [DllImport("user32", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetParent(IntPtr hWnd);

        public static string GetIniPropertyValue(string AppStartupDir, string DirConfig, string FileConfigIni, string SectionName, string PropertyName, string DefaultPropertyValue = "") {
            string text = comcore.GetOrSetTextFile(AppStartupDir, "Get", DirConfig, FileConfigIni, "\r\n[手动修改请保持换行格式]\r\n", false, true, true);
            if (!text.Contains("[" + SectionName + "]")) {
                text = string.Concat(new string[]
				{
					text,
					"\r\n[",
					SectionName,
					"]\r\n",
					PropertyName,
					"=",
					DefaultPropertyValue,
					"\r\n"
				});
                comcore.GetOrSetTextFile(AppStartupDir, "Set", DirConfig, FileConfigIni, text, false, true, true);
                return DefaultPropertyValue;
            }
            if (!text.Contains("\r\n" + PropertyName + "=")) {
                string text2 = Strings.Split(text, "[" + SectionName + "]", -1, CompareMethod.Binary)[0];
                string text3 = Strings.Split(text, "[" + SectionName + "]", -1, CompareMethod.Binary)[1];
                if (!text3.Contains("[")) {
                    text = string.Concat(new string[]
					{
						text,
						PropertyName,
						"=",
						DefaultPropertyValue,
						"\r\n"
					});
                } else {
                    Strings.InStr(text, "[" + SectionName + "]", CompareMethod.Binary);
                    string find = "\r\n\r\n";
                    string replacement = string.Concat(new string[]
					{
						"\r\n",
						PropertyName,
						"=",
						DefaultPropertyValue,
						"\r\n\r\n"
					});
                    text = string.Concat(new string[]
					{
						text2,
						"[",
						SectionName,
						"]",
						Strings.Replace(text3, find, replacement, 1, 1, CompareMethod.Binary)
					});
                }
                comcore.GetOrSetTextFile(AppStartupDir, "Set", DirConfig, FileConfigIni, text, false, true, true);
                return DefaultPropertyValue;
            }
            string expression = Strings.Split(text, "\r\n" + PropertyName + "=", -1, CompareMethod.Binary)[1];
            return Strings.Split(expression, "\r\n", -1, CompareMethod.Binary)[0];
        }

        public static void SetIniPropertyValue(string AppStartupDir, string DirConfig, string FileConfigIni, string SectionName, string PropertyName, string PropertyValue) {
            string text = comcore.GetOrSetTextFile(AppStartupDir, "Get", DirConfig, FileConfigIni, "\r\n[手动修改请保持换行格式]\r\n", false, true, true);
            if (!text.Contains("[" + SectionName + "]")) {
                text = string.Concat(new string[]
				{
					text,
					"\r\n[",
					SectionName,
					"]\r\n",
					PropertyName,
					"=",
					PropertyValue,
					"\r\n"
				});
                comcore.GetOrSetTextFile(AppStartupDir, "Set", DirConfig, FileConfigIni, text, false, true, true);
            } else if (!text.Contains("\r\n" + PropertyName + "=")) {
                string text2 = Strings.Split(text, "[" + SectionName + "]", -1, CompareMethod.Binary)[0];
                string text3 = Strings.Split(text, "[" + SectionName + "]", -1, CompareMethod.Binary)[1];
                if (!text3.Contains("[")) {
                    text = string.Concat(new string[]
					{
						text,
						PropertyName,
						"=",
						PropertyValue,
						"\r\n"
					});
                } else {
                    Strings.InStr(text, "[" + SectionName + "]", CompareMethod.Binary);
                    string find = "\r\n\r\n";
                    string replacement = string.Concat(new string[]
					{
						"\r\n",
						PropertyName,
						"=",
						PropertyValue,
						"\r\n\r\n"
					});
                    text = string.Concat(new string[]
					{
						text2,
						"[",
						SectionName,
						"]",
						Strings.Replace(text3, find, replacement, 1, 1, CompareMethod.Binary)
					});
                }
                comcore.GetOrSetTextFile(AppStartupDir, "Set", DirConfig, FileConfigIni, text, false, true, true);
            } else {
                string expression = Strings.Split(text, "\r\n" + PropertyName + "=", -1, CompareMethod.Binary)[1];
                string str = Strings.Split(expression, "\r\n", -1, CompareMethod.Binary)[0];
                text = Strings.Replace(text, PropertyName + "=" + str, PropertyName + "=" + PropertyValue, 1, -1, CompareMethod.Binary);
                comcore.GetOrSetTextFile(AppStartupDir, "Set", DirConfig, FileConfigIni, text, false, true, true);
            }
        }

        public static void WriteToLog(string AppStartupDir, string LogDir, string LogText) {
            string dir = LogDir + "\\" + Strings.Format(DateAndTime.Now, "yyyy-MM");
            string file = Strings.Format(DateAndTime.Now, "yyyy-MM-dd") + "_Log.log";
            LogText = Strings.Format(DateAndTime.Now, "【HH:mm:ss】: ") + LogText + "\r\n";
            comcore.GetOrSetTextFile(AppStartupDir, "Set", dir, file, LogText, true, true, true);
        }

        public static string ReadFromLog(string AppStartupDir, string LogDir, DateTime DateTime) {
            string dir = LogDir + "\\" + Strings.Format(DateTime, "yyyy-MM");
            string file = Strings.Format(DateTime, "yyyy-MM-dd") + "_Log.log";
            return comcore.GetOrSetTextFile(AppStartupDir, "Get", dir, file, "", false, false, false);
        }

        public static string GetOrSetTextFile(string AppStartupDir, string GetOrSet, string Dir, string File, string ValueSet_Or_DefValueGet = "", bool isAppend = false, bool CreatDir = true, bool CreatFile = true) {
            string result="";
            
            return result;
        }

        public static void PostMsgKeyPress(IntPtr hWnd, Keys KeyCode, int SleepTime) {
            int arg_0B_1 = 256;
            string text = null;
            comcore.PostMessage_1(hWnd, arg_0B_1, (int)KeyCode, ref text);
            Thread.Sleep(50);
            int arg_23_1 = 257;
            text = null;
            comcore.PostMessage_1(hWnd, arg_23_1, (int)KeyCode, ref text);
            Thread.Sleep(SleepTime);
        }

        public static void PostMsgKeyDown(IntPtr hWnd, Keys KeyCode, int SleepTime) {
            int tmp = 256;
            string text = null;
            comcore.PostMessage_1(hWnd, tmp, (int)KeyCode, ref text);
            Thread.Sleep(SleepTime);
        }

        public static void PostMsgMouseClickControl(IntPtr hWnd, int SleepTime) {
            Thread.Sleep(SleepTime);
            comcore.PostMessage(hWnd, 513, 1, 0);
            Thread.Sleep(50);
            comcore.PostMessage(hWnd, 514, 0, 0);
        }

        public static void PostMsgMouseClickPoint(IntPtr hWnd, Point Pos, int SleepTime) {
            comcore.SetCursorPos(Pos.X, Pos.Y);
            Thread.Sleep(50);
//             if (!Class1.smethod_0().Mouse.ButtonsSwapped) {
//                 comcore.mouse_event(2, 0, 0, 0, 0);
//                 Thread.Sleep(50);
//                 comcore.mouse_event(4, 0, 0, 0, 0);
//                 Thread.Sleep(SleepTime);
//             } else {
//                 comcore.mouse_event(8, 0, 0, 0, 0);
//                 Thread.Sleep(50);
//                 comcore.mouse_event(16, 0, 0, 0, 0);
//                 Thread.Sleep(SleepTime);
//             }
        }

        public static void PostMsgMouseClickPoint2(IntPtr hWnd, Point Pos, int SleepTime) {
            string value = Pos.Y.ToString("X4") + Pos.X.ToString("X4");
            int lParam = Convert.ToInt32(value, 16);
            comcore.PostMessage(hWnd, 513, 1, lParam);
            Thread.Sleep(50);
            comcore.PostMessage(hWnd, 514, 0, lParam);
            Thread.Sleep(SleepTime);
        }

        public static void SendMsgSetEditText(IntPtr hWnd, string Text, int SleepTime) {
            comcore.SendMessage_1(hWnd, 12, 0, ref Text);
            Thread.Sleep(SleepTime);
        }

        public static void PostMsgWMCmd(IntPtr hWnd, int wParam, int SleepTime) {
            string text = null;
            comcore.PostMessage_1(hWnd, 273, wParam, ref text);
            //API_SendMessage(hWnd,273
            Thread.Sleep(SleepTime);
        }

        public static void PostMsgClosehWnd(IntPtr hWnd, int SleepTime) {
            int arg_0F_1 = 274;
            int arg_0F_2 = 61536;
            string text = null;
            comcore.PostMessage_1(hWnd, arg_0F_1, arg_0F_2, ref text);
            Thread.Sleep(SleepTime);
        }

        public static void PostMsgRestoreWnd(IntPtr hWnd, int SleepTime) {
            int arg_0F_1 = 274;
            int arg_0F_2 = 61728;
            string text = null;
            comcore.PostMessage_1(hWnd, arg_0F_1, arg_0F_2, ref text);
            Thread.Sleep(SleepTime);
        }
        
        public static void PostMsgMoveWnd(IntPtr hWnd) {
            comcore.ReleaseCapture();
            comcore.PostMessage(hWnd, 161, 2, 0);
        }

        public static IntPtr[] EnumVisibleWindows() {
            Array.Resize<IntPtr>(ref comcore.intptr_0, 0);
            comcore.EnumWindows(new comcore.lpEnumFunc(comcore.EnumVisibleWindowsProc), 0);
            return comcore.intptr_0;
        }

        public static bool EnumVisibleWindowsProc(IntPtr hWnd, int lParam) {
            checked {
                if (comcore.IsWindowVisible_1(hWnd)) {
                    Array.Resize<IntPtr>(ref comcore.intptr_0, comcore.intptr_0.Length + 1);
                    comcore.intptr_0[comcore.intptr_0.Length - 1] = hWnd;
                }
                return true;
            }
        }

        public static IntPtr[] EnumVisible32770Windows(IntPtr hWndParent) {
            Array.Resize<IntPtr>(ref comcore.intptr_1, 0);
            IntPtr arg_1E_1 = (IntPtr)0;
            string text = "#32770";
            string text2 = null;
            IntPtr intPtr = comcore.FindWindowEx(hWndParent, arg_1E_1, ref text, ref text2);
            checked {
                while ((int)intPtr != 0) {
                    if (comcore.IsWindowVisible_1(intPtr)) {
                        Array.Resize<IntPtr>(ref comcore.intptr_1, comcore.intptr_1.Length + 1);
                        comcore.intptr_1[comcore.intptr_1.Length - 1] = intPtr;
                    }
                    IntPtr arg_68_1 = intPtr;
                    text2 = "#32770";
                    text = null;
                    intPtr = comcore.FindWindowEx(hWndParent, arg_68_1, ref text2, ref text);
                }
                return comcore.intptr_1;
            }
        }

        public static bool IsControlEnabled(IntPtr hWnd) {
            int windowLong = comcore.GetWindowLong(hWnd, -16);
            return (windowLong & 134217728) == 0;
        }

        public static bool IsWindowMinimized(IntPtr hWnd) {
            int windowLong = comcore.GetWindowLong(hWnd, -16);
            return (windowLong & 536870912) != 0;
        }

        public static void RestoreAndActiveWindow(IntPtr hWnd, int SleepTime) {
            comcore.PostMsgRestoreWnd(hWnd, 300);
            comcore.SetWindowPos(hWnd, 0, 0, 0, 0, 0, 65);
            Thread.Sleep(200);
            comcore.SetForegroundWindow_1(hWnd);
            Thread.Sleep(50);
            Thread.Sleep(SleepTime);
        }

        public static void SetForegroundWindow(IntPtr hWnd) {
            comcore.SetForegroundWindow_1(hWnd);
        }

        public static bool IsWindowVisible(IntPtr hWnd) {
            return comcore.IsWindowVisible_1(hWnd);
        }

        public static bool IsWindowValid(IntPtr hWnd) {
            return comcore.IsWindow(hWnd);
        }

        public static string GetWindowText(IntPtr hWnd, bool isBySendMsg = false) {
            string text = new string('\0', 256);
            if (!isBySendMsg) {
                comcore.GetWindowText_1(hWnd, ref text, Strings.Len(text));
            } else {
                comcore.SendMessage_1(hWnd, 13, Strings.Len(text), ref text);
            }
            return text.Trim(new char[]
			{
				'\0'
			});
        }

        public static Rectangle GetWindowRect(IntPtr hWnd) {
            comcore.RECTAPI rECTAPI = new comcore.RECTAPI();
            comcore.GetWindowRect_1(hWnd, ref rECTAPI);
            Rectangle result = checked(new Rectangle(rECTAPI.Left, rECTAPI.Top, rECTAPI.Right - rECTAPI.Left, rECTAPI.Bottom - rECTAPI.Top));
            return result;
        }

        public static string GetWindowFullPath(IntPtr hWnd) {
            int dwProcId = 123456;
            comcore.GetWindowThreadProcessId((int)hWnd, ref dwProcId);
            int num = comcore.OpenProcess(2035711, false, dwProcId);
            string text = new string('\0', 256);
            comcore.GetModuleFileNameEx((IntPtr)num, (IntPtr)0, ref text, text.Length);
            comcore.CloseHandle(num);
            return text.Trim(new char[]
			{
				'\0'
			});
        }

        public static void SetTreeViewItemHeight(IntPtr hTreeView) {
            int num = comcore.SendMessage(hTreeView, 4380, 0, 0);
            int wParam = checked(num + 5);
            comcore.SendMessage(hTreeView, 4379, wParam, 0);
        }

        public static IntPtr GetTreeViewItem(IntPtr hTreeView, string ItemText, bool IsExpande = false, string SubItemText = "") {
            for (int i = comcore.SendMessage(hTreeView, 4362, 0, 0); i != 0; i = comcore.SendMessage(hTreeView, 4362, 1, i)) {
                string treeViewItemText = comcore.GetTreeViewItemText(hTreeView, (IntPtr)i);
                if (treeViewItemText.Contains(ItemText)) {
                    if (IsExpande) {
                        for (int j = comcore.SendMessage(hTreeView, 4362, 4, i); j != 0; j = comcore.SendMessage(hTreeView, 4362, 1, j)) {
                            string treeViewItemText2 = comcore.GetTreeViewItemText(hTreeView, (IntPtr)j);
                            if (treeViewItemText2.Contains(SubItemText)) {
                                return (IntPtr)j;
                            }
                        }
                    }
                    return (IntPtr)i;
                }
            }
            return (IntPtr)0;
        }

        public static string GetTreeViewItemText(IntPtr hTreeView, IntPtr hItem) {
            byte[] array = new byte[256];
            int dwProcId = 0;
            comcore.GetWindowThreadProcessId((int)hTreeView, ref dwProcId);
            int num = comcore.OpenProcess(2035711, false, dwProcId);
            int num2 = comcore.VirtualAllocEx(num, 0, 256, 4096, 4);
            comcore.TVITEMAPI tVITEMAPI = new comcore.TVITEMAPI();
            tVITEMAPI.hItem = (int)hItem;
            tVITEMAPI.Mask = 1;
            tVITEMAPI.pszText = num2;
            tVITEMAPI.cchTextMax = 1024;
            int num3 = comcore.VirtualAllocEx(num, 0, Strings.Len(tVITEMAPI), 4096, 4);
            comcore.WriteProcessMemory(num, num3, ref tVITEMAPI, Strings.Len(tVITEMAPI), 0);
            comcore.SendMessage(hTreeView, 4364, 0, num3);
            comcore.ReadProcessMemory(num, num2, array, array.Length, 0);
            string result = Encoding.Default.GetString(array).Trim(new char[]
			{
				'\0'
			}).Trim();
            comcore.VirtualFreeEx(num, num2, 0, 32768);
            comcore.VirtualFreeEx(num, num3, 0, 32768);
            comcore.CloseHandle(num);
            return result;
        }

        public static comcore.RECTAPI GetTreeViewItemRectAPI(IntPtr hTreeView, IntPtr hItem) {
            comcore.RECTAPI rECTAPI = default(comcore.RECTAPI);
            rECTAPI.Left = (int)hItem;
            int dwProcId = 0;
            comcore.GetWindowThreadProcessId((int)hTreeView, ref dwProcId);
            int num = comcore.OpenProcess(2035711, false, dwProcId);
            int num2 = comcore.VirtualAllocEx(num, 0, Strings.Len(rECTAPI), 4096, 4);
            comcore.WriteProcessMemory_3(num, num2, ref rECTAPI, Strings.Len(rECTAPI), 0);
            comcore.SendMessage(hTreeView, 4356, 1, num2);
            comcore.ReadProcessMemory_1(num, num2, ref rECTAPI, Strings.Len(rECTAPI), 0);
            comcore.VirtualFreeEx(num, num2, 0, 32768);
            comcore.CloseHandle(num);
            return rECTAPI;
        }

        public static void ClickTreeViewItem(IntPtr hTreeView, bool IsPressKeyHome, string ItemText, int SleepTime, bool IsFindSubItem = false, string SubItemText = "") {
            if (IsPressKeyHome) {
                comcore.PostMsgKeyPress(hTreeView, Keys.Home, 50);
            }
            IntPtr treeViewItem = comcore.GetTreeViewItem(hTreeView, ItemText, IsFindSubItem, SubItemText);
            comcore.RECTAPI treeViewItemRectAPI = comcore.GetTreeViewItemRectAPI(hTreeView, treeViewItem);
            Point treeViewItemRECTAPICenterPoint = comcore.GetTreeViewItemRECTAPICenterPoint(hTreeView, treeViewItemRectAPI);
            comcore.PostMsgMouseClickPoint(hTreeView, treeViewItemRECTAPICenterPoint, SleepTime);
        }

        public static Point GetTreeViewItemRECTAPICenterPoint(IntPtr hTreeView, comcore.RECTAPI RectAPI) {
            comcore.POINTAPI pOINTAPI = default(comcore.POINTAPI);
            checked {
                pOINTAPI.X = (int)Math.Round(unchecked((double)RectAPI.Left + (double)(checked(RectAPI.Right - RectAPI.Left)) / 2.0));
                pOINTAPI.Y = (int)Math.Round(unchecked((double)RectAPI.Top + (double)(checked(RectAPI.Bottom - RectAPI.Top)) / 2.0));
                comcore.ClientToScreen(hTreeView, ref pOINTAPI);
                Point result = new Point(pOINTAPI.X, pOINTAPI.Y);
                return result;
            }
        }

        public static string GetListViewText(IntPtr hListView) {
            string text = "";
            int value = comcore.SendMessage(hListView, 4127, 0, 0);
            int num = comcore.SendMessage((IntPtr)value, 4608, 0, 0);
            int num2 = comcore.SendMessage(hListView, 4100, 0, 0);
            int arg_3B_0 = 0;
            checked {
                int num3 = num - 1;
                for (int i = arg_3B_0; i <= num3; i++) {
                    text = text + comcore.smethod_0(hListView, i) + "\t";
                }
                text += "\r\n";
                int arg_71_0 = 0;
                int num4 = num2 - 1;
                for (int j = arg_71_0; j <= num4; j++) {
                    int arg_7B_0 = 0;
                    int num5 = num - 1;
                    for (int k = arg_7B_0; k <= num5; k++) {
                        text = text + comcore.smethod_1(hListView, k, j) + "\t";
                    }
                    text += "\r\n";
                }
                return text;
            }
        }

        private static string smethod_0(IntPtr intptr_2, int int_10) {
            byte[] array = new byte[256];
            int dwProcId = 0;
            comcore.GetWindowThreadProcessId((int)intptr_2, ref dwProcId);
            int num = comcore.OpenProcess(2035711, false, dwProcId);
            int num2 = comcore.VirtualAllocEx(num, 0, 256, 4096, 4);
            comcore.LVCOLUMNAPI lVCOLUMNAPI = new comcore.LVCOLUMNAPI();
            lVCOLUMNAPI.Mask = 4;
            lVCOLUMNAPI.iSubItem = int_10;
            lVCOLUMNAPI.pszText = num2;
            lVCOLUMNAPI.cchTextMax = 256;
            int num3 = comcore.VirtualAllocEx(num, 0, Strings.Len(lVCOLUMNAPI), 4096, 4);
            comcore.WriteProcessMemory_2(num, num3, ref lVCOLUMNAPI, Strings.Len(lVCOLUMNAPI), 0);
            comcore.SendMessage(intptr_2, 4121, int_10, num3);
            comcore.ReadProcessMemory(num, num2, array, array.Length, 0);
            string result = Encoding.Default.GetString(array).Trim(new char[]
			{
				'\0'
			}).Trim();
            comcore.VirtualFreeEx(num, num2, 0, 32768);
            comcore.VirtualFreeEx(num, num3, 0, 32768);
            comcore.CloseHandle(num);
            return result;
        }

        private static string smethod_1(IntPtr intptr_2, int int_10, int int_11) {
            byte[] array = new byte[256];
            int dwProcId = 0;
            comcore.GetWindowThreadProcessId((int)intptr_2, ref dwProcId);
            int num = comcore.OpenProcess(2035711, false, dwProcId);
            int num2 = comcore.VirtualAllocEx(num, 0, 256, 4096, 4);
            comcore.LVITEMAPI lVITEMAPI= new comcore.LVITEMAPI();
            lVITEMAPI.Mask = 1;
            lVITEMAPI.iSubItem = int_10;
            lVITEMAPI.pszText = num2;
            lVITEMAPI.cchTextMax = 256;
            int num3 = comcore.VirtualAllocEx(num, 0, Strings.Len(lVITEMAPI), 4096, 4);
            comcore.WriteProcessMemory_1(num, num3, ref lVITEMAPI, Strings.Len(lVITEMAPI), 0);
            comcore.SendMessage(intptr_2, 4141, int_11, num3);
            comcore.ReadProcessMemory(num, num2, array, array.Length, 0);
            string result = Encoding.Default.GetString(array).Trim(new char[]
			{
				'\0'
			}).Trim();
            comcore.VirtualFreeEx(num, num2, 0, 32768);
            comcore.VirtualFreeEx(num, num3, 0, 32768);
            comcore.CloseHandle(num);
            return result;
        }

        public static string GetStatusBarItemText(IntPtr hStatusBar, int Index) {
            byte[] array = new byte[256];
            int dwProcId = 0;
            comcore.GetWindowThreadProcessId((int)hStatusBar, ref dwProcId);
            int num = comcore.OpenProcess(2035711, false, dwProcId);
            int num2 = comcore.VirtualAllocEx(num, 0, 256, 4096, 4);
            comcore.SendMessage(hStatusBar, 1026, Index, num2);
            comcore.ReadProcessMemory(num, num2, array, array.Length, 0);
            string result = Encoding.Default.GetString(array).Trim(new char[]
			{
				'\0'
			}).Trim();
            comcore.VirtualFreeEx(num, num2, 0, 32768);
            comcore.CloseHandle(num);
            return result;
        }

        /// <summary>
        /// 从剪切板中获取仓位信息
        /// </summary>
        /// <param name="Form"></param>
        /// <param name="hWndList"></param>
        /// <returns></returns>
        public static string GetCVirtualGridCtrlText(object Form, IntPtr hWndList) {
            if (comcore.IsClipboardBusy) {
                return "";
            }
            comcore.IsClipboardBusy = true;
            string result;
            try {
                string tmp = comcore.th_clip_gettext();
                comcore.PostMsgWMCmd(hWndList, 57634, 10);
                string text = comcore.th_clip_gettext();
                comcore.smethod_4(RuntimeHelpers.GetObjectValue(Form), tmp);
                comcore.IsClipboardBusy = false;
                result = text;
            } catch (Exception ex) {
                ProjectData.SetProjectError(ex);
                comcore.IsClipboardBusy = false;
                result = "";
                ProjectData.ClearProjectError();
            }
            return result;
        }

        private static string th_clip_gettext() {
            Thread thread = new Thread(new ThreadStart(comcore.clip_gettext));
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();
            return comcore.cvirtualgridctrlText;
        }

        private static void clip_gettext() {
            comcore.cvirtualgridctrlText = Clipboard.GetText();
        }

        private static void smethod_4(object object_0, string string_1) {
            Type arg_37_1 = null;
            string arg_37_2 = "Invoke";
            object[] array = new object[]
			{
				new comcore.Delegate0(comcore.smethod_5),
				string_1
			};
            object[] arg_37_3 = array;
            string[] arg_37_4 = null;
            Type[] arg_37_5 = null;
            bool[] array2 = new bool[]
			{
				false,
				true
			};
            NewLateBinding.LateCall(object_0, arg_37_1, arg_37_2, arg_37_3, arg_37_4, arg_37_5, array2, true);
            if (array2[1]) {
                string_1 = (string)Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array[1]), typeof(string));
            }
        }

        private static void smethod_5(string string_1) {
            Clipboard.SetDataObject(string_1);
        }

        public static void SetControlText(Control Control, string Text) {
            Control.Invoke(new comcore.Delegate1(comcore.smethod_6), new object[]
			{
				Control,
				Text
			});
        }

        private static void smethod_6(Control control_0, string string_1) {
            if (control_0 is NumericUpDown) {
                ((NumericUpDown)control_0).Value = Conversions.ToDecimal(string_1);
            } else {
                control_0.Text = string_1;
            }
        }

        public static void SetControlForeColor(Control Control, Color ForeColor) {
            Control.Invoke(new comcore.Delegate2(comcore.smethod_7), new object[]
			{
				Control,
				ForeColor
			});
        }

        private static void smethod_7(Control control_0, Color color_0) {
            control_0.ForeColor = color_0;
        }

        public static Control[] GetRootAndAllSubControls(Control RootControl) {
            Control[] result = new Control[]
			{
				RootControl
			};
            comcore.smethod_8(ref result, RootControl);
            return result;
        }

        private static void smethod_8(ref Control[] control_0, Control control_1) {
            checked {
                IEnumerator enumerator = control_1.Controls.GetEnumerator();
                try {
                    while (enumerator.MoveNext()) {
                        Control control = (Control)enumerator.Current;
                        Array.Resize<Control>(ref control_0, control_0.Length + 1);
                        control_0[control_0.Length - 1] = control;
                        if (control.Controls.Count > 0) {
                            comcore.smethod_8(ref control_0, control);
                        }
                    }
                } finally {
                    if (enumerator is IDisposable) {
                        (enumerator as IDisposable).Dispose();
                    }
                }
            }
        }

        public static Point GetAbsLocationOnForm(Form Form, Point ControlLocation) {
            return new Point(0);
        }

        public static void ShowToolTip(Control Control, string Text, Point Position, bool IsShowTemp = false, int ShowTempDuration = 5000, bool IsBalloon = false) {
            Control.Invoke(new comcore.Delegate3(comcore.smethod_9), new object[]
			{
				Control,
				Text,
				Position,
				IsShowTemp,
				ShowTempDuration,
				IsBalloon
			});
        }

        private static void smethod_9(Control control_0, string string_1, Point point_0, bool bool_0, int int_10, bool bool_1) {
            ToolTip toolTip = new ToolTip();
            toolTip.AutoPopDelay = 3000;
            toolTip.InitialDelay = 200;
            toolTip.ReshowDelay = 500;
            toolTip.ShowAlways = true;
            toolTip.IsBalloon = bool_1;
            if (bool_0) {
                toolTip.Show(string_1, control_0, point_0, int_10);
            } else {
                toolTip.SetToolTip(control_0, string_1);
            }
        }

        public static string GetMachineCode() {
            string text = "";
            string text2 = comcore.smethod_10();
            text2 = ((Operators.CompareString(text2, "", false) == 0) ? "1234567890" : text2);
            string text3 = comcore.smethod_11();
            text3 = ((Operators.CompareString(text3, "", false) == 0) ? "1234567890" : text3);
            string text4 = comcore.smethod_12();
            text4 = ((Operators.CompareString(text4, "", false) == 0) ? "1234567890" : text4);
            string text5 = comcore.smethod_13();
            text5 = ((Operators.CompareString(text5, "", false) == 0) ? "1234567890" : text5);
            string text6 = comcore.smethod_14();
            text6 = ((Operators.CompareString(text6, "", false) == 0) ? "1234567890" : text6);
            text += comcore.smethod_18(text2 + text3, true, (text2 + text3).Length / 3);
            text += comcore.smethod_18(text3 + text4, false, (text3 + text4).Length / 4);
            text += comcore.smethod_18(text4 + text5, true, (text4 + text5).Length / 2);
            text += comcore.smethod_18(text5 + text6, false, (text5 + text6).Length / 3);
            text += comcore.smethod_18(text6 + text2, true, (text6 + text2).Length / 2);
            text += comcore.smethod_18(text2 + text4, true, (text2 + text4).Length / 2);
            text += comcore.smethod_18(text3 + text6, true, (text3 + text6).Length / 2);
            text = Strings.Left(text, 20).PadRight(20, '0');
            return string.Concat(new string[]
			{
				text.Substring(0, 4),
				"-",
				text.Substring(4, 4),
				"-",
				text.Substring(8, 4),
				"-",
				text.Substring(12, 4),
				"-",
				text.Substring(16, 4)
			});
        }

        private static string smethod_10() {
            string empty = string.Empty;
            string string_ = comcore.smethod_16("SELECT SerialNumber FROM Win32_BIOS", "SerialNumber");
            return comcore.smethod_17(string_);
        }

        private static string smethod_11() {
            string empty = string.Empty;
            string string_ = comcore.smethod_16("SELECT UUID FROM Win32_ComputerSystemProduct", "UUID");
            return comcore.smethod_17(string_);
        }

        private static string smethod_12() {
            string empty = string.Empty;
            string string_ = comcore.smethod_16("SELECT ProcessorId FROM Win32_Processor", "ProcessorId");
            return comcore.smethod_17(string_);
        }

        private static string smethod_13() {
            string empty = string.Empty;
            string string_ = comcore.smethod_16("SELECT SerialNumber FROM Win32_BaseBoard", "SerialNumber");
            return comcore.smethod_17(string_);
        }

        private static string smethod_14() {
            string empty = string.Empty;
            string string_ = comcore.smethod_16("SELECT SerialNumber FROM Win32_PhysicalMedia", "SerialNumber");
            return comcore.smethod_17(string_);
        }

        private static string smethod_15() {
            string empty = string.Empty;
            string string_ = comcore.smethod_16("SELECT MACAddress FROM Win32_NetworkAdapter WHERE ((MACAddress Is Not NULL) AND (Manufacturer<>'Microsoft'))", "MACAddress");
            return comcore.smethod_17(string_);
        }

        private static string smethod_16(string string_1, string string_2) {
            string text = string.Empty;
            try {
                ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(string_1);
                ManagementObjectCollection managementObjectCollection = managementObjectSearcher.Get();
                int arg_1A_0 = managementObjectCollection.Count;

                ManagementObjectCollection.ManagementObjectEnumerator enumerator = managementObjectCollection.GetEnumerator();
                try {

                    while (enumerator.MoveNext()) {
                        ManagementObject managementObject = (ManagementObject)enumerator.Current;
                        object objectValue = RuntimeHelpers.GetObjectValue(managementObject[string_2]);
                        if (objectValue != null && Operators.ConditionalCompareObjectNotEqual(objectValue, "", false)) {
                            text = objectValue.ToString();
                            string result = text;
                            return result;
                        }
                    }
                } finally {
                    if (enumerator != null) {
                        ((IDisposable)enumerator).Dispose();
                    }
                }
            } catch (Exception expr_74) {
                ProjectData.SetProjectError(expr_74);
                string result = text;
                ProjectData.ClearProjectError();
                return result;
            }
            return text;
        }

        private static string smethod_17(string string_1) {
            string text = string.Empty;
            string_1 = string_1.Trim().ToUpper();
            string text2 = string_1;
            string_1 = string.Empty;
            int arg_26_0 = 0;
            checked {
                int num = text2.Length - 1;
                for (int i = arg_26_0; i <= num; i++) {
                    string text3 = text2.Substring(i, 1);
                    if ((Strings.AscW(text3) >= 48 & Strings.AscW(text3) <= 57) | (Strings.AscW(text3) >= 65 & Strings.AscW(text3) <= 69) | (Strings.AscW(text3) >= 97 & Strings.AscW(text3) <= 101)) {
                        string_1 += text3;
                    }
                }
                int arg_AB_0 = 0;
                int num2 = string_1.Length - 1;
                for (int j = arg_AB_0; j <= num2; j++) {
                    string value = string_1.Substring(j, 1);
                    string str = string.Empty;
                    try {
                        str = Conversions.ToString(Convert.ToInt32(value, 16));
                    } catch (Exception expr_D7) {
                        ProjectData.SetProjectError(expr_D7);
                        str = "9";
                        ProjectData.ClearProjectError();
                    }
                    text += str;
                }
                return text;
            }
        }

        private static string smethod_18(string string_1, bool bool_0, int int_10) {
            string text = "";
            checked {
                if (bool_0) {
                    int arg_15_0 = 1;
                    int num = string_1.Length - 1;
                    int num2 = arg_15_0;
                    while ((int_10 >> 31 ^ num2) <= (int_10 >> 31 ^ num)) {
                        text += string_1.Substring(num2, 1);
                        num2 += int_10;
                    }
                } else {
                    int arg_49_0 = 0;
                    int num3 = string_1.Length - 1;
                    int num4 = arg_49_0;
                    while ((int_10 >> 31 ^ num4) <= (int_10 >> 31 ^ num3)) {
                        text += string_1.Substring(num4, 1);
                        num4 += int_10;
                    }
                }
                return text;
            }
        }

        public static GraphicsPath CreateRoundRectPath(Rectangle Rect, int TLRadius, int TRRadius, int BRRadius, int BLRadius, bool CorrectForDrawPath) {
            Rectangle rectangle = Rect;
            GraphicsPath graphicsPath = new GraphicsPath();
            Rectangle rect = new Rectangle(Point.Empty, Size.Empty);
            checked {
                int num;
                if (CorrectForDrawPath) {
                    rectangle.Width -= 2;
                    rectangle.Height -= 2;
                    num = -1;
                } else {
                    rectangle.Offset(-1, -1);
                    rectangle.Width -= 5;
                    rectangle.Height -= 5;
                    num = -6;
                }
                Size size = new Size(TLRadius - num, TLRadius - num);
                rect.Size = size;
                rect.X = rectangle.Left;
                rect.Y = rectangle.Top;
                graphicsPath.AddArc(rect, 180f, 90f);
                size = new Size(TRRadius - num, TRRadius - num);
                rect.Size = size;
                rect.X = rectangle.Right - TRRadius;
                rect.Y = rectangle.Top;
                graphicsPath.AddArc(rect, 270f, 90f);
                size = new Size(BRRadius - num, BRRadius - num);
                rect.Size = size;
                rect.X = rectangle.Right - BRRadius;
                rect.Y = rectangle.Bottom - BRRadius;
                if (BRRadius == 0) {
                    if (CorrectForDrawPath) {
                        graphicsPath.AddLine(Rect.Right - 1, Rect.Bottom - 1, Rect.Right - 1, Rect.Bottom - 1);
                    } else {
                        graphicsPath.AddLine(Rect.Right, Rect.Bottom, Rect.Right, Rect.Bottom);
                    }
                } else {
                    graphicsPath.AddArc(rect, 0f, 90f);
                }
                size = new Size(BLRadius - num, BLRadius - num);
                rect.Size = size;
                rect.X = rectangle.Left;
                rect.Y = rectangle.Bottom - BLRadius;
                graphicsPath.AddArc(rect, 90f, 90f);
                graphicsPath.CloseFigure();
                return graphicsPath;
            }
        }

        public static string GetOSBit() {
            if (Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE").IndexOf("64") < 0) {
                return "32bit";
            }
            return "64bit";
        }


    }

}
