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
        /// ����
        /// </summary>
        public bool Start() {
            bool re = false;
            try {
                _active = true;
                server.Start();
                re = true;
            } catch (Exception ex) {
                EchoHelper.Echo("��ȷ������IP��ַ��ȷ���󣬿��������м��룺ipconfig�鿴����IP��", "���������", EchoHelper.EchoType.������Ϣ);
                EchoHelper.EchoExceptionNOShow(ex);
            }
            return re;
        }

        /// <summary>
        /// ֹͣ
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
                EchoHelper.Echo("����ʧ�ܣ���ȷ��������IP��ȷ�����˿�5188�Ƿ񱻷���ǽ���أ�", "���������", EchoHelper.EchoType.������Ϣ);

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


        #region TcpCommon���з���
        /// <summary>
        /// �����ļ���hashֵ 
        /// </summary>
        public string CalcFileHash(string FilePath) {
            return tcpCommon.CalcFileHash(FilePath);
        }

        /// <summary>
        /// �����ļ�
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool SendFile(string filePath) {
            return tcpCommon.SendFile(filePath, netstream);
        }

        /// <summary>
        /// �����ļ�
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool ReceiveFile(string filePath) {
            return tcpCommon.ReceiveFile(filePath, netstream);
        }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool SendMessage(string message) {
            return tcpCommon.SendMessage(message, netstream);
        }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        /// <returns></returns>
        public string ReadMessage() {
            return tcpCommon.ReadMessage(netstream);
        }
        #endregion

        #region IDisposable ��Ա

        public void Dispose() {
            Stop();
        }

        #endregion
    }
}
