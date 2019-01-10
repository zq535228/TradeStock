using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.jiechan.service;
using System.Threading;
using Amib.Threading;
using org.jiechan.main.models;

namespace org.jiechan.main.core {
    public class taskmanage {

        private static readonly object locker = new object();

        private static List<taskmodel> tm = new List<taskmodel>();

        private static taskmanage tkmg;

        private bool runorstop = true;
        
        private taskmanage() {
        }

        public static taskmanage GetInstance() {

            if (tkmg == null) {
                lock (locker) {
                    if (tkmg == null) {
                        tkmg = new taskmanage();
                    }

                }
            }
            return tkmg;
        }

        public void run() {
            // 创建一个线程池
            new SmartThreadPool().QueueWorkItem(() => {


                while (runorstop) {
                    
                    for (int i = tm.Count-1; i > -1; i--) {
                        EchoHelper.Echo(tm[i].ToString());
                        tm.Remove(tm[i]);
                    }

                    Thread.Sleep(1000);
                }
            });

        }

        public void stop() {
            runorstop = false;
        }

        public void putlist() {

            for (int i = 0; i < 30; i++) {
                //tm.Insert(0,i.ToString());
            }

        }


        public void add() {
            //tm.Add("wo shi xin lai d ");
        }

        public void insert() {
            //tm.Insert(0, "aaa");
        }

    }
}
