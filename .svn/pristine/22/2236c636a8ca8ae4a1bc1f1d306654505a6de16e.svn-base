using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using STSdb4.Database;
using org.jiechan.service;

namespace org.jiechan.main.models {

    public class holdingstockopt {

        public static holdingstockopt GetInstance() {
            // 定义一个标识确保线程同步

            if (db == null) {
                lock (locker) {
                    if (db == null) {
                        db = new holdingstockopt();
                    }

                }
            }
            return db;
        }

        private static readonly object locker = new object();

        private static holdingstockopt db;

        private static IStorageEngine engine;

        /// <summary>
        /// 默认使用localtask.db数据库,进行实例化.
        /// </summary>
        /// <param name="IsLocalDb">是否使用本地任务数据库:localtask.VDB,如果否:就相当于直接实例化一个空的new DbTools();</param>
        private holdingstockopt() {
            try {
                bool exist = FilesHelper.DirectoryExist(PathHelper.ConfigPath);
                if (exist == false) {
                    FilesHelper.CreateDirectory(PathHelper.ConfigPath);
                }

                engine = STSdb.FromFile(PathHelper.ConfigPath + "\\mystockmodel.VDB");

            } catch (Exception ex) {
                EchoHelper.EchoException(ex);
            }

        }



        /// <summary>
        /// 获取当前db文件内的mystockmodel的集合。这里主要考虑的是本地插件任务列表。例如:localtask.db
        /// </summary>
        /// <returns></returns>
        public List<holdingstockmodel> GetStockList() {
            List<holdingstockmodel> mlist = new List<holdingstockmodel>();
            try {
                ITable<int, holdingstockmodel> t = engine.OpenXTable<int, holdingstockmodel>("mystockmodel");

                foreach (var item in t) {
                    mlist.Add(item.Value);
                }

            } catch {

            }
            return mlist;
        }


        /// <summary>
        /// 获取当前db文件内的mystockmodel的Count.
        /// </summary>
        /// <returns></returns>
        public int GetStockCount() {
            int re = 0;
            try {
                ITable<int, holdingstockmodel> t = engine.OpenXTable<int, holdingstockmodel>("mystockmodel");
                re = Convert.ToInt32(t.Count());
            } catch {
            }
            return re;
        }


        /// <summary>
        /// 添加任务到忍者任务列表中
        /// </summary>
        /// <param name="stock"></param>
        public void SaveStock(holdingstockmodel stock) {
            if (stock == null || stock.stock == null || string.IsNullOrEmpty(stock.stock.name)) {
                EchoHelper.Echo("股票信息不完整，请完善后保存！", EchoHelper.EchoType.异常信息);
                return;
            }
            try {
                var t = engine.OpenXTable<int, holdingstockmodel>("mystockmodel");

                stock.id = nextStockID(stock.id);
                t[stock.id] = stock;
                engine.Commit();

            } catch (Exception ex) {
                EchoHelper.EchoException(ex);
            }
        }

        /// <summary>
        /// 删除主键是TaskID的任务.针对的是LocalTask
        /// </summary>
        /// <param name="TaskID"></param>
        public void DelStock(int TaskID) {
            try {
                var t = engine.OpenXTable<int, holdingstockmodel>("mystockmodel");
                t.Delete(TaskID);
                engine.Commit();

            } catch (Exception ex) {
                EchoHelper.EchoException(ex);
            }
        }


        /// <summary>
        /// 获取本地的单一任务
        /// </summary>
        /// <param name="task"></param>
        public holdingstockmodel GetStock(int taskID) {
            holdingstockmodel mt = new holdingstockmodel();
            try {
                var t = engine.OpenXTable<int, holdingstockmodel>("mystockmodel");
                mt = t[taskID];
            } catch {


            }
            return mt;
        }

        /// <summary>
        /// 获取本地的单一任务
        /// </summary>
        /// <param name="task"></param>
        public bool ExistStock(string stockname) {
            List<holdingstockmodel> lt = GetStockList();
            return lt.Exists(x => x.stock.name == stockname);
        }



        /// <summary>
        /// 获取NEXT的任务ID,这个是中间的方法,不能轻易的关闭engine,否则接下来的方法无法使用engine
        /// </summary>
        /// <param name="stockID"></param>
        /// <returns></returns>
        private int nextStockID(int stockID) {
            int re = 0;
            if (0 != stockID) {
                re = stockID;
            } else {
                try {
                    ITable<int, holdingstockmodel> t = engine.OpenXTable<int, holdingstockmodel>("mystockmodel");
                    re = t.LastRow.Key + 1;
                } catch (Exception) {
                    re = 1;
                }
            }
            return re;

        }

    }

}
