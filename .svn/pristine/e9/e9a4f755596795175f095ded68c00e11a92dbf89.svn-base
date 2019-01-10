using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using org.jiechan.service;
using org.jiechan.main.core;
using Amib.Threading;
using System.Threading;
using ComponentFactory.Krypton.Toolkit;

namespace org.jiechan.main {
    public partial class j_main : j_base {

        public j_main() {
            InitializeComponent();
            wait.CloseMsg();
        }

        private void j_main_Load(object sender, EventArgs e) {


            new j_jiechan().Show(this.dockPanelMain);
            new j_mystock().Show(this.dockPanelMain);
            new j_taskover().Show(this.dockPanelMain);
            new j_taskinfo().Show(this.dockPanelMain);


            j_task task = new j_task();
            task.Show(this.dockPanelMain);
            task.DockTo(dockPanelMain.Panes[2], DockStyle.Fill, 0);




            wait.CloseMsg();


            // 创建一个线程池
            new SmartThreadPool().QueueWorkItem(() => {
                spvoice.GetInstance().speakText("欢迎使用忍者交易助手。祝您交易愉快！");

                while (true) {
                    hqconnect();
                    Thread.Sleep(1000);
                }

            });
        }



        private void hqconnect() {

            hqsh.Text = "上证指数：" + sinahq.GetInstance().marketindex("s_sh000001").price.ToString();
            hqsz.Text = "深证成指：" + sinahq.GetInstance().marketindex("s_sz399001").price.ToString();
            hqcyb.Text = "创业板：" + sinahq.GetInstance().marketindex("s_sz399006").price.ToString();

            if (comm.GetInstance().istradlogined()) {
                qhconn.Text = "交易连接成功：" + TimeHelper.DateTimeStringTime();
                qhconn.ForeColor = Color.Black;
            } else {
                qhconn.Text = "交易已断开";
                qhconn.ForeColor = Color.Red;

            }


        }

        private void btnrefresh_Click(object sender, EventArgs e) {
        }

        private void btnhiddentrade_Click(object sender, EventArgs e) {
            if (comm.GetInstance().istradlogined()) {

                if (btnhiddentrade.Text == "隐藏交易端") {
                    comm.GetInstance().hiddentradewind();
                    btnhiddentrade.Text = "显示交易端";
                } else {
                    comm.GetInstance().showtradewind();
                    btnhiddentrade.Text = "隐藏交易端";
                }
            } else {
                btnhiddentrade.Text = "关联同花顺";

            }

        }

        /// <summary>
        /// 重写关闭方法 增加弹出确定关闭对话框.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e) {
            e.Cancel = true;

            //             if (runNum > 0 && KryptonMessageBox.Show("您还有运行中的任务，有可能造成数据丢失，确认退出？", "请确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK) {
            //                 e.Cancel = false;
            //             } else {
            //                 e.Cancel = true;
            //             }
            if (KryptonMessageBox.Show("真的要退出【忍者交易助手】？", "请确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK) {
                e.Cancel = false;
            } else {
                e.Cancel = true;
            }


            base.OnClosing(e);
        }

        private void btnconsole_Click(object sender, EventArgs e) {
            Console.Clear();//清空控制台信息

        }


        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void 程序设定ToolStripMenuItem_Click(object sender, EventArgs e) {
            if (new j_set().ShowDialog() == DialogResult.OK) {
                EchoHelper.Echo("程序设定完成！");
            } else {
                EchoHelper.Echo("程序设定未保存！");

            }
        }

    }
}
