using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;

namespace org.renzhe.plat.service {

    public class TcpClientHelper : IDisposable {
        TcpClient client;
        NetworkStream netstream;
        string _serverip = "127.0.0.1";
        int _port = 8080;

        TcpCommon tcpCommon = new TcpCommon();

        #region TcpClientHelper constructor
        public TcpClientHelper(string strServerIP, int serverPort) {
            _serverip = strServerIP;
            _port = serverPort;

        }
        #endregion

        public void Start() {
            try {
                client = new TcpClient(_serverip, _port);
                netstream = client.GetStream();

            } catch (Exception ex) {
                //EchoHelper.Echo(ex.Message, "同步服务器", EchoHelper.EchoType.错误信息);
                EchoHelper.EchoExceptionNOShow(ex);
            }
        }

        public void Stop() {
            if (netstream != null) {
                netstream.Close();
            }

            if (client != null) {
                client.Close();
            }
        }

        #region TcpCommon所有方法
        public string CalcFileHash(string FilePath) {
            return tcpCommon.CalcFileHash(FilePath);
        }

        public bool SendFile(string filePath) {
            return tcpCommon.SendFile(filePath, netstream);
        }


        public bool ReceiveFile(string filePath) {
            return tcpCommon.ReceiveFile(filePath, netstream);
        }


        public bool SendMessage(string message) {
            return tcpCommon.SendMessage(message, netstream);
        }

        public string ReadMessage() {
            return tcpCommon.ReadMessage(netstream);
        }
        #endregion

        #region IDisposable 成员

        public void Dispose() {
            if (netstream != null) {
                netstream.Close();
            }

            if (client != null) {
                client.Close();
            }
        }

        #endregion
    }
}
