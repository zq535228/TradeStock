using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace org.renzhe.plat.service {
    public class TcpHelper {


        public bool TestConnection(string IP) {
            int retryTime = 0;
            return TestConnection(IP, ref retryTime);
        }

        /// <summary>
        /// 测试远程服务器接受tcp信息是否成功？连续测试2次，失败返回false
        /// </summary>
        /// <param name="IP"></param>
        /// <returns></returns>
        private bool TestConnection(string IP, ref int retryTime) {
            string re = "";

            if (!IPHelper.IsPingIP(IP)) {
                return false;
            }

            try {
                TcpClientHelper client = new TcpClientHelper(IP, 5188);
                client.Start();
                client.SendMessage("测试连接");
                re = client.ReadMessage();
                client.Stop();
            } catch (Exception ex) {
                EchoHelper.EchoExceptionNOShow(ex);
            }

            if (re == "连接成功") {
                return true;
            } else {
                if (retryTime < 1) {
                    retryTime++;
                    return TestConnection(IP, ref retryTime);
                } else {
                    return false;
                }
            }

        }


        private void test() {
            for (int i = 100; i < 120; i++) {
                string ip = "192.168.1." + i;
                Console.WriteLine(TestConnection(ip));
            }
        }
    }
}
