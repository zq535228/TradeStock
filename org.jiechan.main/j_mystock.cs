using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.Collections;
using org.jiechan.main.models;
using org.jiechan.main.core;
using org.jiechan.service;

namespace org.jiechan.main {
    public partial class j_mystock : DockContent {
        public X_Waiting wait = new X_Waiting();

        public j_mystock() {
            InitializeComponent();
        }

        private void j_mystock_Load(object sender, EventArgs e) {
            loadmystock();
        }

        private void loadmystock() {
            tv.Nodes.Clear();
            //IList<mystockmodel> slt = mystockopt.GetInstance().GetStockList();
            accountmodel acc = comm.GetInstance().getaccount();

            TreeNode TNode1 = new TreeNode(acc.holdingpercents + "% " + "股票持仓（" + acc.winlosepercent + "%）");
            TNode1.ForeColor = acc.winlose > 0 ? Color.Red : Color.Green;
            TreeNode TNode2 = new TreeNode("自选股");

            tv.Nodes.Add(TNode1);
            tv.Nodes.Add(TNode2);

            if (comm.GetInstance().istradlogined()) {
                List<holdingstockmodel> slt = comm.GetInstance().getholdingstocks();
                for (int i = 0; i < slt.Count; i++) {
                    //如果持有就是持仓   47%-万达院线（+2%）
                    TreeNode tmp = new TreeNode();
                    string flt = (slt[i].marketvalue / acc.totalmoney * 100).ToString("F1");
                    tmp.Text = flt + "% " + slt[i].stock.name + "（" + slt[i].profitrate.ToString("F1") + "%）";
                    tmp.Tag = slt[i].stock.codenum;
                    tmp.ForeColor = slt[i].profitrate > 0 ? Color.Red : Color.Green;
                    TNode1.Nodes.Add(tmp);
                }
            } else {
                TreeNode tmp = new TreeNode();
                tmp.Text = "交易已断开";
                tmp.ForeColor = Color.Red;
                TNode1.Nodes.Add(tmp);
            }


            List<stockmodel> slt2 = stockopt.GetInstance().GetStockList();
            for (int i = 0; i < slt2.Count; i++) {
                //如果没有持仓就是自选股
                TreeNode tmp = new TreeNode();
                tmp.Text = slt2[i].name + "（" + slt2[i].codenum + "）";
                tmp.Tag = slt2[i].codenum;
                TNode2.Nodes.Add(tmp);
            }

            tv.ExpandAll();


        }

        private void bsmAdd_Click(object sender, EventArgs e) {
            j_mystockadd w = new j_mystockadd();
            if (w.ShowDialog() == DialogResult.OK) {
                this.DialogResult = DialogResult.OK;
                EchoHelper.Echo("保存成功！", "", EchoHelper.EchoType.淡蓝信息);

            }
            loadmystock();
        }

        private void bsmDel_Click(object sender, EventArgs e) {
            int id = Convert.ToInt32(tv.SelectedNode.Tag);
            holdingstockopt.GetInstance().DelStock(id);
            loadmystock();
        }

        #region =============================闪电买入，闪电卖出====================================
        private void 闪电买入_Click(object sender, EventArgs e) {
            if (tv.SelectedNode.Tag == null || tv.SelectedNode.Tag.ToString() == "") {
                EchoHelper.ShowBalloon("请您确认", "请选择您要操作的个股", tv);
                return;
            }
            wait.ShowMsg("获取最新股价，并计算仓位数量。请稍候...");
            string tmp = tv.SelectedNode.Tag.ToString();
            stockmodel st = sinahq.GetInstance().stockinfo(tmp);
            accountmodel ac = comm.GetInstance().getaccount();
            double tmp_num = Math.Round((ac.totalmoney / st.price / 10), 0);
            int buynum = Convert.ToInt32(Math.Round(tmp_num / 100, 0) * 100);
            buynum = st.price * buynum < ac.usable ? buynum : Convert.ToInt32(Math.Round(Math.Round((ac.usable / st.price) - 99, 0) / 100, 0) * 100);

            wait.CloseMsg();
            if (EchoHelper.ShowDialog2("确认买入", "买入【" + st.name + "(" + st.codenum + ")】，" + buynum + "股？现价" + st.price + "，耗费资金：" + st.price * buynum + "元") == DialogResult.OK) {
                comm.GetInstance().buystock(tv.SelectedNode.Tag.ToString(), 7.6, 100);
            }
        }

        private void 闪电卖出_Click(object sender, EventArgs e) {
            if (tv.SelectedNode.Tag == null || tv.SelectedNode.Tag.ToString() == "") {
                EchoHelper.ShowBalloon("请您确认", "请选择您要操作的个股", tv);
                return;
            }
            if (tv.SelectedNode.Parent.Text == "自选股") {
                EchoHelper.ShowDialog("自选股只能买入，因为你还没有仓位哦！", EchoHelper.MessageType.错误);
                return;
            }
            wait.ShowMsg("获取最新股价，并计算仓位数量。请稍候...");
            string tmp = tv.SelectedNode.Tag.ToString();
            stockmodel st = sinahq.GetInstance().stockinfo(tmp);
            accountmodel ac = comm.GetInstance().getaccount();
            double tmp_num = Math.Round((ac.totalmoney / st.price / 10), 0);
            int sellnum = Convert.ToInt32(Math.Round(tmp_num / 100 - 99, 0) * 100);
            //比较现有持仓是否购卖
            holdingstockmodel hstm = comm.GetInstance().getholdingstocks().Find(delegate(holdingstockmodel tmpst) { return tmpst.stock.codenum == tmp; });
            sellnum = sellnum > hstm.usablenum ? hstm.usablenum : sellnum;
            wait.CloseMsg();
            if (EchoHelper.ShowDialog2("确认卖出", "卖出【" + st.name + "(" + st.codenum + ")】，" + sellnum + "股？现价" + st.price + "，回笼资金：" + st.price * sellnum + "元") == DialogResult.OK) {
                //comm.GetInstance().buystock(tv.SelectedNode.Tag.ToString(), 7.6, 100);

            }

        }
        #endregion



        private void menu_Opening(object sender, CancelEventArgs e) {
            if (comm.GetInstance().istradlogined()) {
                btntaskbuy.Enabled = true;
                btnquickbuy.Enabled = true;
                btntasksell.Enabled = true;
                btnquicksell.Enabled = true;
            } else {
                btntaskbuy.Enabled = false;
                btnquickbuy.Enabled = false;
                btntasksell.Enabled = false;
                btnquicksell.Enabled = false;
            }
            if (tv.SelectedNode.Parent.Text == "自选股") {
                btntasksell.Enabled = false;
                btnquicksell.Enabled = false;
            } else {
                btndelmystock.Enabled = false;
            }
        }

        private void 挂单买入_Click(object sender, EventArgs e) {
            taskmodel t = new taskmodel();
            stockmodel s = new stockmodel();
            s.codenum = tv.SelectedNode.Tag.ToString();
            t.stock = s;
            t.buyorsell = taskbuyorsell.买入;
            DialogResult dr = new j_taskadd(t).ShowDialog();

            if (dr == DialogResult.OK) {

            }

        }

        private void 挂单卖出_Click(object sender, EventArgs e) {
            taskmodel t = new taskmodel();
            stockmodel s = new stockmodel();
            s.codenum = tv.SelectedNode.Tag.ToString();
            t.stock = s;
            t.buyorsell = taskbuyorsell.卖出;

            DialogResult dr = new j_taskadd(t).ShowDialog();

            if (dr == DialogResult.OK) {

            }

        }


    }
}
