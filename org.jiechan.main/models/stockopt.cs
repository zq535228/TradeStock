using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using STSdb4.Database;
using org.jiechan.service;

namespace org.jiechan.main.models {

    public class stockopt {

        public static stockopt GetInstance() {
            // 定义一个标识确保线程同步

            if (db == null) {
                lock (locker) {
                    if (db == null) {
                        db = new stockopt();
                    }

                }
            }
            return db;
        }

        private static readonly object locker = new object();

        private static stockopt db;

        private static IStorageEngine engine;

        /// <summary>
        /// 默认使用localtask.db数据库,进行实例化.
        /// </summary>
        /// <param name="IsLocalDb">是否使用本地任务数据库:localtask.VDB,如果否:就相当于直接实例化一个空的new DbTools();</param>
        private stockopt() {
            try {
                bool exist = FilesHelper.DirectoryExist(PathHelper.ConfigPath);
                if (exist == false) {
                    FilesHelper.CreateDirectory(PathHelper.ConfigPath);
                }

                engine = STSdb.FromFile(PathHelper.ConfigPath + "\\stockmodel.VDB");

            } catch (Exception ex) {
                EchoHelper.EchoException(ex);
            }

        }



        /// <summary>
        /// 获取当前db文件内的stockmodel的集合。这里主要考虑的是本地插件任务列表。例如:localtask.db
        /// </summary>
        /// <returns></returns>
        public List<stockmodel> GetStockList() {
            List<stockmodel> mlist = new List<stockmodel>();
            try {
                ITable<string, stockmodel> t = engine.OpenXTable<string, stockmodel>("stockmodel");

                foreach (var item in t) {
                    mlist.Add(item.Value);
                }

            } catch {

            }
            return mlist;
        }


        /// <summary>
        /// 获取当前db文件内的stockmodel的Count.
        /// </summary>
        /// <returns></returns>
        public int GetStockCount() {
            int re = 0;
            try {
                ITable<string, stockmodel> t = engine.OpenXTable<string, stockmodel>("stockmodel");
                re = Convert.ToInt32(t.Count());
            } catch {
            }
            return re;
        }


        /// <summary>
        /// 添加任务到忍者任务列表中
        /// </summary>
        /// <param name="stock"></param>
        public void SaveStock(stockmodel stock) {
            try {
                var t = engine.OpenXTable<string, stockmodel>("stockmodel");
                t[stock.codenum] = stock;
                engine.Commit();

            } catch (Exception ex) {
                EchoHelper.EchoException(ex);
            }
        }

        /// <summary>
        /// 删除主键是TaskID的任务.针对的是LocalTask
        /// </summary>
        /// <param name="codenum"></param>
        public void DelStock(string codenum) {
            try {
                var t = engine.OpenXTable<string, stockmodel>("stockmodel");
                t.Delete(codenum);
                engine.Commit();

            } catch (Exception ex) {
                EchoHelper.EchoException(ex);
            }
        }


        /// <summary>
        /// 获取本地的单一任务
        /// </summary>
        /// <param name="task"></param>
        public stockmodel GetStock(string codenum) {
            stockmodel mt = new stockmodel();
            try {
                var t = engine.OpenXTable<string, stockmodel>("stockmodel");
                mt = t[codenum];
            } catch {


            }
            return mt;
        }


        public bool exist(string codenum) {
            var t = engine.OpenXTable<string, stockmodel>("stockmodel");
            return t.Exists(codenum);

        }
    }

}
