// ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadStartMethod2));
// 
// public void ThreadStartMethod2(object arg)
//         {
//             int workcount = Convert.ToInt32(numericUpDown2.Value);//定义总数
//             ///  _count = workcount * 100;
// 
//             ThreadMulti thread = new ThreadMulti(workcount , workcount);
// 
//             thread.WorkMethod = new ThreadMulti.DelegateWork(DoWork2);
//             thread.CompleteEvent = new ThreadMulti.DelegateComplete(WorkComplete2);
//             thread.Start();
//         }




using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace org.jiechan.service {
    public class ThreadMulti {

        #region 变量

        public delegate void DelegateComplete();
        public delegate void DelegateWork(int taskindex , int threadindex);

        public DelegateComplete CompleteEvent;
        public DelegateWork WorkMethod;

        private Thread[] _threads;
        private bool[] _threadState;
        private int _taskCount = 0;
        private int _taskindex = 0;
        private int _threadCount = 20;//定义线程   

        #endregion

        public ThreadMulti(int taskcount) {
            _taskCount = taskcount;
        }

        public ThreadMulti(int taskcount , int threadCount) {
            _taskCount = taskcount;
            _threadCount = threadCount;
        }

        #region 获取任务 参考了老羽 http://www.cnblogs.com/michael-zhangyu/archive/2009/07/16/1524737.html 的博客
        private int GetTask() {
            lock(this) {
                if(_taskindex < _taskCount) {
                    _taskindex++;
                    return _taskindex;
                } else {
                    return 0;
                }
            }
        }
        #endregion

        #region Start

        /// <summary>
        /// 启动
        /// </summary>
        public void Start() {
            //采用 Hawker(http://www.cnblogs.com/tietaren/)的建议,精简了很多
            _taskindex = 0;
            int num = _taskCount < _threadCount ? _taskCount : _threadCount;
            _threadState = new bool[num];
            _threads = new Thread[num];

            for(int n = 0; n < num; n++) {
                _threadState[n] = false;
                _threads[n] = new Thread(new ParameterizedThreadStart(Work));
                _threads[n].Start(n);
            }
        }

        /// <summary>
        /// 结束线程
        /// </summary>
        /// 
        public void Resume() {
            for(int i = 0; i < _threads.Length; i++) {
                _threads[i].Resume();
            }
        }
        public void Stop() {
            for(int i = 0; i < _threads.Length; i++) {
                _threads[i].Abort();
            }

            //string s = "";
            //for (int j = 0; j < _threads.Length; j++)
            //{
            //    s += _threads[j].ThreadState.ToString() + "\r\n";
            //}
            //MessageBox.Show(s);
        }
        public void Suspend() {
            for(int i = 0; i < _threads.Length; i++) {
                _threads[i].Suspend();
            }

            //string s = "";
            //for (int j = 0; j < _threads.Length; j++)
            //{
            //    s += _threads[j].ThreadState.ToString() + "\r\n";
            //}
            //MessageBox.Show(s);
        }
        #endregion

        #region Work

        public void Work(object arg) {
            //提取任务并执行
            int threadindex = int.Parse(arg.ToString());
            int taskindex = GetTask();

            while(taskindex != 0 && WorkMethod != null) {
                WorkMethod(taskindex , threadindex + 1);
                taskindex = GetTask();
            }
            //所有的任务执行完毕
            _threadState[threadindex] = true;

            //处理并发 如果有两个线程同时完成只允许一个触发complete事件
            lock(this) {
                for(int i = 0; i < _threadState.Length; i++) {
                    if(_threadState[i] == false) {
                        return;
                    }
                }
                //如果全部完成
                if(CompleteEvent != null) {
                    CompleteEvent();
                }

                //触发complete事件后 重置线程状态
                //为了下个同时完成的线程不能通过上面的判断
                for(int j = 0; j < _threadState.Length; j++) {
                    _threadState[j] = false;
                }
            }

        }

        #endregion
    }
}
