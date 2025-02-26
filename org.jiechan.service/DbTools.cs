﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using STSdb4.Database;
using System.Collections;
using ProtoBuf;
//using ProtoBuf;

///两者区分开就是为了,避免实例化互相影响.例如要操作dbtool中的方法,而实例化db数据库占用localtask.vdb文件产生干扰.
///使用两个类
///分别是x3的
///x4的数据库操作类
///偶尔也会使用x3的.
namespace org.jiechan.service {

    /// <summary>
    /// x3中的就数据库工具类.
    /// </summary>
    public class DbTools {

//         #region 封装的常用数据序列化
//         public void SavePluginModel(string filePath, object obj) {
// 
//             if (null == obj) {
//                 return;
//             }
//             try {
//                 if (filePath.ToLower().EndsWith(".vdb")) {
//                     string _buff = FilesHelper.ReadFile(filePath + "_cache", Encoding.UTF8);
//                     string _vdb = FilesHelper.ReadFile(filePath, Encoding.UTF8);
//                     if (File.Exists(filePath) && _buff != _vdb) {
//                         File.Copy(filePath, filePath + "_buff", true);
//                     }
//                 }
//             } catch {
//             }
// 
//             FilesHelper.Write_File(filePath, ClasstoString(obj));
//         }
// 
// 
//         /// <summary>
//         /// 保存当前db文件内的ModelWinServer
//         /// </summary>
//         /// <param name="mws"></param>
//         public bool SaveModelWinServer(ModelWinServer mws) {
//             bool re = false;
//             try {
//                 Save(PathHelper.ConfigPath + "\\" + MD5Helper.MD5(mws.IP) + ".db", mws);
//                 re = true;
//             } catch (Exception ex) {
//                 re = false;
//             }
//             return re;
//         }
// 
//         /// <summary>
//         /// 输入服务器的IP，链接的序列化信息，进行保存。
//         /// </summary>
//         /// <param name="ip"></param>
//         /// <param name="mwsStr"></param>
//         /// <returns></returns>
//         public bool SaveModelWinServer(string ip, string mwsStr) {
//             bool re = false;
//             try {
// 
//                 FilesHelper.Write_File(PathHelper.ConfigPath + "\\" + MD5Helper.MD5(ip) + ".db", mwsStr);
//                 re = true;
//             } catch (System.Exception ex) {
//                 re = false;
//             }
//             return re;
//         }
// 
//         /// <summary>
//         /// 获取当前db文件内的ModelWinServer
//         /// </summary>
//         /// <returns></returns>
//         public ModelWinServer GetModelWinServer(string dbpath) {
//             ModelWinServer w = new ModelWinServer();
//             try {
//                 w = (ModelWinServer)(new DbTools().Read(dbpath));
//             } catch {
//             }
//             return w;
//         }
// 
// 
// 
//         #endregion

        #region 保存数据

        /// <summary>
        /// 以序列化的方式保存文件，obj序列化保存到filePath
        /// </summary>
        /// <param name="filePath">要保存的路径</param>
        /// <param name="obj">对象，一般是Model，或者ModelList等形式</param>
        public void Save(string filePath, object obj) {
            if (null == obj) {
                return;
            }
            try {
                if (filePath.ToLower().EndsWith(".vdb")) {
                    string _buff = FilesHelper.ReadFile(filePath + "_cache", Encoding.UTF8);
                    string _vdb = FilesHelper.ReadFile(filePath, Encoding.UTF8);
                    if (File.Exists(filePath) && _buff != _vdb) {
                        File.Copy(filePath, filePath + "_buff", true);
                    }
                }
            } catch {
            }

            FilesHelper.Write_File(filePath, ClasstoString(obj));
        }

        /// <summary>
        /// 从路径中读取 序列化的字符串，转换为OBJ返回。
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public object Read(string filePath) {
            if (!string.IsNullOrEmpty(filePath) && !File.Exists(filePath)) {
                EchoHelper.Echo("系统未发现数据库密钥文件，自动为您生成...", "", EchoHelper.EchoType.淡蓝信息);
                FilesHelper.Write_File(filePath, "");
            }
            try {
                object obj = StringtoClass(FilesHelper.ReadFile(filePath, Encoding.UTF8));
                return obj;
            } catch {
                return null;
            }
        }

        public object StringtoClass(string str) {
            return StringtoClass(str, "VCDS");
        }

        public object StringtoClass(string str, string key) {
            object result = null;
            byte[] dBytes = Convert.FromBase64String(str);
            dBytes = Decryption(dBytes, key);
            using (MemoryStream ms = new MemoryStream(dBytes)) {
                IFormatter formatter = new BinaryFormatter();
                result = formatter.Deserialize(ms);
            }
            return result;
        }

        public string ClasstoString(object obj) {
            return ClasstoString(obj, "VCDS");
        }

        public string ClasstoString(object obj, string key) {
            byte[] dBytes = null;
            using (MemoryStream ms = new MemoryStream()) {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                dBytes = ms.GetBuffer();
            }
            string Base64Str;
            dBytes = Encryption(dBytes, key);
            Base64Str = Convert.ToBase64String(dBytes);
            return Base64Str;
        }

        public byte[] ClasstoBuf<T>(T instance) {
            byte[] buf = null;

            using (MemoryStream ms = new MemoryStream()) {
                Serializer.Serialize<T>(ms, instance);
                buf = ms.ToArray();
            }
            return buf;

        }

        public T BuftoClass<T>(byte[] buf) {
            T re;
            using (MemoryStream ms = new MemoryStream(buf)) {
                re = Serializer.Deserialize<T>(ms);
            }
            return re;
        }


        #endregion

        #region 加密数据
        public byte[] Encryption(byte[] data, string key) {
            byte[] by = new byte[0];
            if (data.Length > 0) {
                try {
                    DESCryptoServiceProvider descsp = new DESCryptoServiceProvider();   //实例化加解密类对象
                    byte[] KEY = Encoding.Unicode.GetBytes(key);                        //定义字节数组，用来存储密钥
                    MemoryStream MStream = new MemoryStream();                          //实例化内存流对象
                    //使用内存流实例化加密流对象 
                    if (!string.IsNullOrEmpty(key)) {
                        CryptoStream CStream = new CryptoStream(MStream, descsp.CreateEncryptor(KEY, KEY), CryptoStreamMode.Write);
                        CStream.Write(data, 0, data.Length);                                //向加密流中写入数据
                        CStream.FlushFinalBlock();                                          //释放加密流
                    } else {
                        MStream.Write(data, 0, data.Length);
                        MStream.Flush();
                    }
                    by = MStream.ToArray();                                                        //返回加密后的数组
                } catch (Exception ex) {
                    EchoHelper.EchoException(ex);
                }
            }
            return by;
        }
        #endregion

        #region 解密数据
        public byte[] Decryption(byte[] data, string key) {
            byte[] by = new byte[0];
            if (data.Length > 0) {
                try {
                    DESCryptoServiceProvider descsp = new DESCryptoServiceProvider();   //实例化加解密类对象
                    byte[] KEY = Encoding.Unicode.GetBytes(key);                        //定义字节数组，用来存储密钥
                    MemoryStream MStream = new MemoryStream();                          //实例化内存流对象

                    if (!string.IsNullOrEmpty(key)) {
                        //使用内存流实例化加密流对象 
                        CryptoStream CStream = new CryptoStream(MStream, descsp.CreateDecryptor(KEY, KEY), CryptoStreamMode.Write);
                        CStream.Write(data, 0, data.Length);                                //向加密流中写入数据
                        CStream.FlushFinalBlock();
                    } else {
                        MStream.Write(data, 0, data.Length);
                        MStream.Flush();
                    }                                    //释放加密流
                    by = MStream.ToArray();                                           //返回加密后的数组
                } catch (Exception ex) {
                    EchoHelper.EchoException(ex);
                }
            }
            return by;
        }
        #endregion

    }

    // 
    //     public class DbMWSOperate {
    // 
    //         public static List<ModelWinServer> Client_GetWinServerList() {
    //             List<ModelWinServer> li = new List<ModelWinServer>();
    // 
    //             IList<FileInfo> al = FilesHelper.ReadDirectoryList(PathHelper.ConfigPath, ".db");
    // 
    // 
    //             for (int i = 0; i < al.Count; i++) {
    //                 if (al[i].Name.Length == 19) {//找到19位的db文件,进行加载...
    //                     DbTools db = new DbTools();
    //                     ModelWinServer mws = db.GetModelWinServer(al[i].FullName);
    //                     if (mws != null) {
    //                         li.Add(mws);
    //                     }
    //                 }
    //             }
    // 
    // 
    //             return li;
    //         }
    // 
    //     }
    // 
    // 
    //     /// <summary>
    //     /// 实例化操作任务的数据库的类
    //     /// </summary>
    //     public class DbTaskOperate {
    // 
    // 
    // 
    //         public static DbTaskOperate GetInstance() {
    //             // 定义一个标识确保线程同步
    // 
    //             if (db == null) {
    //                 lock (locker) {
    //                     if (db == null) {
    //                         db = new DbTaskOperate();
    //                     }
    // 
    //                 }
    //             }
    //             return db;
    //         }
    // 
    //         private static readonly object locker = new object();
    // 
    //         private static DbTaskOperate db;
    // 
    //         private static IStorageEngine engine;
    // 
    //         /// <summary>
    //         /// 默认使用localtask.db数据库,进行实例化.
    //         /// </summary>
    //         /// <param name="IsLocalDb">是否使用本地任务数据库:localtask.VDB,如果否:就相当于直接实例化一个空的new DbTools();</param>
    //         private DbTaskOperate() {
    //             try {
    //                 bool exist = FilesHelper.DirectoryExist(PathHelper.ConfigPath);
    //                 if (exist == false) {
    //                     FilesHelper.CreateDirectory(PathHelper.ConfigPath);
    //                 }
    // 
    //                 engine = STSdb.FromFile(PathHelper.ConfigPath + "\\localtask.VDB");
    // 
    //             } catch (Exception ex) {
    //                 EchoHelper.EchoException(ex);
    //             }
    // 
    //         }
    // 
    // 
    // 
    //         /// <summary>
    //         /// 获取当前db文件内的ModelTask的集合。这里主要考虑的是本地插件任务列表。例如:localtask.db
    //         /// </summary>
    //         /// <returns></returns>
    //         public List<ModelTask> GetModelTaskList() {
    //             List<ModelTask> mlist = new List<ModelTask>();
    //             try {
    //                 ITable<int, ModelTask> t = engine.OpenXTable<int, ModelTask>("ModelTask");
    // 
    //                 foreach (var item in t) {
    //                     mlist.Add(item.Value);
    //                 }
    // 
    //             } catch {
    // 
    //             }
    //             return mlist;
    //         }
    // 
    // 
    //         /// <summary>
    //         /// 获取当前db文件内的ModelTask的Count.
    //         /// </summary>
    //         /// <returns></returns>
    //         public int GetModelTaskCount() {
    //             int re = 0;
    //             try {
    //                 ITable<int, ModelTask> t = engine.OpenXTable<int, ModelTask>("ModelTask");
    //                 re = Convert.ToInt32(t.Count());
    //             } catch {
    //             }
    //             return re;
    //         }
    // 
    //         /// <summary>
    //         /// 获取当前db文件内的ModelTask的Count.传入当前的服务器链接IP地址。
    //         /// </summary>
    //         /// <returns></returns>
    //         public int GetModelTaskCount(string ip) {
    //             int re = 0;
    //             List<ModelTask> mlist = new List<ModelTask>();
    //             try {
    //                 ITable<int, ModelTask> t = engine.OpenXTable<int, ModelTask>("ModelTask");
    // 
    //                 foreach (var item in t) {
    //                     if (item.Value.mws.IP == ip) {
    //                         re++;
    //                     }
    // 
    //                 }
    // 
    //             } catch {
    // 
    //             }
    //             return re;
    //         }
    // 
    // 
    //         /// <summary>
    //         /// 添加任务到忍者任务列表中
    //         /// </summary>
    //         /// <param name="task"></param>
    //         public void SaveModelTask(ModelTask task) {
    //             try {
    //                 if (task.ArtTaskID == 0) {
    //                     EchoHelper.Echo("请设置ArtTaskID，程序保存出现错误。", "请联系Qin", EchoHelper.EchoType.错误信息);
    //                 }
    // 
    //                 var t = engine.OpenXTable<int, ModelTask>("ModelTask");
    //                 task.ID = nextTaskID(task.ID);
    //                 t[task.ID] = task;
    //                 engine.Commit();
    // 
    //             } catch (Exception ex) {
    //                 EchoHelper.EchoException(ex);
    //             }
    //         }
    // 
    //         /// <summary>
    //         /// 删除主键是TaskID的任务.针对的是LocalTask
    //         /// </summary>
    //         /// <param name="TaskID"></param>
    //         public void DelModelTask(int TaskID) {
    //             try {
    //                 var t = engine.OpenXTable<int, ModelTask>("ModelTask");
    //                 t.Delete(TaskID);
    //                 engine.Commit();
    // 
    //             } catch (Exception ex) {
    //                 EchoHelper.EchoException(ex);
    //             }
    //         }
    // 
    //         /// <summary>
    //         /// 删除主键是TaskID的任务.针对的是LocalTask
    //         /// </summary>
    //         /// <param name="TaskID"></param>
    //         public void DelModelTask(ModelWinServer mws) {
    //             try {
    //                 ArrayList al = new ArrayList();
    // 
    //                 ITable<int, ModelTask> f = engine.OpenXTable<int, ModelTask>("ModelTask");
    // 
    //                 foreach (var item in f) {
    //                     if (item.Value.mws.IP == mws.IP) {
    //                         al.Add(item.Key);
    //                     }
    //                 }
    // 
    //                 var t = engine.OpenXTable<int, ModelTask>("ModelTask");
    //                 for (int i = 0; i < al.Count; i++) {
    //                     t.Delete(Convert.ToInt32(al[i]));
    //                 }
    // 
    //                 engine.Commit();
    // 
    //             } catch (Exception ex) {
    //                 EchoHelper.EchoException(ex);
    //             }
    //         }
    // 
    // 
    //         /// <summary>
    //         /// 获取本地的单一任务
    //         /// </summary>
    //         /// <param name="task"></param>
    //         public ModelTask GetModelTask(int taskID) {
    //             ModelTask mt = new ModelTask();
    //             try {
    //                 var t = engine.OpenXTable<int, ModelTask>("ModelTask");
    //                 mt = t[taskID];
    //             } catch {
    // 
    // 
    //             }
    //             return mt;
    //         }
    // 
    //         /// <summary>
    //         /// 获取NEXT的任务ID,这个是中间的方法,不能轻易的关闭engine,否则接下来的方法无法使用engine
    //         /// </summary>
    //         /// <param name="taskID"></param>
    //         /// <returns></returns>
    //         private int nextTaskID(int taskID) {
    //             int re = 0;
    //             if (0 != taskID) {
    //                 re = taskID;
    //             } else {
    //                 try {
    //                     ITable<int, ModelTask> t = engine.OpenXTable<int, ModelTask>("ModelTask");
    //                     re = t.LastRow.Key + 1;
    //                 } catch (Exception) {
    //                     re = 1;
    //                 }
    //             }
    //             return re;
    // 
    //         }
    // 
    //     }
    // 
    //     /// <summary>
    //     /// 实例化操作文章的数据库的类
    //     /// </summary>
    //     public class DbArtOperate {
    // 
    //         public static DbArtOperate GetInstance() {
    //             // 定义一个标识确保线程同步
    // 
    //             if (db == null) {
    //                 lock (locker) {
    //                     if (db == null) {
    //                         db = new DbArtOperate();
    //                     }
    // 
    //                 }
    //             }
    //             return db;
    //         }
    // 
    //         private static readonly object locker = new object();
    // 
    //         private static DbArtOperate db;
    // 
    //         private static IStorageEngine engine;
    // 
    //         private DbArtOperate() {
    //             if (null == engine) {
    //                 try {
    //                     bool exist = FilesHelper.DirectoryExist(PathHelper.ConfigPath);
    //                     if (exist == false) {
    //                         FilesHelper.CreateDirectory(PathHelper.ConfigPath);
    //                     }
    // 
    //                     engine = STSdb.FromFile(PathHelper.ConfigPath + "\\localarticle.VDB");
    //                 } catch (Exception ex) {
    //                     EchoHelper.EchoException(ex);
    //                 }
    //             }
    //         }
    // 
    //         /// <summary>
    //         /// 保存文章
    //         /// </summary>
    //         /// <param name="ma"></param>
    //         public void SaveModelArticle(ModelArticle ma) {
    //             try {
    //                 if (ma.ArtTaskID == 0) {
    //                     EchoHelper.Echo("文章所属任务未知，ArtTaskID不能为空，请联系Qin进行处理！", "", EchoHelper.EchoType.异常信息);
    //                     return;
    //                 }
    //                 var t = engine.OpenXTable<int, ModelArticle>("ModelArticle");
    //                 ma.ID = nextArtID(ma.ID);
    //                 t[ma.ID] = ma;
    //                 engine.Commit();
    // 
    //             } catch (Exception ex) {
    //                 EchoHelper.EchoException(ex);
    //             }
    // 
    //         }
    // 
    //         /// <summary>
    //         /// 获取当前db文件内的ModelArticle的集合。这里主要考虑的是本地插件任务列表。
    //         /// </summary>
    //         /// <param name="ModelTaskID">传入当前任务的识别标志ID</param>
    //         /// <returns></returns>
    //         public List<ModelArticle> GetModelArticleList() {
    //             try {
    //                 ITable<int, ModelArticle> t = engine.OpenXTable<int, ModelArticle>("ModelArticle");
    //                 List<ModelArticle> mlist = new List<ModelArticle>();
    //                 foreach (var item in t) {
    //                     mlist.Add(item.Value);
    //                 }
    // 
    //                 return mlist;
    // 
    //             } catch {
    //                 return null;
    //             }
    //         }
    // 
    // 
    // 
    //         /// <summary>
    //         /// 获取当前db文件内的ModelArticle的集合。这里主要考虑的是本地插件任务列表。
    //         /// </summary>
    //         /// <param name="ModelTaskID">传入当前任务的识别标志ID</param>
    //         /// <returns></returns>
    //         public List<ModelArticle> GetModelArticleList(int taskArtID) {
    //             try {
    //                 ITable<int, ModelArticle> t = engine.OpenXTable<int, ModelArticle>("ModelArticle");
    //                 List<ModelArticle> mlist = new List<ModelArticle>();
    //                 foreach (var item in t) {
    //                     if (item.Value.ArtTaskID == taskArtID) {
    //                         mlist.Add(item.Value);
    //                     }
    //                 }
    // 
    //                 return mlist;
    // 
    //             } catch {
    //                 return null;
    //             }
    //         }
    // 
    // 
    //         /// <summary>
    //         /// 随机获取某个任务的一个文章.
    //         /// </summary>
    //         /// <param name="ArtTaskID"></param>
    //         /// <returns></returns>
    //         public ModelArticle GetModelArticleRndOne(int ArtTaskID) {
    //             ModelArticle ma = new ModelArticle();
    // 
    //             try {
    //                 ITable<int, ModelArticle> t = engine.OpenXTable<int, ModelArticle>("ModelArticle");
    //                 List<ModelArticle> mlist = new List<ModelArticle>();
    // 
    //                 foreach (var item in t) {
    //                     if (item.Value.ArtTaskID == ArtTaskID) {
    //                         mlist.Add(item.Value);
    //                     }
    //                 }
    // 
    //                 if (mlist.Count > 0) {
    //                     int rnd = new Random().Next(mlist.Count);
    //                     ma = mlist[rnd];
    //                 }
    //             } catch {
    //                 return null;
    //             }
    // 
    //             return ma;
    //         }
    // 
    //         /// <summary>
    //         /// 删除任务下所有文章.
    //         /// </summary>
    //         /// <param name="ArtTaskID"></param>
    //         public void DelModelArticleByTaskID(int ArtTaskID) {
    //             try {
    //                 ITable<int, ModelArticle> t = engine.OpenXTable<int, ModelArticle>("ModelArticle");
    // 
    //                 foreach (var item in t) {
    //                     if (item.Value.ArtTaskID == ArtTaskID) {
    //                         t.Delete(item.Key);
    //                     }
    //                 }
    // 
    //                 engine.Commit();
    // 
    //             } catch (Exception ex) {
    //                 EchoHelper.EchoException(ex);
    //             }
    // 
    //         }
    // 
    //         /// <summary>
    //         /// 删除的文章
    //         /// </summary>
    //         /// <param name="title">通过文章标题删除</param>
    //         public void DelModelArticleByTitle(string title) {
    //             try {
    //                 var t = engine.OpenXTable<int, ModelArticle>("ModelArticle");
    // 
    //                 foreach (var item in t) {
    //                     if (item.Value.Title == title) {
    //                         t.Delete(item.Key);
    //                     }
    //                 }
    // 
    //                 engine.Commit();
    // 
    //             } catch (Exception ex) {
    //                 EchoHelper.EchoException(ex);
    //             }
    // 
    //         }
    // 
    // 
    //         /// <summary>
    //         /// 获取文章数量,具体某一个任务中的文章数量
    //         /// </summary>
    //         /// <param name="ArtTaskID"></param>
    //         /// <returns></returns>
    //         public int GetModelArticleCount(int ArtTaskID) {
    //             int re = 0;
    // 
    //             try {
    //                 ITable<int, ModelArticle> t = engine.OpenXTable<int, ModelArticle>("ModelArticle");
    //                 List<ModelArticle> mlist = new List<ModelArticle>();
    //                 foreach (var item in t) {
    // 
    //                     if (item.Value.ArtTaskID == ArtTaskID) {
    //                         mlist.Add(item.Value);
    //                     }
    // 
    //                 }
    // 
    //                 re = mlist.Count;
    // 
    //             } catch (Exception ex) {
    //                 EchoHelper.EchoException(ex);
    //                 re = 0;
    //             }
    // 
    //             return re;
    // 
    //         }
    // 
    //         /// <summary>
    //         /// 获取文章数量,数据库中的文章总数
    //         /// </summary>
    //         public int GetModelArticleCount() {
    //             int re = 0;
    //             try {
    //                 ITable<int, ModelArticle> t = engine.OpenXTable<int, ModelArticle>("ModelArticle");
    //                 re = Convert.ToInt32(t.Count().ToString());
    //             } catch {
    //                 return 0;
    //             } finally {
    //                 //Close();
    //             }
    // 
    //             return re;
    // 
    //         }
    // 
    // 
    //         /// <summary>
    //         /// 获取NEXT的文章ID,这个是中间的方法,不能轻易的关闭engine,否则接下来的方法无法使用engine
    //         /// </summary>
    //         /// <param name="aID"></param>
    //         /// <returns></returns>
    //         private int nextArtID(int aID) {
    //             int re = 0;
    //             if (0 != aID) {
    //                 re = aID;
    //             } else {
    //                 try {
    //                     ITable<int, ModelArticle> t = engine.OpenXTable<int, ModelArticle>("ModelArticle");
    //                     re = t.LastRow.Key + 1;
    //                 } catch (Exception) {
    //                     re = 1;
    //                 }
    //             }
    //             return re;
    // 
    //         }
    // 
    // 
    // 
    //     }
    // 
    // 
    //     public class DbHashOperate {
    //         public static DbHashOperate GetInstance() {
    //             // 定义一个标识确保线程同步
    // 
    //             if (db == null) {
    //                 lock (locker) {
    //                     if (db == null) {
    //                         db = new DbHashOperate();
    //                     }
    // 
    //                 }
    //             }
    //             return db;
    //         }
    // 
    //         private static readonly object locker = new object();
    // 
    //         private static DbHashOperate db;
    // 
    //         private static IStorageEngine engine;
    // 
    //         private DbHashOperate() {
    //             try {
    //                 bool exist = FilesHelper.DirectoryExist(PathHelper.ConfigPath);
    //                 if (exist == false) {
    //                     FilesHelper.CreateDirectory(PathHelper.ConfigPath);
    //                 }
    // 
    //                 engine = STSdb.FromFile(PathHelper.ConfigPath + "\\localhash.VDB");
    //             } catch (Exception ex) {
    //                 EchoHelper.EchoException(ex);
    //             }
    //         }
    // 
    //         public List<ModelPickHash> GetModelPickHashList() {
    //             List<ModelPickHash> mlist = new List<ModelPickHash>();
    //             try {
    //                 ITable<int, ModelPickHash> t = engine.OpenXTable<int, ModelPickHash>("ModelPickHash");
    //                 foreach (var item in t) {
    //                     mlist.Add(item.Value);
    //                 }
    // 
    //             } catch (Exception ex) {
    //                 EchoHelper.EchoException(ex);
    //             }
    // 
    //             return mlist;
    //         }
    // 
    //         /// <summary>
    //         /// 保存PickHash
    //         /// </summary>
    //         /// <param name="h"></param>
    //         public void SaveModelPickHash(ModelPickHash h) {
    //             try {
    //                 var t = engine.OpenXTable<int, ModelPickHash>("ModelPickHash");
    //                 h.ID = nextHashID(h.ID);
    //                 t[h.ID] = h;
    //                 engine.Commit();
    // 
    //             } catch (Exception ex) {
    //                 EchoHelper.EchoException(ex);
    //             }
    // 
    //         }
    // 
    //         /// <summary>
    //         /// 保存PickHash
    //         /// </summary>
    //         /// <param name="h"></param>
    //         public void SaveModelPickHash(int hv) {
    //             try {
    //                 ModelPickHash h = new ModelPickHash();
    //                 h.HashValue = hv;
    //                 h.ID = nextHashID(h.ID);
    // 
    //                 var t = engine.OpenXTable<int, ModelPickHash>("ModelPickHash");
    //                 t[h.ID] = h;
    //                 engine.Commit();
    // 
    //             } catch (Exception ex) {
    //                 EchoHelper.EchoException(ex);
    //             }
    // 
    //         }
    // 
    // 
    //         /// <summary>
    //         /// 是否存在该条hashcode
    //         /// </summary>
    //         /// <param name="hv"></param>
    //         /// <returns></returns>
    //         public bool ExitHash(int hv) {
    //             bool re = false;
    // 
    //             try {
    //                 ITable<int, ModelPickHash> t = engine.OpenXTable<int, ModelPickHash>("ModelPickHash");
    //                 List<ModelPickHash> mlist = new List<ModelPickHash>();
    // 
    //                 foreach (var item in t) {
    //                     if (item.Value.HashValue == hv) {
    //                         mlist.Add(item.Value);
    //                     }
    //                 }
    // 
    //                 if (mlist.Count > 0) {
    //                     re = true;
    //                 }
    // 
    //             } catch {
    // 
    //             }
    // 
    // 
    // 
    // 
    //             return re;
    //         }
    // 
    // 
    //         /// <summary>
    //         /// 获取NEXT的PickHahsID,这个是中间的方法,不能轻易的关闭engine,否则接下来的方法无法使用engine
    //         /// </summary>
    //         /// <param name="id"></param>
    //         /// <returns></returns>
    //         private int nextHashID(int id) {
    //             int re = 0;
    //             if (0 != id) {
    //                 re = id;
    //             } else {
    //                 try {
    //                     ITable<int, ModelPickHash> t = engine.OpenXTable<int, ModelPickHash>("ModelPickHash");
    //                     re = t.LastRow.Key + 1;
    //                 } catch (Exception) {
    //                     re = 1;
    //                 }
    //             }
    //             return re;
    // 
    //         }
    // 
    //         private void test() {
    //             DbHashOperate.GetInstance().SaveModelPickHash(123456);
    //             bool b = DbHashOperate.GetInstance().ExitHash(123456);
    //             if (b) {
    // 
    //             }
    //         }
    // 
    //     }
    // 
    // 
    //     /// <summary>
    //     /// 针对modelsentence类中的字段说明。
    //     /// 如果taskid=0那么就是公用断言库，所有任务都可以利用他生成文章。
    //     /// 如果taskid是固定的数字，那就是对应的任务的断言库。
    //     /// </summary>
    //     public class DbSTSOperate {
    //         public static DbSTSOperate GetInstance() {
    //             // 定义一个标识确保线程同步
    // 
    //             if (db == null) {
    //                 lock (locker) {
    //                     if (db == null) {
    //                         db = new DbSTSOperate();
    //                     }
    // 
    //                 }
    //             }
    //             return db;
    //         }
    // 
    //         private static readonly object locker = new object();
    // 
    //         private static DbSTSOperate db;
    // 
    //         private static IStorageEngine engine;
    // 
    //         private DbSTSOperate() {
    //             try {
    //                 bool exist = FilesHelper.DirectoryExist(PathHelper.ConfigPath);
    //                 if (exist == false) {
    //                     FilesHelper.CreateDirectory(PathHelper.ConfigPath);
    //                 }
    // 
    //                 engine = STSdb.FromFile(PathHelper.ConfigPath + "\\localSentence.VDB");
    //             } catch (Exception ex) {
    //                 EchoHelper.EchoException(ex);
    //             }
    //         }
    // 
    //         /// <summary>
    //         /// 添加Str
    //         /// </summary>
    //         /// <param name="msts"></param>
    //         public void SaveModelSentence(ModelSentence msts) {
    //             try {
    //                 var t = engine.OpenXTable<int, ModelSentence>("ModelSentence");
    //                 bool exist = false;
    // 
    //                 long ii = t.Count();
    //                 if (ii > 0) {
    //                     foreach (var item in t) {
    //                         if (item.Value.body == msts.body) {
    //                             exist = true;
    //                             break;
    //                         }
    //                     }
    //                 }
    // 
    // 
    //                 if (!exist && msts.body.Length > 1) {
    //                     msts.ID = nextID(msts.ID);
    //                     t[msts.ID] = msts;
    //                     engine.Commit();
    //                 }
    // 
    //             } catch (Exception ex) {
    //                 EchoHelper.EchoException(ex);
    //             }
    // 
    //         }
    // 
    //         /// <summary>
    //         /// 删除Str
    //         /// </summary>
    //         /// <param name="body"></param>
    //         public void DelModelSentence(string body) {
    //             try {
    //                 ITable<int, ModelSentence> t = engine.OpenXTable<int, ModelSentence>("ModelSentence");
    // 
    //                 foreach (var item in t) {
    //                     if (item.Value.body == body) {
    //                         t.Delete(item.Key);
    //                     }
    //                 }
    // 
    //                 engine.Commit();
    // 
    //             } catch (Exception ex) {
    //                 EchoHelper.EchoException(ex);
    //             }
    // 
    //         }
    // 
    // 
    //         /// <summary>
    //         /// 获取STS中的Str集合，获取前500条。
    //         /// </summary>
    //         /// <returns></returns>
    //         public List<ModelSentence> GetModelSentenceList(int taskID) {
    // 
    //             List<ModelSentence> sre = new List<ModelSentence>();
    //             try {
    //                 ITable<int, ModelSentence> t = engine.OpenXTable<int, ModelSentence>("ModelSentence");
    //                 int i = 0;
    //                 foreach (var item in t) {
    //                     if (item.Value.taskID == taskID) {
    //                         sre.Insert(0, item.Value);
    //                         i++;
    //                         if (i > 500) break;
    //                     }
    //                 }
    //             } catch {
    //                 return null;
    //             }
    //             return sre;
    //         }
    // 
    // 
    //         /// <summary>
    //         /// 获取总条数
    //         /// </summary>
    //         /// <returns></returns>
    //         public int Count() {
    //             int re = 0;
    //             try {
    //                 ITable<int, ModelSentence> t = engine.OpenXTable<int, ModelSentence>("ModelSentence");
    //                 re = Convert.ToInt32(t.Count());
    //             } catch {
    // 
    //             }
    //             return re;
    //         }
    // 
    //         /// <summary>
    //         /// 获取NEXT的任务ID,这个是中间的方法,不能轻易的关闭engine,否则接下来的方法无法使用engine
    //         /// </summary>
    //         /// <param name="id"></param>
    //         /// <returns></returns>
    //         private int nextID(int aID) {
    //             int re = 0;
    //             if (0 != aID) {
    //                 re = aID;
    //             } else {
    //                 try {
    //                     ITable<int, ModelSentence> t = engine.OpenXTable<int, ModelSentence>("ModelSentence");
    //                     re = t.LastRow.Key + 1;
    //                 } catch (Exception) {
    //                     re = 1;
    //                 }
    //             }
    //             return re;
    // 
    //         }
    // 
    // 
    // 
    //     }
    // 


}
