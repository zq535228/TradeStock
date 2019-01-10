using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Amib.Threading;

namespace org.jiechan.service {
    public class STP {

        private static readonly object locker = new object();
        private static SmartThreadPool stpool;
        private static STP _stp;

        public static STP GetInstence() {
            if (_stp == null) {
                lock (locker) {
                    if (_stp == null) {
                        _stp = new STP();
                    }

                }
            }
            return _stp;
        }

        private STP() {
            stpool = new SmartThreadPool();
        }

        public string GetThCountStr {
            get {
                return "线程状态：" + stpool.InUseThreads + "/" + stpool.MaxThreads;
            }
        }

        public IWorkItemResult QueueWorkItem(Amib.Threading.Action action) {
            IWorkItemResult wir = stpool.QueueWorkItem(action);
//             Exception e;
//             //wir.GetResult(out e);
// 
//             if (null != e) {
//                 //EchoHelper.EchoException(e);
//             }
// 
            return wir;
        }

        public IWorkItemResult<TResult> QueueWorkItem<TResult>(Amib.Threading.Func<TResult> func) {
            return stpool.QueueWorkItem<TResult>(func);
        }


    }

    public class MyClass {
        public void test() {
            STP.GetInstence().QueueWorkItem(() => {
                t();
            });
        }

        private void t() {
            throw new NotImplementedException();
        }



    }
}
