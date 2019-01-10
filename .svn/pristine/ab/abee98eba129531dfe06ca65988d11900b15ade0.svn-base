using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace org.jiechan.service {
    public class Webb : WebBrowser {

        public delegate void DelUserHandler(string url);

        public void NavigateUrl(string url) {
            if(this.InvokeRequired) {
                DelUserHandler handler = new DelUserHandler(NavigateUrl);
                this.Invoke(handler , url);
            } else {
                this.Navigate(url);

            }
        }



    }
}
