﻿namespace org.jiechan.main {
    partial class j_base {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(j_base));
            this.notiICON = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextmenutuopan = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.contextmenutuopan.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonManager
            // 
            this.kryptonManager.GlobalPalette = this.PaletteRenZhe;
            // 
            // PaletteRenZhe
            // 
            this.PaletteRenZhe.FormStyles.FormMain.StateCommon.Border.Color1 = System.Drawing.Color.Black;
            this.PaletteRenZhe.FormStyles.FormMain.StateCommon.Border.ColorStyle = ComponentFactory.Krypton.Toolkit.PaletteColorStyle.Rounding4;
            this.PaletteRenZhe.FormStyles.FormMain.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.PaletteRenZhe.FormStyles.FormMain.StateCommon.Border.Rounding = 2;
            this.PaletteRenZhe.HeaderStyles.HeaderForm.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.PaletteRenZhe.HeaderStyles.HeaderForm.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.PaletteRenZhe.ToolMenuStatus.ToolStrip.ToolStripBorder = System.Drawing.Color.LightGray;
            this.PaletteRenZhe.ToolMenuStatus.ToolStrip.ToolStripGradientBegin = System.Drawing.Color.Transparent;
            this.PaletteRenZhe.ToolMenuStatus.ToolStrip.ToolStripGradientEnd = System.Drawing.Color.LightGray;
            this.PaletteRenZhe.ToolMenuStatus.ToolStrip.ToolStripGradientMiddle = System.Drawing.Color.WhiteSmoke;
            this.PaletteRenZhe.ToolMenuStatus.ToolStrip.ToolStripText = System.Drawing.Color.Black;
            this.PaletteRenZhe.ToolMenuStatus.UseRoundedEdges = ComponentFactory.Krypton.Toolkit.InheritBool.False;
            // 
            // notiICON
            // 
            this.notiICON.Icon = ((System.Drawing.Icon)(resources.GetObject("notiICON.Icon")));
            this.notiICON.Text = "忍者X4营销平台";
            this.notiICON.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notiICON_MouseClick);
            // 
            // contextmenutuopan
            // 
            this.contextmenutuopan.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.contextmenutuopan.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4});
            this.contextmenutuopan.Name = "contextmenutuopan";
            this.contextmenutuopan.Size = new System.Drawing.Size(147, 92);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(146, 22);
            this.toolStripMenuItem1.Text = "官方论坛";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(146, 22);
            this.toolStripMenuItem2.Text = "官方淘宝商店";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(146, 22);
            this.toolStripMenuItem3.Text = "帮助博客";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(146, 22);
            this.toolStripMenuItem4.Text = "退出";
            // 
            // j_base
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 372);
            this.Name = "j_base";
            this.Text = "Form1";
            this.SizeChanged += new System.EventHandler(this.j_base_SizeChanged);
            this.contextmenutuopan.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.NotifyIcon notiICON;
        private System.Windows.Forms.ContextMenuStrip contextmenutuopan;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
    }
}

