﻿using System;
using System.Drawing;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using System.Collections;
using System.Xml;
using org.jiechan.service;
using org.jiechan.encrypt;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic;


namespace org.jiechan.main {

    public partial class j_login : j_base {

        public j_login() {
            InitializeComponent();
            Form.CheckForIllegalCrossThreadCalls = false;
            new WebServHelper().MemberDoit();
            wait.CloseMsg();
        }

        private void X_Form_Login_Load(object sender, EventArgs e) {
            tablogin_SelectedIndexChanged(sender, e);

            STP.GetInstence().QueueWorkItem(() => {
                th_load();
            });

            STP.GetInstence().QueueWorkItem(() => {
                //checkUpdate();
            });

            wait.ShowMsg("1/10 加载登录窗口中...");
            EchoHelper.Echo("窗體加載完成！請輸入忍者X4的論壇賬戶，論壇密碼，然後登錄！", null, 0);
            UserName.Text = ini.re("登录账户密码", "PID");
            PassWord.Text = ini.re("登录账户密码", "PWD");
            wait.CloseMsg();
        }

        private void th_load() {
            CpuID.Text = HardWare.GetCpuID();
            int count = FilesHelper.ReadDirectoryListCount(Application.StartupPath, ".dll");
            PluginNum.Text = count + "个";
        }


        private void tablogin_SelectedIndexChanged(object sender, EventArgs e) {
            switch (tablogin.SelectedIndex) {
                case 0: {
                        tablogin.Size = new Size(tablogin.Size.Width, groupBox1.Size.Height + 37);
                        this.Size = new Size(this.Size.Width, Pic_Login.Size.Height + tablogin.Size.Height + 35);
                        break;
                    }
                case 1: {
                        tablogin.Size = new Size(tablogin.Size.Width, groupBox2.Size.Height + 37);
                        this.Size = new Size(this.Size.Width, Pic_Login.Size.Height + tablogin.Size.Height + 35);
                        break;
                    }

            }
        }


        private void btnLogin_Click(object sender, EventArgs e) {
            wait.ShowMsg("2/10 登录中，此过程较慢，请稍候...");
            this.DialogResult = DialogResult.OK;

            //判断用户名吗是否为空
            if (string.IsNullOrEmpty(UserName.Text) || string.IsNullOrEmpty(PassWord.Text)) {
                EchoHelper.Echo("用户名密码不能留空，请输入！", "输入错误", EchoHelper.EchoType.红色信息);
                wait.CloseMsg();
                return;
            }
            //保存用户名密码,默认的.
            ini.up("登录账户密码", "PID", UserName.Text);
            if (ckPWD.Checked) {
                ini.up("登录账户密码", "PWD", PassWord.Text);
            }

            //             Thread th = new Thread(new ThreadStart(th_login));
            //             th.Name = "th_login";
            //             th.IsBackground = true;
            //             th.Start();
            STP.GetInstence().QueueWorkItem(() => {
                th_login();
            });
            btnLogin.Enabled = false;

        }

        private void th_login() {

            try {
                ModelUser mm = GetMM(UserName.Text, PassWord.Text);

                //时间在当前允许范围内
                bool isnow = mm.LoginTime > DateTime.Now.AddMinutes(-10) && mm.LoginTime < DateTime.Now.AddMinutes(10);
                if (!isnow && mm.LoginTime.ToString() != "0001/1/1 0:00:00") {
                    mm.CallBackMessage = "服务器时间：" + mm.LoginTime + "，允许误差是±10min，请修正您的本地时间。";
                }

                if (mm.IsSuccess == true && isnow) {
                    wait.ShowMsg("9/10 二次验证通过...");
                    EchoHelper.Echo(mm.CallBackMessage, "系统登录", EchoHelper.EchoType.淡蓝信息);
                    this.DialogResult = DialogResult.OK;
                } else {
                    wait.ShowMsg("验证失败！");
                    EchoHelper.Echo(mm.CallBackMessage, "系统登录", EchoHelper.EchoType.红色信息);
                }
            } catch (Exception ex) {
                EchoHelper.EchoException(ex);
            } finally {
                wait.ShowMsg("10/10 进入系统...");
                wait.CloseMsg();
                btnLogin.Enabled = true;
            }
        }

        public ModelUser GetMM(string uname, string pwd) {

            try {
                if (!string.IsNullOrEmpty(uname) && !string.IsNullOrEmpty(pwd)) {
                    ModelUser tmp = new ModelUser();
                    tmp.UserName = uname;
                    tmp.PassWord = pwd;
                    tmp.IsSuccess = false;
                    tmp.GroupStr = "";
                    tmp.CpuID = CpuID.Text;
                    tmp.CallBackMessage = "";
                    tmp.FromIP = IPHelper.GetIP();
                    tmp.HashKey = StringHelper.getRandCode(4);

                    wait.ShowMsg("3/10 加密验证中，请稍后...");
                    DbTools db = new DbTools();
                    byte[] modelbyte = db.ClasstoBuf(tmp);
                    byte[] modelstrbyte_back = new WebServHelper().ValidateMysql(modelbyte);
                    wait.ShowMsg("4/10 验证完成，授权状态判断...");
                    mu = db.BuftoClass<ModelUser>(modelstrbyte_back);
                }
            } catch (System.Exception ex) {
                EchoHelper.EchoException(ex);
            } finally {
                wait.CloseMsg();
            }
            return mu;
        }

        private void btnVIP_Click(object sender, EventArgs e) {
            ProcessHelper.openUrl("http://vip.renzhe.org/porder.aspx");
        }


        #region 更新。
        private string UpdaterUrl;
        /// <summary>
        /// 检查更新,运行softupdate.exe
        /// </summary>
<<<<<<< .mine
        private void checkUpdate() {
            if (checkForUpdate() > 0) {
                EchoHelper.Echo("勤劳的忍者Qin，又更新了，爱他就更新吧？", "软件更新", EchoHelper.EchoType.任务信息);
=======
        private void checkUpdate() {
            if (checkForUpdate() > 0) {
                EchoHelper.Echo("勤劳的忍者Qin，又更新了，爱他就更新吧？", "软件更新", EchoHelper.EchoType.绿色信息);
>>>>>>> .r80
                string xmlFile = Application.StartupPath + @"\Temp\UpdateList.xml";
                string upStr = new XmlFiles(xmlFile).GetNodeValue("//description");
                if (upStr.Contains("\n")) {
                    upStr = upStr.Split('\n')[1].ToString();
                    upStr = upStr.Trim();
                }
                DialogResult dre = MessageBox.Show("更新吗？\n" + upStr, "更新程序", MessageBoxButtons.OKCancel);
                if (dre == DialogResult.OK) {
                    string exe_path = Application.StartupPath;
                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    process.StartInfo.FileName = "org.renzhe.plat.update.exe";
                    process.StartInfo.WorkingDirectory = exe_path;
                    process.StartInfo.CreateNoWindow = true;
                    process.Start();
                    Application.Exit();
                }
            } else {
                string currentVer = getCurrentVer();
                EchoHelper.Echo("恭喜您，您的版本已经是最新！当前版本：" + currentVer, "软件更新", EchoHelper.EchoType.绿色信息);

                if ("3.0.3.1" == currentVer) {
                    FilesHelper.DeleteFile(Application.StartupPath + "\\org.renzhe.plat.encrypt.dll");
                    FilesHelper.DeleteFile(Application.StartupPath + "\\org.renzhe.plat.gather.dll");
                    FilesHelper.DeleteFile(Application.StartupPath + "\\org.renzhe.plat.iplugin.dll");
                    FilesHelper.DeleteFile(Application.StartupPath + "\\org.renzhe.plat.model.dll");
                    FilesHelper.DeleteFile(Application.StartupPath + "\\org.renzhe.plat.reflection.dll");
                    FilesHelper.DeleteFile(Application.StartupPath + "\\org.renzhe.plat.rztask.dll");
                    FilesHelper.DeleteFile(Application.StartupPath + "\\org.renzhe.plat.service.dll");
                    FilesHelper.DeleteFile(Application.StartupPath + "\\org.renzhe.plat.usercontrol.dll");
                }
            }

            wait.CloseMsg();
        }

        private string getCurrentVer() {
            string oldVer = string.Empty;
            try {
                string localXmlFile = Application.StartupPath + "\\UpdateList.xml";
                XmlFiles localXmlFiles = new XmlFiles(localXmlFile);
                XmlNodeList oldNodeList = localXmlFiles.GetNodeList("AutoUpdater/Files");
                oldVer = oldNodeList.Item(0).Attributes["Ver"].Value.Trim();
            } catch (Exception ex) {
                EchoHelper.EchoException(ex);
            }
            return oldVer;
        }

        private int checkForUpdate() {
            string localXmlFile = Application.StartupPath + "\\UpdateList.xml";
            if (!File.Exists(localXmlFile)) {
                return -1;
            }
            XmlFiles updaterXmlFiles = new XmlFiles(localXmlFile);

            string tempUpdatePath = Application.StartupPath + "\\Temp\\";
            this.UpdaterUrl = updaterXmlFiles.GetNodeValue("//Url") + "UpdateList.xml";
            this.DownUpdateFile(tempUpdatePath);

            string serverXmlFile = tempUpdatePath + "UpdateList.xml";

            if (!File.Exists(serverXmlFile)) {
                return -1;
            }

            XmlFiles serverXmlFiles = new XmlFiles(serverXmlFile);
            XmlFiles localXmlFiles = new XmlFiles(localXmlFile);

            int k = 0;
            try {
                XmlNodeList newNodeList = serverXmlFiles.GetNodeList("AutoUpdater/Files");
                XmlNodeList oldNodeList = localXmlFiles.GetNodeList("AutoUpdater/Files");
                for (int i = 0; i < newNodeList.Count; i++) {
                    string[] fileList = new string[3];

                    string newFileName = newNodeList.Item(i).Attributes["Name"].Value.Trim();
                    string newVer = newNodeList.Item(i).Attributes["Ver"].Value.Trim();

                    ArrayList oldFileAl = new ArrayList();
                    for (int j = 0; j < oldNodeList.Count; j++) {
                        string oldFileName = oldNodeList.Item(j).Attributes["Name"].Value.Trim();
                        string oldVer = oldNodeList.Item(j).Attributes["Ver"].Value.Trim();
                        oldFileAl.Add(oldFileName);
                        oldFileAl.Add(oldVer);
                    }
                    int pos = oldFileAl.IndexOf(newFileName);
                    if (pos == -1) {
                        fileList[0] = newFileName;
                        fileList[1] = newVer;
                        k++;
                    } else if (pos > -1 && newVer.CompareTo(oldFileAl[pos + 1].ToString()) > 0) {
                        fileList[0] = newFileName;
                        fileList[1] = newVer;
                        k++;
                    }
                }
            } catch (Exception ex) {
                EchoHelper.Echo("请检查您的更新UpdateList文件，或删除Temp中的改文件，重新获取！", "文件出错", EchoHelper.EchoType.异常信息);
                EchoHelper.EchoException(ex);
            }
            return k;
        }

        /// <summary>
        /// 下载更新文件到临时目录
        /// </summary>
        /// <returns></returns>
        private void DownUpdateFile(string downpath) {
            if (!System.IO.Directory.Exists(downpath))
                System.IO.Directory.CreateDirectory(downpath);
            string serverXmlFile = downpath + @"UpdateList.xml";

            try {
                WebRequest req = WebRequest.Create(this.UpdaterUrl);
                req.Timeout = 999999;
                WebResponse res = req.GetResponse();
                if (res.ContentLength > 0) {
                    try {
                        WebClient wClient = new WebClient();
                        wClient.DownloadFile(this.UpdaterUrl, serverXmlFile);
                    } catch (Exception ex) {
                        EchoHelper.EchoException(ex);
                    }
                }
            } catch {
                return;
            }
        }

        #endregion

    }
}

        private void groupBox1_Enter(object sender, EventArgs e) {

        }