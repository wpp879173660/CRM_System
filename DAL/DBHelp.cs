using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//导入名称空间
using System.Data;
using System.Data.SqlClient;
namespace DAL
{
   /// <summary>
    /// 这个是数据库的操作类
   /// </summary>
    public  class DBHelp
    {
        #region info
        //public DBHelp()  //创建对象用的
        //{ }
        //~DBHelp()   //释放对象用的
        //{ }
        #endregion

        /// <summary>
        /// 得到配置文件里面的链接字符串
        /// </summary>
        private static readonly string connString = System.Configuration.ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        /// <summary>
        /// 这个是增删改的方法
        /// </summary>
        /// <param name="sql">构造好的SQL语句</param>
        /// <returns>受影响的行数0,非0</returns>
      public static int CUD(string sql)
        {
          
          // 我们的代码是保存在内存里面的。
         // using 可以自动调用CSharp C# 里面的垃圾回收机制
          using(SqlConnection conn = new SqlConnection(connString))
          {
              conn.Open();
              SqlCommand cmd = new SqlCommand(sql, conn);
              return cmd.ExecuteNonQuery();
          }//调用SqlConnection析构函数
        }

      public static int CUD(string sql, SqlParameter[] ps)
      {
          using (SqlConnection conn = new SqlConnection(connString))
          {
              conn.Open();
              SqlCommand cmd = new SqlCommand(sql, conn);
              cmd.Parameters.AddRange(ps);
              //foreach (SqlParameter p in ps)
              //{
              //    cmd.Parameters.Add(p);
              //}
              return cmd.ExecuteNonQuery();
          }//调用SqlConnection析构函数
      }

       /// <summary>
       /// 查询，返回数据流
       /// </summary>
       /// <param name="sql"></param>
       /// <returns></returns>
      public static SqlDataReader ExecuteDataReader(string sql)
      {
          SqlConnection conn = new SqlConnection(connString);
          
              conn.Open();
              SqlCommand cmd = new SqlCommand(sql, conn);
              SqlDataReader sd = cmd.ExecuteReader(CommandBehavior.CloseConnection);
              
              return sd;
          
      }

      public static SqlDataReader ExecuteDataReader(string sql,SqlParameter [] ps)
      {
          SqlConnection conn = new SqlConnection(connString);

          conn.Open();
          SqlCommand cmd = new SqlCommand(sql, conn);
          cmd.Parameters.AddRange(ps);
          SqlDataReader sd = cmd.ExecuteReader(CommandBehavior.CloseConnection);

          return sd;

      }

        /// <summary>
        /// 查询单值
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
      public static object ExecuteObject(string sql)
      {
          using (SqlConnection conn = new SqlConnection(connString))
          {
              conn.Open();
              SqlCommand cmd = new SqlCommand(sql, conn);
              return cmd.ExecuteScalar();
          }
      }

      public static object ExecuteObject(string sql,SqlParameter []ps)
      {
          using (SqlConnection conn = new SqlConnection(connString))
          {
              conn.Open();
              SqlCommand cmd = new SqlCommand(sql, conn);
              cmd.Parameters.AddRange(ps);
              return cmd.ExecuteScalar();
          }
      }


        //dataSet 上机练习自己封装2个
    }
}
