using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace org.renzhe.plat.service {
    public class TcpListenerHelper {
        private string _strServerIP = "";
        private int _serverPort = 0;

        TcpListener server;
        TcpClient client;
        NetworkStream netstream;
        IAsyncResult asyncResult;
        TcpCommon tcpCommon = new TcpCommon();

        ManualResetEvent listenConnected = new ManualResetEvent(false);

        bool _active = false;

        public TcpListenerHelper(string strServerIP, int serverPort) {
            try {
                _strServerIP = strServerIP;
                _serverPort = serverPort;
                server = new TcpListener(IPAddress.Parse(strServerIP), serverPort);
                server.Server.ReceiveTimeout = 1000;
                server.Server.SendTimeout = 1000;

            } catch (Exception ex) {
                EchoHelper.EchoException(ex);
            }
        }

        /// <summary>
        /// 启动
        /// </summary>
        public bool Start() {
            bool re = false;
            try {
                _active = true;
                server.Start();
                re = true;
            } catch (Exception ex) {
                EchoHelper.Echo("请确保您的IP地址正确无误，可以在运行键入：ipconfig查看本机IP。", "服务端启动", EchoHelper.EchoType.错误信息);
                EchoHelper.EchoExceptionNOShow(ex);
            }
            return re;
        }

        /// <summary>
        /// 停止
        /// </summary>
        public void Stop() {
            try {
                _active = false;
                if (client != null) {
                    client.Close();
                }

                if (netstream != null) {
                    netstream.Close();
                }
                server.Stop();

            } catch (Exception ex) {
                EchoHelper.EchoException(ex);
            }
        }

        public void Listen() {
            try {
                listenConnected.Reset();
                asyncResult = server.BeginAcceptTcpClient(new AsyncCallback(AsyncCall), server);
            } catch (Exception ex) {
                EchoHelper.Echo("监听失败，请确保服务器IP正确，检查端口5188是否被防火墙拦截！", "服务端启动", EchoHelper.EchoType.错误信息);

                EchoHelper.EchoExceptionNOShow(ex);
            }
        }

        public void AsyncCall(IAsyncResult ar) {
            try {
                TcpListener tlistener = (TcpListener)ar.AsyncState;

                if (_active) {
                    client = tlistener.EndAcceptTcpClient(ar);
                    netstream = client.GetStream();
                } else {
                    client = null;
                    netstream = null;
                }
                listenConnected.Set();
            } catch (Exception ex) {
                EchoHelper.EchoException(ex);
            }
        }
        public bool WaitForConnect() {
            listenConnected.WaitOne();

            if (client != null && netstream != null) {
                return true;
            } else {
                return false;
            }
        }


        #region TcpCommon所有方法
        /// <summary>
        /// 计算文件的hash值 
        /// </summary>
        public string CalcFileHash(string FilePath) {
            return tcpCommon.CalcFileHash(FilePath);
        }

        /// <summary>
        /// 发送文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool SendFile(string filePath) {
            return tcpCommon.SendFile(filePath, netstream);
        }

        /// <summary>
        /// 接收文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool ReceiveFile(string filePath) {
            return tcpCommon.ReceiveFile(filePath, netstream);
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool SendMessage(string message) {
            return tcpCommon.SendMessage(message, netstream);
        }

        /// <summary>
        /// 接收消息
        /// </summary>
        /// <returns></returns>
        public string ReadMessage() {
            return tcpCommon.ReadMessage(netstream);
        }
        #endregion

        #region IDisposable 成员

        public void Dispose() {
            Stop();
        }

        #endregion
    }
}
