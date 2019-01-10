using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using org.jiechan.service;
using org.jiechan.encrypt;

namespace org.jiechan.main {
    public partial class j_base : baseform {

        public static INIHelper ini = new INIHelper(PathHelper.ConfigPath + "\\Setup.ini");
        public static X_Waiting wait = new X_Waiting();
        public static ModelUser mu;


        public j_base() {
            InitializeComponent();
            kryptonManager.GlobalPalette = PaletteRenZhe;
            Form.CheckForIllegalCrossThreadCalls = false;
        }

        /// <summary>
        /// 撑大LV的行高
        /// </summary>
        protected void listViewHeight(ListView lv, int height) {
            ImageList imgList = new ImageList();
            imgList.ImageSize = new Size(1, height);//分别是宽和高
            lv.SmallImageList = imgList;   //这里设置listView的SmallImageList ,用imgList将其撑大
        }

        /// <summary>
        /// 隐藏在右下角的任务托盘中,点击显示出来.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void notiICON_MouseClick(object sender, MouseEventArgs e) {
            if (e.Button == System.Windows.Forms.MouseButtons.Left) {
                if (!Visible) {
                    this.Visible = true;
                    this.WindowState = FormWindowState.Normal;
                    this.TopMost = true;
                    this.TopMost = false;
                }
            }
        }

        private void j_base_SizeChanged(object sender, EventArgs e) {
            if (this.WindowState == FormWindowState.Minimized) {
                this.Visible = false;
                notiICON.Visible = true;
                this.notiICON.ShowBalloonTip(3000, "我在这里", "忍者交易助手，点击显示界面！", ToolTipIcon.Info);
            } else {
                notiICON.Visible = false;
            }
        }


    }
}
