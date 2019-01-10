using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using org.jiechan.service;
using org.jiechan.service.SplashScreen;

namespace org.jiechan.service {

    public partial class X_Waiting {

        public X_Waiting() {
            Splasher.Show(typeof(X_Splash));
        }

        public void ShowMsg(string msg) {
            try {
                Thread.Sleep(2);
                Splasher.Show();
                Splasher.Status = msg;
            } catch {
                //EchoHelper.EchoException(ex);
            } finally {
                Thread.Sleep(10);
            }
        }

        private void showWaitDlg(object msg) {
            Splasher.Show();
            Splasher.Status = msg.ToString();
        }

        public void CloseMsg() {
            Splasher.Close(false);
        }

        public void CloseMsg(int sec) {

            Thread.Sleep(sec * 1000);
            Splasher.Close(false);
        }


        public void Dispose() {
            Splasher.Close();
        }





    }
}
