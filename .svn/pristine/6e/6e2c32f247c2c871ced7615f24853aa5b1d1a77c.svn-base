using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.VisualBasic.CompilerServices;
using System.Windows.Forms;
using Amib.Threading;
using org.jiechan.service;
using org.jiechan.main.models;
using System.Net;
using System.Drawing;
using System.Security.Permissions;
using Microsoft.VisualBasic;
using System.Collections;

namespace org.jiechan.main.core {
    public class comm {

        public static comm GetInstance() {
            // 定义一个标识确保线程同步

            if (db == null) {
                lock (locker) {
                    if (db == null) {
                        db = new comm();
                    }

                }
            }
            return db;
        }

        private static readonly object locker = new object();

        private static comm db;

        protected comm() {
            ResetAllHandle();
            enumtws();
            istradlogined();
        }



        /// <summary>
        /// 交易窗口的IP
        /// </summary>
        public IntPtr hTradeWindow {
            get;
            set;
        }

        public IntPtr hAfxMDIFrame42s {
            get;
            set;
        }

        public IntPtr hTradeMDIWindow {
            get;
            set;
        }

        public IntPtr hTradeStatusWindow {
            get;
            set;
        }

        public IntPtr hTradeTreeWindow {
            get;
            set;
        }

        public IntPtr hBuyCode_E {
            get;
            set;
        }

        public IntPtr hBuyQuan_E {
            get;
            set;
        }

        public IntPtr hTradeSaleWindow {
            get;
            set;
        }

        public IntPtr hSalePric_E {
            get;
            set;
        }

        public IntPtr hSaleSubm_B {
            get;
            set;
        }

        public IntPtr hUndoSelectAll_B {
            get;
            set;
        }

        public IntPtr hTradeFundWindow {
            get;
            set;
        }

        public IntPtr hFundFreezed_S {
            get;
            set;
        }

        public IntPtr hFundCanDraw_S {
            get;
            set;
        }

        public IntPtr hFundAllFund_S {
            get;
            set;
        }

        public IntPtr hFundComplet_S {
            get;
            set;
        }

        public IntPtr hSaleQuan_E {
            get;
            set;
        }

        public IntPtr hTradeUndoWindow {
            get;
            set;
        }

        public IntPtr hUndoDoUndoAll_B {
            get;
            set;
        }

        public IntPtr hFundBalance_S {
            get;
            set;
        }

        public IntPtr hFundCanUsed_S {
            get;
            set;
        }

        public IntPtr hFundStkValu_S {
            get;
            set;
        }

        public IntPtr hBuySubm_B {
            get;
            set;
        }

        public IntPtr hSaleCode_E {
            get;
            set;
        }

        public IntPtr hTradeBuyWindow {
            get;
            set;
        }

        public IntPtr hBuyPric_E {
            get;
            set;
        }

        public void ResetAllHandle() {
            hAfxMDIFrame42s = (IntPtr)0;
            hTradeMDIWindow = (IntPtr)0;
            hTradeStatusWindow = (IntPtr)0;
            hTradeTreeWindow = (IntPtr)0;
            hTradeBuyWindow = (IntPtr)0;
            hBuyCode_E = (IntPtr)0;
            hBuyPric_E = (IntPtr)0;
            hBuyQuan_E = (IntPtr)0;
            hBuySubm_B = (IntPtr)0;
            hTradeSaleWindow = (IntPtr)0;
            hSaleCode_E = (IntPtr)0;
            hSalePric_E = (IntPtr)0;
            hSaleQuan_E = (IntPtr)0;
            hSaleSubm_B = (IntPtr)0;
            hTradeUndoWindow = (IntPtr)0;
            hUndoSelectAll_B = (IntPtr)0;
            hUndoDoUndoAll_B = (IntPtr)0;
            hTradeFundWindow = (IntPtr)0;
            hFundBalance_S = (IntPtr)0;
            hFundFreezed_S = (IntPtr)0;
            hFundCanUsed_S = (IntPtr)0;
            hFundCanDraw_S = (IntPtr)0;
            hFundStkValu_S = (IntPtr)0;
            hFundAllFund_S = (IntPtr)0;
            hFundComplet_S = (IntPtr)0;
        }


        private IntPtr[] enumtws() {
            IntPtr[] tmp = comcore.EnumVisibleWindows();
            IntPtr[] re = new IntPtr[0];
            hTradeWindow = (IntPtr)0;
            int num = tmp.Length - 1;
            for (int i = 0; i <= num; i++) {
                IntPtr intPtr = tmp[i];
                if (comcore.GetWindowText(intPtr, false).Contains("网上股票交易系统5.0")) {
                    hTradeWindow = intPtr;
                }
            }
            return re;
        }

        public IntPtr GetTipWindow() {
            IntPtr[] tmp = comcore.EnumVisibleWindows();
            int num = tmp.Length - 1;
            for (int i = 0; i <= num; i++) {
                IntPtr intPtr = tmp[i];
                if (comcore.GetWindow(intPtr, 4) == hTradeWindow) {
                    return intPtr;
                }
            }
            return IntPtr.Zero;
        }

        public string GetTipWindowTitleAndPrompt(IntPtr hTipWnd) {
            string text = "";
            string text2 = "";
            string text3 = "Static";
            string text4 = null;
            IntPtr intPtr = comcore.FindWindowEx(hTipWnd, IntPtr.Zero, ref text3, ref text4);
            while ((int)intPtr != 0) {
                if (comcore.IsWindowVisible(intPtr)) {
                    text2 = comcore.GetWindowText(intPtr, true);
                    if (text2.Length > 3) {
                        break;
                    }
                }
                text4 = "Static";
                text3 = null;
                intPtr = comcore.FindWindowEx(hTipWnd, intPtr, ref text4, ref text3);
            }
            text4 = "Static";
            text3 = null;
            intPtr = comcore.FindWindowEx(hTipWnd, intPtr, ref text4, ref text3);
            while ((int)intPtr != 0) {
                if (comcore.IsWindowVisible(intPtr)) {
                    text = comcore.GetWindowText(intPtr, true);
                    if (text.Length > 1) {
                        break;
                    }
                }
                text4 = "Static";
                text3 = null;
                intPtr = comcore.FindWindowEx(hTipWnd, intPtr, ref text4, ref text3);
            }

            if (Operators.CompareString(text, "", false) == 0 & Operators.CompareString(text2, "", false) == 0) {
                return "";
            }
            string text5 = "［" + text + "］: " + text2;
            text5 = text5.Replace("\r", "，").Replace("\n", "，").Replace("\r\n", "，");
            return text5.Replace("，，", "，").Replace("，，", "，").Replace("，，", "，");
        }

        public IntPtr GetTipWindowFirstButton(IntPtr hTipWnd) {
            string text = "Button";
            string text2 = null;
            return comcore.FindWindowEx(hTipWnd, IntPtr.Zero, ref text, ref text2);
        }


        /// <summary>
        /// 返回AfxMDIFrame42s窗口的是否存在，代表是否已经登录
        /// </summary>
        /// <returns></returns>
        public bool istradlogined() {
            string t1 = null;
            string t2 = "AfxMDIFrame42s";
            IntPtr re = comcore.FindWindowEx(hTradeWindow, IntPtr.Zero, ref t2, ref t1);
            hAfxMDIFrame42s = re;
            return re != IntPtr.Zero;

        }

        #region =========================获取当下持仓LIST===================================

        public List<holdingstockmodel> getholdingstocks() {

            if (hAfxMDIFrame42s == IntPtr.Zero) {
                return new List<holdingstockmodel>();
            }

            comcore.PostMsgKeyPress(hTradeWindow, Keys.F4, 1000);

            List<holdingstockmodel> mys = new List<holdingstockmodel>();

            IntPtr[] re = EnumVisible32770Windows();
            //             IntPtr[] re = new IntPtr[0];
            // 
            //             string t1 = null;
            //             string t2 = "#32770";
            //             IntPtr hWnd_tmp = comcore.FindWindowEx(hAfxMDIFrame42s, IntPtr.Zero, ref t2, ref t1);
            // 
            //             while ((int)hWnd_tmp != 0) {
            //                 if (comcore.IsWindowVisible_1(hWnd_tmp)) {
            //                     Array.Resize<IntPtr>(ref re, re.Length + 1);
            //                     re[re.Length - 1] = hWnd_tmp;
            //                 }
            //                 t2 = "#32770";
            //                 t1 = null;
            //                 hWnd_tmp = comcore.FindWindowEx(hAfxMDIFrame42s, hWnd_tmp, ref t2, ref t1);
            //             }

            //找到了可见的3227窗口,继续寻找里面的Gpid
            IntPtr Gpid = IntPtr.Zero;
            if (re.Length > 0) {
                IntPtr h = re[0];
                IntPtr arg_1A_1 = (IntPtr)0;
                string t1 = "Afx:400000:0";
                string t2 = null;
                IntPtr intPtr = comcore.FindWindowEx(h, IntPtr.Zero, ref t1, ref t2);
                IntPtr arg_33_0 = intPtr;
                t1 = "AfxWnd42s";
                t2 = null;
                IntPtr intPtr2 = comcore.FindWindowEx(arg_33_0, IntPtr.Zero, ref t1, ref t2);
                IntPtr arg_4C_0 = intPtr2;
                t1 = "CVirtualGridCtrl";
                t2 = null;
                Gpid = comcore.FindWindowEx(arg_4C_0, IntPtr.Zero, ref t1, ref t2);

            }
            if (Gpid != IntPtr.Zero) {
                string txt = getcvgtext(Gpid);
                mys = getholdinglist(txt);

            }
            return mys;

        }

        private string getcvgtext(IntPtr hWndList) {

            string result = "";
            try {
                Clipboard.Clear();
                comcore.PostMsgWMCmd(hWndList, 57634, 10);
                result = clipboardgettext();
            } catch {
            }
            return result;
        }

        private string clipboardgettext() {
            string result = "";
            Thread thread = new Thread(new ThreadStart(() => {
                result = Clipboard.GetText();
            }));
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();
            return result;
        }

        private List<holdingstockmodel> getholdinglist(string strClipboard) {
            List<holdingstockmodel> mlist = new List<holdingstockmodel>();
            string[] ss = strClipboard.Split('\n');
            string[] ss0_split = ss[0].Split('\t');

            //             string ss0 = "证券代码\t证券名称\t股票余额\t可用余额\t参考盈亏\t参考成本价\t参考盈亏比例(%)\t市价\t市值\t买入成本\t交易市场\t股东帐户\t单位数量\t资讯\t\r";
            //             List<string> sl = ss0.Split('\t').ToList();
            //             sl.FindIndex(str => str.Contains("证券代码"));
            //             sl.Find((str) => { return str.Contains("证券代码"); });

            //             sl.Find(delegate(string str) { return str.Contains("代码"); });

            int codenum = ArrayTool<string>.ArrayQueryIndex(ss0_split, "证券代码");
            int stockname = ArrayTool<string>.ArrayQueryIndex(ss0_split, "证券名称");

            int stocknum = ArrayTool<string>.ArrayQueryIndex(ss0_split, "股票余额");
            if (stocknum == -1) {
                stocknum = ArrayTool<string>.ArrayQueryIndex(ss0_split, "今余额");
            }
            if (stocknum == -1) {
                stocknum = ArrayTool<string>.ArrayQueryIndex(ss0_split, "证券数量");
            }
            if (stocknum == -1) {
                stocknum = ArrayTool<string>.ArrayQueryIndex(ss0_split, "库存数量");
            }
            if (stocknum == -1) {
                stocknum = ArrayTool<string>.ArrayQueryIndex(ss0_split, "股份余额");
            }
            if (stocknum == -1) {
                stocknum = ArrayTool<string>.ArrayQueryIndex(ss0_split, "实际数量");
            }

            int usablenum = ArrayTool<string>.ArrayQueryIndex(ss0_split, "可用余额");
            if (usablenum == -1) {
                usablenum = ArrayTool<string>.ArrayQueryIndex(ss0_split, "可卖量");
            }
            if (usablenum == -1) {
                usablenum = ArrayTool<string>.ArrayQueryIndex(ss0_split, "可卖数量");
            }
            if (usablenum == -1) {
                usablenum = ArrayTool<string>.ArrayQueryIndex(ss0_split, "可用股份");
            }

            int profit = ArrayTool<string>.ArrayQueryIndex(ss0_split, "盈亏");
            if (profit == -1) {
                profit = ArrayTool<string>.ArrayQueryIndex(ss0_split, "合计盈亏");
            }
            if (profit == -1) {
                profit = ArrayTool<string>.ArrayQueryIndex(ss0_split, "浮动盈亏");
            }
            if (profit == -1) {
                profit = ArrayTool<string>.ArrayQueryIndex(ss0_split, "参考浮动盈亏");
            }
            if (profit == -1) {
                profit = ArrayTool<string>.ArrayQueryIndex(ss0_split, "持仓盈亏");
            }
            if (profit == -1) {
                profit = ArrayTool<string>.ArrayQueryIndex(ss0_split, "参考盈亏");
            }
            if (profit == -1) {
                profit = ArrayTool<string>.ArrayQueryIndex(ss0_split, "累计浮动盈亏");
            }

            int profitrate = ArrayTool<string>.ArrayQueryIndex2(ss0_split, "盈亏比例");


            if (stocknum == -1 | usablenum == -1 | profit == -1) {
                EchoHelper.Echo("发送获取持仓失败错误！", EchoHelper.EchoType.红色信息);
                return null;
            }

            for (int i = 1; i < ss.Length; i++) {
                string[] s_tmp = ss[i].Split('\t');
                holdingstockmodel my = new holdingstockmodel();

                my.stock = sinahq.GetInstance().stockinfo(s_tmp[codenum]);

                my.num = int.Parse(s_tmp[stocknum]);
                my.usablenum = int.Parse(s_tmp[usablenum]);
                my.profit = float.Parse(s_tmp[profit]);
                my.profitrate = float.Parse(s_tmp[profitrate]);
                mlist.Add(my);
            }

            return mlist;

        }

        #endregion


        private IntPtr[] EnumVisible32770Windows() {
            if (hAfxMDIFrame42s == IntPtr.Zero) {
                EchoHelper.Echo("交易关联失败！");
            }
            IntPtr[] ip32770s = new IntPtr[] { };
            Array.Resize<IntPtr>(ref ip32770s, 0);
            string text = "#32770";
            string text2 = null;
            IntPtr intPtr = comcore.FindWindowEx(hAfxMDIFrame42s, IntPtr.Zero, ref text, ref text2);
            while ((int)intPtr != 0) {
                if (comcore.IsWindowVisible_1(intPtr)) {
                    Array.Resize<IntPtr>(ref ip32770s, ip32770s.Length + 1);
                    ip32770s[ip32770s.Length - 1] = intPtr;
                }
                text2 = "#32770";
                text = null;
                intPtr = comcore.FindWindowEx(hAfxMDIFrame42s, intPtr, ref text2, ref text);
            }
            return ip32770s;
        }


        // ZDXStock.Comn
        public void FindTradeBuyWindow() {
            comcore.PostMsgKeyPress(hTradeWindow, Keys.F1, 1000);
            hTradeBuyWindow = EnumVisible32770Windows()[0];
            if (hTradeWindow != IntPtr.Zero) {
                string text = "Edit";
                string text2 = null;
                hBuyCode_E = comcore.FindWindowEx(hTradeBuyWindow, IntPtr.Zero, ref text, ref text2);
                text2 = "Static";
                text = null;
                hBuyName_S = comcore.FindWindowEx(hTradeBuyWindow, hBuyCode_E, ref text2, ref text);
                text2 = "Edit";
                text = null;
                hBuyPric_E = comcore.FindWindowEx(hTradeBuyWindow, hBuyName_S, ref text2, ref text);
                text2 = "Static";
                text = null;
                hBuyAvai_S = comcore.FindWindowEx(hTradeBuyWindow, hBuyPric_E, ref text2, ref text);
                text2 = "Edit";
                text = null;
                hBuyQuan_E = comcore.FindWindowEx(hTradeBuyWindow, hBuyAvai_S, ref text2, ref text);
                text2 = "Button";
                text = null;
                hBuySubm_B = comcore.FindWindowEx(hTradeBuyWindow, hBuyQuan_E, ref text2, ref text);
            } else {
                EchoHelper.Echo("查找买入窗口失败！");
            }
        }


        public void buystock(string codenum, double price, int int_1) {
            FindTradeBuyWindow();
            string windowText = comcore.GetWindowText(hBuyCode_E, true);
            comcore.SendMsgSetEditText(hBuyCode_E, codenum, 500);
            comcore.SendMsgSetEditText(hBuyPric_E, Conversions.ToString(price), 50);
            comcore.SendMsgSetEditText(hBuyQuan_E, Conversions.ToString(int_1), 50);
            comcore.PostMsgMouseClickControl(hBuySubm_B, 100);
            confirmbox(200);


        }


        private void confirmbox(int sleeptime, int int_2 = 5) {
            Thread.Sleep(sleeptime);
            int num = int_2 - 1;
            for (int i = 0; i <= num; i++) {
                IntPtr tipWindow = GetTipWindow();
                if (tipWindow != IntPtr.Zero) {
                    string tipWindowTitleAndPrompt = GetTipWindowTitleAndPrompt(tipWindow);
                    if (!string.IsNullOrEmpty(tipWindowTitleAndPrompt)) {
                        IntPtr tipWindowFirstButton = GetTipWindowFirstButton(tipWindow);
                        if ((int)tipWindowFirstButton != 0) {
                            comcore.SetForegroundWindow(tipWindow);
                            comcore.PostMsgMouseClickControl(tipWindowFirstButton, 100);
                            EchoHelper.Echo(tipWindowTitleAndPrompt, EchoHelper.EchoType.绿色信息);
                            Thread.Sleep(sleeptime);
                        }
                    }
                }
            }
        }

        public void test() {
            enumtws();
            istradlogined();
            getholdingstocks();
        }

        public void hiddentradewind() {
            int w = System.Windows.Forms.SystemInformation.WorkingArea.Width + 30;
            int h = System.Windows.Forms.SystemInformation.WorkingArea.Height + 130;
            comcore.SetWindowPos(hTradeWindow, 1, w, h, 800, 500, 0x0010);
        }

        public void showtradewind() {
            comcore.SetWindowPos(hTradeWindow, 0, 30, 30, 800, 500, 0x0010);
        }

        public accountmodel getaccount() {
            accountmodel model = new accountmodel();
            if (hAfxMDIFrame42s == IntPtr.Zero) {
                return new accountmodel();
            }

            string[] re = new string[] { };

            string t1 = null;
            string t2 = "#32770";

            IntPtr hWnd_tmp = comcore.FindWindowEx(hAfxMDIFrame42s, IntPtr.Zero, ref t2, ref t1);
            IntPtr fip = comcore.FindWindowEx(hAfxMDIFrame42s, hWnd_tmp, ref t2, ref t1);


            t2 = "Static";
            t1 = null;
            hWnd_tmp = comcore.FindWindowEx(fip, IntPtr.Zero, ref t2, ref t1);

            while ((int)hWnd_tmp != 0) {
                hWnd_tmp = comcore.FindWindowEx(fip, hWnd_tmp, ref t2, ref t1);
                Array.Resize<string>(ref re, re.Length + 1);
                re[re.Length - 1] = comcore.GetWindowText(hWnd_tmp);
            }

            model.totalmoney = float.Parse(re[11]);
            model.usable = float.Parse(re[5]);
            model.holdingpercents = re[18];
            model.winlose = float.Parse(re[40]);
            return model;
        }


        //      mod4(object object_0, string string_1) {
        //             Type arg_37_1 = null;
        //             string arg_37_2 = "Invoke";
        //             object[] array = new object[]
        // 	{
        // 		new Delegate0(Module1.smethod_5),
        // 		string_1
        // 	};
        //             object[] arg_37_3 = array;
        //             string[] arg_37_4 = null;
        //             Type[] arg_37_5 = null;
        //             bool[] array2 = new bool[]
        // 	{
        // 		false,
        // 		true
        // 	};
        //             NewLateBinding.LateCall(object_0, arg_37_1, arg_37_2, arg_37_3, arg_37_4, arg_37_5, array2, true);
        //             if (array2[1]) {
        //                 string_1 = (string)Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array[1]), typeof(string));
        //             }
        //         }

        //             IntPtr[] pstr = new IntPtr[0];
        //             Array.Resize<IntPtr>(ref pstr, 0);
        // 
        //             string text = "#32770";
        //             string text2 = null;
        //             IntPtr intPtr = comcore.FindWindowEx(hWnd_tmp, IntPtr.Zero, ref text, ref text2);
        // 
        //             while ((int)intPtr != 0) {
        //                 Array.Resize<IntPtr>(ref pstr, pstr.Length + 1);
        //                 pstr[pstr.Length - 1] = intPtr;
        // 
        //                 text = "#32770";
        //                 text2 = null;
        //                 intPtr = comcore.FindWindowEx(hWnd_tmp, intPtr, ref text, ref text2);
        //             }


        //         public  IntPtr[] EnumVisible32770Windows(IntPtr hWndParent) {
        //             Array.Resize<IntPtr>(ref Module1.intptr_1, 0);
        //             IntPtr arg_1E_1 = (IntPtr)0;
        //             string text = "#32770";
        //             string text2 = null;
        //             IntPtr intPtr = comcore.FindWindowEx(hWndParent, arg_1E_1, ref text, ref text2);
        //             checked {
        //                 while ((int)intPtr != 0) {
        //                     if (Module1.IsWindowVisible_1(intPtr)) {
        //                         Array.Resize<IntPtr>(ref Module1.intptr_1, Module1.intptr_1.Length + 1);
        //                         Module1.intptr_1[Module1.intptr_1.Length - 1] = intPtr;
        //                     }
        //                     IntPtr arg_68_1 = intPtr;
        //                     text2 = "#32770";
        //                     text = null;
        //                     intPtr = Module1.FindWindowEx(hWndParent, arg_68_1, ref text2, ref text);
        //                 }
        //                 return Module1.intptr_1;
        //             }
        //         }


        //总资产



        public IntPtr hBuyName_S { get; set; }

        public IntPtr hBuyAvai_S { get; set; }
    }
}
