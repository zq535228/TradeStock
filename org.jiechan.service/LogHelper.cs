﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace org.jiechan.service {
    public class LogHelper {




        public static LogHelper GetInstance() {
            // 定义一个标识确保线程同步

            if (log == null) {
                lock (locker) {
                    if (log == null) {
                        log = new LogHelper();
                    }

                }
            }
            return log;
        }

        private static readonly object locker = new object();

        private static LogHelper log;

        private LogHelper() {

        }


        ///   <summary>   
        ///   生成系统日志。   
        ///   </summary>   
        ///   <param   name="Description">所记录日志描述。</param>   
        ///   <param   name="Content">所记录日志内容。</param>   
        public void CreateLog(string Description, string Content, EchoHelper.EchoType etype) {

            string FilePath = Application.StartupPath + "\\Log\\";

            if (!Directory.Exists(FilePath)) {
                Directory.CreateDirectory(FilePath);
            }

            string path = Application.StartupPath + "\\Config\\Setup.ini";

            INIHelper ini = new INIHelper(path);

            bool NormalInfo = true;
            bool TaskInfo = true;
            bool FailInfo = true;

            try {
                NormalInfo = Convert.ToBoolean(ini.re("日志设定", "普通信息"));
                TaskInfo = Convert.ToBoolean(ini.re("日志设定", "任务信息"));
                FailInfo = Convert.ToBoolean(ini.re("日志设定", "错误信息"));
            } catch {
            }

            switch (etype) {
                case EchoHelper.EchoType.淡蓝信息:
                    if (!NormalInfo)
                        return;
                    FilePath += "普通信息_log_";
                    break;
                case EchoHelper.EchoType.绿色信息:
                    if (!TaskInfo)
                        return;
                    FilePath += "任务信息_log_";
                    break;
                case EchoHelper.EchoType.红色信息:
                    if (!FailInfo)
                        return;
                    FilePath += "错误信息_log_";
                    break;
                default:
                    FilePath += "异常信息_log_";
                    break;
            }

            FilePath += TimeHelper.DateString() + ".txt";
            //天使更新


            try {
                StreamWriter sw;
                if (!File.Exists(FilePath)) {
                    sw = File.CreateText(FilePath);
                } else {
                    sw = File.AppendText(FilePath);
                }

                sw.WriteLine("-------------------" + "[System Time]:   " + DateTime.Now.ToString() + "---------------------------");
                sw.WriteLine(Description.ToString() + "：" + Content.ToString());
                sw.WriteLine();

                sw.Close();

            } catch {

            }

        }
    }
}
