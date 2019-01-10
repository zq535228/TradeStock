using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using org.jiechan.service;

namespace org.jiechan.service {

    public partial class X_TextBox : baseform {

        public string InputStr;

        public X_TextBox(object str) {
            InitializeComponent();
            this.InputStr = str == null ? "" : str.ToString();
            this.txtInputStr.Text = InputStr;
        }

        public X_TextBox(string str) {
            InitializeComponent();
            this.InputStr = str;
            this.txtInputStr.Text = str;
        }

        private void TS_保存_Click(object sender, EventArgs e) {
            this.InputStr = txtInputStr.Text;
            this.DialogResult = DialogResult.OK;
        }

        private void TS_取消_Click(object sender, EventArgs e) {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
