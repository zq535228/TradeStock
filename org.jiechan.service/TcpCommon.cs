using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Net.Sockets;

namespace org.renzhe.plat.service {

    internal class TcpCommon {
        private static readonly int _blockLength = 500 * 1024;

        /// <summary>
        /// �����ļ���hashֵ 
        /// </summary>
        internal string CalcFileHash(string FilePath) {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] hash;
            using (FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096)) {
                hash = md5.ComputeHash(fs);
            }
            return BitConverter.ToString(hash);
        }

        /// <summary>
        /// �����ļ�
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="stream"></param>
        /// <returns></returns>
        internal bool SendFile(string filePath, NetworkStream stream) {
            FileStream fs = File.Open(filePath, FileMode.Open);
            int readLength = 0;
            byte[] data = new byte[_blockLength];

            //���ʹ�С
            byte[] length = new byte[8];
            BitConverter.GetBytes(new FileInfo(filePath).Length).CopyTo(length, 0);
            stream.Write(length, 0, 8);

            //�����ļ�
            while ((readLength = fs.Read(data, 0, _blockLength)) > 0) {
                stream.Write(data, 0, readLength);
            }
            fs.Close();
            return true;
        }

        /// <summary>
        /// �����ļ�
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="stream"></param>
        /// <returns></returns>
        internal bool ReceiveFile(string filePath, NetworkStream stream) {
            try {
                long count = GetSize(stream);
                if (count == 0) {
                    return false;
                }

                long index = 0;
                byte[] clientData = new byte[_blockLength];
                if (File.Exists(filePath)) {
                    File.Delete(filePath);
                }
                string path = new FileInfo(filePath).Directory.FullName;
                if (!Directory.Exists(path)) {
                    Directory.CreateDirectory(path);
                }

                FileStream fs = File.Open(filePath, FileMode.OpenOrCreate);
                try {
                    //���㵱ǰҪ��ȡ�Ŀ�Ĵ�С
                    int currentBlockLength = 0;
                    if (_blockLength < count - index) {
                        currentBlockLength = _blockLength;
                    } else {
                        currentBlockLength = (int)(count - index);
                    }

                    int receivedBytesLen = stream.Read(clientData, 0, currentBlockLength);
                    index += receivedBytesLen;
                    fs.Write(clientData, 0, receivedBytesLen);

                    while (receivedBytesLen > 0 && index < count) {
                        clientData = new byte[_blockLength];
                        receivedBytesLen = 0;

                        if (_blockLength < count - index) {
                            currentBlockLength = _blockLength;
                        } else {
                            currentBlockLength = (int)(count - index);
                        }
                        receivedBytesLen = stream.Read(clientData, 0, currentBlockLength);
                        index += receivedBytesLen;
                        fs.Write(clientData, 0, receivedBytesLen);
                    }
                } catch (Exception ex) {
                    EchoHelper.EchoException(ex);
                    return false;
                } finally {
                    fs.Close();
                }
            } catch (Exception ex) {
                EchoHelper.EchoExceptionNOShow(ex);
                return false;
            }
            return true;
        }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        /// <param name="message"></param>
        /// <param name="stream"></param>
        /// <returns></returns>
        internal bool SendMessage(string message, NetworkStream stream) {

            try {
                byte[] data = Encoding.UTF8.GetBytes(message);
                byte[] resultData = new byte[8 + data.Length];
                BitConverter.GetBytes(data.Length).CopyTo(resultData, 0);
                data.CopyTo(resultData, 8);

                stream.Write(resultData, 0, resultData.Length);
                return true;
            } catch (Exception ex) {
                EchoHelper.EchoExceptionNOShow(ex);
                return false;
            }
        }

        /// <summary>
        /// ��ȡ��Ϣ
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        internal string ReadMessage(NetworkStream stream) {
            string result = "";
            int messageLength = 0;

            byte[] resultbyte = new byte[500 * 1024];
            //��ȡ���ݴ�С
            int index = 0;
            int count = GetSize(stream);

            byte[] data = new byte[count];
            while (index < count && (messageLength = stream.Read(data, 0, count - index)) != 0) {
                data.CopyTo(resultbyte, index);
                index += messageLength;
            }
            result = Encoding.UTF8.GetString(resultbyte, 0, index);
            return result;
        }

        /// <summary>
        /// ��ȡҪ��ȡ�����ݵĴ�С
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        private int GetSize(NetworkStream stream) {
            int count = 0;
            byte[] countBytes = new byte[8];
            try {
                if (stream.Read(countBytes, 0, 8) == 8) {
                    count = BitConverter.ToInt32(countBytes, 0);
                } else {
                    return 0;
                }
            } catch (Exception ex) {
                EchoHelper.EchoExceptionNOShow(ex);
            }
            return count;
        }
    }
}
