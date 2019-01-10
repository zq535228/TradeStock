using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using org.jiechan.main.models;
using Amib.Threading;
using org.jiechan.service;

namespace org.jiechan.main {
    public partial class j_task : DockContent {




        public j_task() {
            InitializeComponent();
        }

        private void j_task_Load(object sender, EventArgs e) {
            new SmartThreadPool().QueueWorkItem(() => {
                lvload();
            });

        }

        private void lvload() {
            lv.Items.Clear();
            List<taskmodel> lt = taskopt.GetInstance().GetTaskList();
            foreach (taskmodel item in lt) {
                ListViewItem lvsub = new ListViewItem(item.id.ToString());


                lvsub.SubItems.Add(item.stock.name);
                lvsub.SubItems.Add(item.stock.price.ToString());
                lvsub.SubItems.Add(item.optnum1.ToString());

                lv.Items.Add(lvsub);
            }

            listViewHeight(lv, 20);
        }

        private void 挂单买卖ToolStripMenuItem_Click(object sender, EventArgs e) {
            EchoHelper.ShowDialog("请在左侧[我的股票]中，右键添加任务！", EchoHelper.MessageType.提示);
        }
    }
}
