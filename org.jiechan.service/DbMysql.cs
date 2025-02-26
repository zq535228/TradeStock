﻿using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using System.Collections;
using System.Data;

namespace org.jiechan.service {
    public class DbMysql {

        public static string connectionString = "Server=renzhe2013.mysql.rds.aliyuncs.com;port=3306;user id=renzhe_org;password='Zqowner3';charset=utf8;database=renzhe_org;pooling=false;";

        public static void connX3RENZHEORG() {
            connectionString = "Server=renzhe2013.mysql.rds.aliyuncs.com;port=3306;user id=x3_renzhe_org;password='z123456';charset=utf8;database=x3_renzhe_org;pooling=false;";
        }

        public static void connBBSRENZHEORG() {
            connectionString = "Server=renzhe2013.mysql.rds.aliyuncs.com;port=3306;user id=renzhe_org;password='Zqowner3';charset=utf8;database=renzhe_org;pooling=false;";
        }

        public static void connCreate(string server , string uid , string password) {
            connectionString = "Server=" + server + ";port=3306;user id=" + uid + ";password='" + password + "';charset=utf8;database=renzhe_org;pooling=false;";
        }

        public static bool Ping(string conn) {
            bool re = true;
            using(MySqlConnection connection = new MySqlConnection(conn)) {
                try {
                    connection.Open();
                } catch {
                    re = false;
                } finally {
                    connection.Close();
                }
            }
            return re;
        }



        #region  Sql数据库基础类
        /// <summary>
        /// 执行SQL
        /// </summary>
        /// <param name="SQLString">SQL</param>
        /// <returns>INT</returns>
        public static int ExecuteSql(string SQLString) {
            using(MySqlConnection connection = new MySqlConnection(connectionString)) {
                using(MySqlCommand cmd = new MySqlCommand(SQLString , connection)) {
                    try {
                        connection.Open();
                        int rows = cmd.ExecuteNonQuery();
                        return rows;
                    } catch(System.Data.SqlClient.SqlException E) {
                        connection.Close();
                        throw new Exception(E.Message);
                    } finally {
                        connection.Close();
                    }
                }
            }
        }

        public static int ExecuteSql(string SQLString , string content) {
            using(MySqlConnection connection = new MySqlConnection(connectionString)) {
                MySqlCommand cmd = new MySqlCommand(SQLString , connection);
                System.Data.SqlClient.SqlParameter myParameter = new System.Data.SqlClient.SqlParameter("@content" , MySqlDbType.String);
                myParameter.Value = content;
                cmd.Parameters.Add(myParameter);
                try {
                    connection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                } catch(System.Data.SqlClient.SqlException E) {
                    throw new Exception(E.Message);
                } finally {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// 执行SQL数组
        /// </summary>
        /// <param name="SQLStringList">SQL数组</param>
        public static void ExecuteSqlTran(ArrayList SQLStringList) {
            using(MySqlConnection conn = new MySqlConnection(connectionString)) {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                MySqlTransaction tx = conn.BeginTransaction();
                cmd.Transaction = tx;
                try {
                    for(int n = 0; n < SQLStringList.Count; n++) {
                        string strsql = SQLStringList[n].ToString();
                        if(strsql.Trim().Length > 1) {
                            cmd.CommandText = strsql;
                            cmd.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                } catch(System.Data.SqlClient.SqlException E) {
                    tx.Rollback();
                    throw new Exception(E.Message);
                } finally {
                    cmd.Dispose();
                    conn.Close();
                }
            }
        }


        /// <summary>
        /// DataTable
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>DataTable</returns>
        public static DataTable GetDataTable(string SQLString) {
            using(MySqlConnection connection = new MySqlConnection(connectionString)) {
                DataSet ds = new DataSet();
                try {
                    connection.Open();
                    MySqlDataAdapter command = new MySqlDataAdapter(SQLString , connection);
                    command.Fill(ds);
                } catch(System.Data.SqlClient.SqlException ex) {
                    throw new Exception(ex.Message);
                } finally {
                    connection.Close();
                }
                return ds.Tables[0];
            }
        }

        /// <summary>
        /// 获得IDataReader
        /// </summary>
        /// <param name="strSQL">SQL语句</param>
        /// <returns>IDataReader</returns>
        public static IDataReader GetReader(string strSQL) {
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand(strSQL , connection);
            try {
                connection.Open();
                IDataReader myReader = cmd.ExecuteReader();
                return myReader;

            } catch(System.Data.SqlClient.SqlException e) {
                throw new Exception(e.Message);
            }
        }




        /// <summary>
        /// 获得单一字段
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>返回object</returns>
        public static object GetScalar(string SQLString) {
            using(MySqlConnection connection = new MySqlConnection(connectionString)) {
                using(MySqlCommand cmd = new MySqlCommand(SQLString , connection)) {
                    try {
                        connection.Open();
                        object obj = cmd.ExecuteScalar();
                        if((Object.Equals(obj , null)) || (Object.Equals(obj , System.DBNull.Value))) {
                            return null;
                        } else {
                            return obj;
                        }
                    } catch(System.Data.SqlClient.SqlException e) {
                        connection.Close();
                        throw new Exception(e.Message);
                    } finally {
                        connection.Close();
                    }
                }
            }
        }

        #endregion






    }


}
