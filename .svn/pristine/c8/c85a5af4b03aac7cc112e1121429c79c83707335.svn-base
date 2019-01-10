using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using STSdb4.Database;
using org.jiechan.service;

namespace org.jiechan.main.models {

    public class taskopt {

        public static taskopt GetInstance() {
            // 定义一个标识确保线程同步

            if (db == null) {
                lock (locker) {
                    if (db == null) {
                        db = new taskopt();
                    }

                }
            }
            return db;
        }

        private static readonly object locker = new object();

        private static taskopt db;

        private static IStorageEngine engine;

        /// <summary>
        /// 默认使用localtask.db数据库,进行实例化.
        /// </summary>
        /// <param name="IsLocalDb">是否使用本地任务数据库:localtask.VDB,如果否:就相当于直接实例化一个空的new DbTools();</param>
        private taskopt() {
            try {
                bool exist = FilesHelper.DirectoryExist(PathHelper.ConfigPath);
                if (exist == false) {
                    FilesHelper.CreateDirectory(PathHelper.ConfigPath);
                }

                engine = STSdb.FromFile(PathHelper.ConfigPath + "\\taskmodel.VDB");

            } catch (Exception ex) {
                EchoHelper.EchoException(ex);
            }

        }



        /// <summary>
        /// 获取当前db文件内的taskmodel的集合。这里主要考虑的是本地插件任务列表。例如:localtask.db
        /// </summary>
        /// <returns></returns>
        public List<taskmodel> GetTaskList() {
            List<taskmodel> mlist = new List<taskmodel>();
            try {
                ITable<int, taskmodel> t = engine.OpenXTable<int, taskmodel>("taskmodel");

                foreach (var item in t) {
                    mlist.Add(item.Value);
                }

            } catch {

            }
            return mlist;
        }


        /// <summary>
        /// 获取当前db文件内的taskmodel的Count.
        /// </summary>
        /// <returns></returns>
        public int GetTaskCount() {
            int re = 0;
            try {
                ITable<int, taskmodel> t = engine.OpenXTable<int, taskmodel>("taskmodel");
                re = Convert.ToInt32(t.Count());
            } catch {
            }
            return re;
        }


        /// <summary>
        /// 添加任务到忍者任务列表中
        /// </summary>
        /// <param name="task"></param>
        public void SaveTask(taskmodel task) {
            try {
                var t = engine.OpenXTable<int, taskmodel>("taskmodel");
                task.id = nextTaskID(task.id);
                t[task.id] = task;
                engine.Commit();

            } catch (Exception ex) {
                EchoHelper.EchoException(ex);
            }
        }

        /// <summary>
        /// 删除主键是TaskID的任务.针对的是LocalTask
        /// </summary>
        /// <param name="taskID"></param>
        public void DelTask(int taskID) {
            try {
                var t = engine.OpenXTable<int, taskmodel>("taskmodel");
                t.Delete(taskID);
                engine.Commit();

            } catch (Exception ex) {
                EchoHelper.EchoException(ex);
            }
        }


        /// <summary>
        /// 获取本地的单一任务
        /// </summary>
        /// <param name="task"></param>
        public taskmodel GetTask(int taskID) {
            taskmodel mt = new taskmodel();
            try {
                var t = engine.OpenXTable<int, taskmodel>("taskmodel");
                mt = t[taskID];
            } catch {


            }
            return mt;
        }

        /// <summary>
        /// 获取NEXT的任务ID,这个是中间的方法,不能轻易的关闭engine,否则接下来的方法无法使用engine
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns></returns>
        private int nextTaskID(int taskID) {
            int re = 0;
            if (0 != taskID) {
                re = taskID;
            } else {
                try {
                    ITable<int, taskmodel> t = engine.OpenXTable<int, taskmodel>("taskmodel");
                    re = t.LastRow.Key + 1;
                } catch (Exception) {
                    re = 1;
                }
            }
            return re;

        }

    }

}
