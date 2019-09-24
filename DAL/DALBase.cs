using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
namespace DAL
{
   public class DALBase
    {


        public static int Insert(ModelBase s)
        {
            Type t = s.GetType();//  反射，程序运行的时候动态的获得类型
            //如何得到类型的名称
            string tableName = t.Name;
            List<SqlParameter> ls = new List<SqlParameter>();
            StringBuilder sb = new StringBuilder();
            StringBuilder sb1 = new StringBuilder();
            sb.Append("insert into ");
            sb.Append(tableName);
            sb.Append("(");

            // 得到了这个类型里面的所有的公共的属性 (列名)
            PropertyInfo[] ps = t.GetProperties();
            foreach (PropertyInfo item in ps)
            {

                if (item.GetValue(s, null) != null)
                {
                    sb.Append(item.Name);
                    sb.Append(",");
                    SqlParameter p = new SqlParameter("@" + item.Name, item.GetValue(s, null));
                    ls.Add(p);
                    //得到属性的值
                    // Console.WriteLine(item.GetValue(s, null)); 
                    sb1.Append("@" + item.Name);
                    sb1.Append(",");
                }


            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append(")");
            sb.Append(" values (");
            sb1.Remove(sb1.Length - 1, 1);
            sb1.Append(")");
            sb.Append(sb1);

            return DBHelp.CUD(sb.ToString(), ls.ToArray());
            
        }


        public static int Delete<T>(object s)
        {
            Type t = typeof(T);
            string tableName = t.Name;
            StringBuilder sb = new StringBuilder();
            sb.Append("delete from ");
            sb.Append(tableName);
            sb.Append(" where ");
            //找主键
            string kyName = "";
            
            object[] objs = t.GetCustomAttributes(true);
            if (objs.Length > 0)
            {
                kyName = (objs[0] as KeyinfoAttribute).KeyName;

            }
            else
            {
                throw new Exception("您的模型类上没有标示主键信息....");
            }
            sb.Append(kyName);
            sb.Append("=");
            sb.Append("@" + kyName);
            // delete from tableName where  id=@id
            SqlParameter p = new SqlParameter("@" + kyName, s);
            return DBHelp.CUD(sb.ToString(), new SqlParameter[] { p });
        }


        //delete方法 多条件删除 and  


        public static int Update(ModelBase s)
        {
            // update student  set age=@age,sex=@sex where ID=@ID
            Type t = s.GetType();
            string tableName = t.Name;
            StringBuilder sb = new StringBuilder();
            sb.Append("update ");
            sb.Append(tableName);
            sb.Append(" set ");
            string kyName = "";
            //得到自定义的特性
            object[] objs = t.GetCustomAttributes(true);
            List<SqlParameter> ls = new List<SqlParameter>();
            if (objs.Length > 0)
            {
                kyName = (objs[0] as KeyinfoAttribute).KeyName;
               
            }
            else
            {
                throw new Exception("...............");
            }

            foreach (PropertyInfo item in t.GetProperties())
            {
                //排除掉没有赋值的属性
                if (item.GetValue(s, null) != null)
                {
                    if (item.Name.ToLower() != kyName.ToLower())
                    {
                        sb.Append(item.Name);
                        sb.Append("=");
                        sb.Append("@" + item.Name);
                        sb.Append(",");

                        ls.Add(new SqlParameter("@" + item.Name, item.GetValue(s, null)));
                    }
                    else
                    {
                        ls.Add(new SqlParameter("@" + kyName, item.GetValue(s, null)));
                    }
                }
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append(" where ");
            sb.Append(kyName);
            sb.Append("=");
            sb.Append("@" + kyName);

            return DBHelp.CUD(sb.ToString(), ls.ToArray());
        }


        public static List<T> Selects<T>() where T : ModelBase
        {

            List<T> ls = new List<T>();

            //生成一段SQL语句
            Type type = typeof(T);
            string sql = "select * from " + type.Name;
            //泛型集合，在编译之前就对类型进行了严格的检查
            using (SqlDataReader sd = DBHelp.ExecuteDataReader(sql))
            {
                while (sd.Read()) //读取数据库里面的数据
                {
                    T t1 = Activator.CreateInstance<T>();
                    //得到这个类型的属性
                    PropertyInfo[] ps = type.GetProperties();
                    foreach (var item in ps)
                    {
                        if (sd[item.Name] != DBNull.Value)
                        {
                            //赋值
                            item.SetValue(t1, sd[item.Name]);
                        }
                    }
                    ls.Add(t1);
                    //Studens s = new Studens();
                    //s.classID = 

                }
            }

            return ls;
        }

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entiy"></param>
        /// <returns></returns>
        public static List<T> SelectByWhere<T>(ModelBase entiy) where T : ModelBase
        {
            List<T> ls = new List<T>();



            return ls;
        }
        public static List<T> SelectByWhere<T>(string sql) where T : ModelBase
        {
            List<T> ls = new List<T>();

            Type t = typeof(T);

            using (SqlDataReader sd = DBHelp.ExecuteDataReader(sql)) 
            {
                while (sd.Read()) //读取数据库里面的数据
                {
                    T t1 = Activator.CreateInstance<T>();
                    //得到这个类型的属性
                    PropertyInfo[] pp = t.GetProperties();
                    foreach (var item in pp)
                    {
                        if (sd[item.Name] != DBNull.Value)
                        {
                            //赋值
                            item.SetValue(t1, sd[item.Name]);
                        }
                    }
                    ls.Add(t1);


                }
            }



            return ls;
        }


        public static int GetCount<T>() where T : ModelBase
        {
            Type t = typeof(T);
            string tableName = t.Name;
            string sql = "select count(*) from " + tableName + "";
            return Convert.ToInt32(DBHelp.ExecuteObject(sql));
        }

        public static int GetCount(string sql)
        {
          
            return Convert.ToInt32(DBHelp.ExecuteObject(sql));
        }
    }
}
