using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using org.jiechan.main.models;
using org.jiechan.main.core;
using Amib.Threading;
using org.jiechan.service;

namespace org.jiechan.main {
    public partial class j_taskadd : j_base {

        public taskmodel task = new taskmodel();

        public j_taskadd(taskmodel _task) {
            task = _task;
            InitializeComponent();
        }

        private void txtstockcode_TextChanged(object sender, EventArgs e) {
            if (txtstockcodenum.Text.Length == 6) {

                new SmartThreadPool().QueueWorkItem(() => {
                    stockmodel model = sinahq.GetInstance().stockinfo(txtstockcodenum.Text);
                    txtstockname.Text = model.name;
                    txtstockprice.Text = model.price.ToString();
                    txtbuyprice.Text = (model.price * 0.95).ToString("F2");

                });

            }

        }


        private void j_taskadd_Load(object sender, EventArgs e) {
            load();
            txtstockcode_TextChanged(sender, e);
            tabControl1_SelectedIndexChanged(sender, e);
        }
        private void load() {
            groupBox1.Text = "任务基本信息：" + task.id;
            txtwhydothis.Text = task.whydothis;
            txtstockcodenum.Text = task.stock.codenum;
            txtstockname.Text = task.stock.name;
            txtstockprice.Text = task.stock.price.ToString();
            txtoptpercent.Value = Convert.ToInt32(task.optpercent);
            txtbuyprice.Text = task.buyprice.ToString();
            switch (task.plan) {
                case taskplan.勾头:
                    btngoutou.Checked = true;
                    break;
                case taskplan.区间:
                    btnqujian.Checked = true;
                    break;
                case taskplan.单点:
                    btndandian.Checked = true;
                    break;
                case taskplan.大盘:
                    btndapan.Checked = true;
                    break;
                default:
                    btndandian.Checked = true;
                    break;
            }
            txtbuycompare.Text = task.buycompare.ToString();
            txtbuycompare2.Text = task.buycompare.ToString();//跟随select
            txtbuycompare3.Text = task.buycompare.ToString();//跟随select

            switch (task.sign) {
                case tasksign.全部满足:
                    btnsign_all.Checked = true;
                    break;
                case tasksign.任意一条:
                    btnsign_one.Checked = true;
                    break;
                default:
                    btnsign_one.Checked = true;
                    break;
            }

            txtbuy000001.Text = task.buy000001.ToString();
            txtbuy399001.Text = task.buy399001.ToString();
            txtbuy399006.Text = task.buy399006.ToString();
            btnbuy000001.Checked = task.buy000001bool;
            btnbuy399001.Checked = task.buy399001bool;
            btnbuy399006.Checked = task.buy399006bool;
            //
            txtbuy000001.Enabled = task.buy000001bool;
            txtbuy399001.Enabled = task.buy399001bool;
            txtbuy399006.Enabled = task.buy399006bool;
            txtbuycompare2.Enabled = task.buy399001bool;
            txtbuycompare3.Enabled = task.buy399006bool;

            switch (task.buyorsell) {
                case taskbuyorsell.买入:
                    groupBox3.Enabled = false;
                    tabControl1.SelectedIndex = 0;
                    break;
                case taskbuyorsell.卖出:
                    tabControl1.SelectedIndex = 1;
                    groupBox1.Enabled = groupBox2.Enabled = groupBox5.Enabled = false;
                    break;
                default:
                    break;
            }

            //


        }

        private void btn保存_Click(object sender, EventArgs e) {
            this.DialogResult = DialogResult.OK;
            taskmodel model = new taskmodel();

            //             model.stock = sinahq.GetInstance().stockinfo(txtstockcode.Text);
            //             model.optnum = int.Parse(txtnum.Text);
            //             model.stopprofit1 = float.Parse(txtstopprofit.Text);
            //             model.stoploss = float.Parse(txtstoploss.Text);
            //             model.whydothis = txtwhy.Text;

            taskopt.GetInstance().SaveTask(model);

        }

        private void btn取消_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void kryptonTrackBar1_ValueChanged(object sender, EventArgs e) {
            txtdycw.Text = "动用" + txtoptpercent.Value + "0%仓位，可购买2000股";
        }

        private void txtbuycompare_SelectedIndexChanged(object sender, EventArgs e) {
            txtbuycompare2.Text = txtbuycompare3.Text = txtbuycompare.Text;
        }

        #region ==================================大盘参照，勾选控件的可用不可用变化。=========================
        private void btnbuy000001_CheckedChanged(object sender, EventArgs e) {
            txtbuycompare.Enabled = txtbuy000001.Enabled = btnbuy000001.Checked;
        }

        private void btnbuy399001_CheckedChanged(object sender, EventArgs e) {
            txtbuycompare2.Enabled = txtbuy399001.Enabled = btnbuy399001.Checked;
        }

        private void btnbuy399006_CheckedChanged(object sender, EventArgs e) {
            txtbuycompare3.Enabled = txtbuy399006.Enabled = btnbuy399006.Checked;
        }
        #endregion

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e) {

            switch (tabControl1.SelectedTab.Text) {
                case "买入操作": {
                        tabControl1.Size = new Size(tabControl1.Size.Width, groupBox1.Size.Height + groupBox2.Size.Height + groupBox5.Size.Height + 37);
                        this.Size = new Size(this.Size.Width, pictureBox1.Size.Height + toolStrip1.Size.Height + tabControl1.Size.Height + 29);
                        break;
                    }
                case "卖出操作": {
                        tabControl1.Size = new Size(tabControl1.Size.Width, groupBox3.Size.Height + 37);
                        this.Size = new Size(this.Size.Width, pictureBox1.Size.Height + toolStrip1.Size.Height + tabControl1.Size.Height + 29);
                        break;
                    }
                case "备注说明": {
                        tabControl1.Size = new Size(tabControl1.Size.Width, groupBox6.Size.Height + 37);
                        this.Size = new Size(this.Size.Width, pictureBox1.Size.Height + toolStrip1.Size.Height + tabControl1.Size.Height + 29);
                        break;
                    }
            }


        }

    }
}
