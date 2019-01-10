using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using org.jiechan.main.models;
using org.jiechan.service;
using org.jiechan.main.core;
using Amib.Threading;

namespace org.jiechan.main {
    public partial class j_mystockadd : j_base {

        public holdingstockmodel my = new holdingstockmodel();

        public j_mystockadd() {
            InitializeComponent();
        }

        private void j_mystockadd_Load(object sender, EventArgs e) {
            txtstocknum.Focus();
        }

        private void toolStripButton1_Click(object sender, EventArgs e) {
            stockmodel st = new stockmodel();
            st.name = txtstockname.Text;
            st.codenum = txtstocknum.Text;


            if (!stockopt.GetInstance().exist(st.codenum)) {
                stockopt.GetInstance().SaveStock(st);
               
            } else {
                EchoHelper.Echo("已存在", EchoHelper.EchoType.红色信息);
            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;


        }

        private void txtstocknum_TextChanged(object sender, EventArgs e) {
            if (txtstocknum.Text.Length == 6) {

                new SmartThreadPool().QueueWorkItem(() => {
                    stockmodel model = sinahq.GetInstance().stockinfo(txtstocknum.Text);
                    txtstockname.Text = model.name;
                    txtprice.Text = model.price.ToString();
                    //股票k线图
                    pkline.Image = sinahq.GetInstance().getbitmap(txtstocknum.Text);
                });
                
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e) {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;

        }




    }
}
