using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Model;
using DAL;
using System.Data;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
namespace UI
{
    /// <summary>
    /// W 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    [System.Web.Script.Services.ScriptService]
    public class W : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod(EnableSession = true)]
        public bool seleus(string name, string pwd)
        {
            if (Convert.ToBoolean(DBHelp.ExecuteObject("select count(*) from users where UserLName = @name and UserLPWD = @pwd", new SqlParameter[] {
                new SqlParameter("@name",name),
                new SqlParameter("@pwd",pwd),
            })))
            {
                
                Session["name"] = name;
                int id = Convert.ToInt32(DBHelp.ExecuteObject(string.Format(@"select UserID from users where UserLName = @UserLName"), new SqlParameter[] {
                    new SqlParameter("@UserLName",Session["name"].ToString()),
                }));
                Session["id"] = id;
                return true;
            }
            else
            {
                return false;
            }
        }

        [WebMethod(EnableSession = true)]
        public string getuser()
        {
            if (Session["name"] != null) {
                return Session["name"].ToString();
            }
            else
            {
                return "";
            }

        }

        [WebMethod(EnableSession = true)]
        public List<Menu> seleMenu() {
            //return DALBase.Selects<Menu>();
            string sql = string.Format(@"select * from menu where ID in
                                        (select MenuID from dbo.Power where RoleID in
                                            (select RoleID from dbo.Users where UserLName = '{0}'))", Session["name"].ToString());
            return DALBase.SelectByWhere<Menu>(sql);
        }

        [WebMethod]
        public List<Role> seleRole() {
            return DALBase.Selects<Role>();
        }
        [WebMethod]
        public List<Role> seleRolest()
        {
            return DALBase.Selects<Role>();
        }

        [WebMethod]
        public List<Menu> seleMenus()
        {
            return DALBase.Selects<Menu>();
        }

        [WebMethod]
        public string selequanxian(int id) {
            JavaScriptSerializer js = new JavaScriptSerializer();
            Dictionary<string, object> dic = new Dictionary<string, object>();
            string sql1 = string.Format(@"select * from Menu where ID in(
select MenuID from Power where  RoleID={0})", id);
            string sql2 = string.Format(@"select * from Menu where ID not in(
select MenuID from Power where  RoleID={0})", id);
            dic.Add("a1", DALBase.Selects<Menu>());
            dic.Add("you", DALBase.SelectByWhere<Menu>(sql1));
            dic.Add("mei", DALBase.SelectByWhere<Menu>(sql2));
            return js.Serialize(dic);
        }

        [WebMethod]
        public bool xiugaiquanxian(int id, List<int> ids) {
            HashSet<int> ha = new HashSet<int>(ids);
            try
            {
                Power p1 = new Power() {
                    RoleID = id
                };
                DBHelp.CUD(string.Format(@"delete Power where RoleID = {0}", id));
                foreach (var item in ha)
                {
                    Power p2 = new Power() {
                        RoleID = id,
                        MenuID = item,
                    };
                    DALBase.Insert(p2);
                }
                return true;
            }
            catch
            {

                return false;
            }
        }

        //[WebMethod]
        //public List<Role> seleRoles() {
        //    return DALBase.Selects<Role>();
        //}
        [WebMethod]
        public kong<Role> seleRoles(int index, int size)
        {

            string sql = string.Format(@"select top {0} * from Role where id not in
                                (select top {1} id from Role)", size, (index - 1) * size);

            int tiaoshu = DALBase.GetCount<Role>();
            int count = tiaoshu % size == 0 ? tiaoshu / size : tiaoshu / size + 1;
            List<Role> u = DALBase.SelectByWhere<Role>(sql);
            kong<Role> s = new kong<Role>();
            s.biao = u;
            s.count = count;
            return s;
        }

        [WebMethod]
        public bool upRole(int id, string name) {
            Role s = new Role() { ID = id, RoleName = name };
            return Convert.ToBoolean(DALBase.Update(s));
        }

        [WebMethod]
        public bool delRoles(int id)
        {
            return Convert.ToBoolean(DALBase.Delete<Role>(id));
        }

        [WebMethod]
        public int inserRole(string name)
        {
            Role r = new Role() {
                RoleName = name,
            };
            if (Convert.ToInt32(DALBase.Insert(r)) > 0) {
                return Convert.ToInt32(DBHelp.ExecuteObject("select max(id) from Role"));
            } else {
                return -1;
            };
        }

        [WebMethod]
        public int inRole(Users u)
        {
            if (Convert.ToBoolean(DALBase.Insert(u)))
            {
                return Convert.ToInt32(DBHelp.ExecuteObject("select max(UserID) from Users"));
            }
            else {
                return -1;
            }
        }


        [WebMethod]
        public kong<view_seleusers> seleUsers(int index, int size)
        {

            string sql = string.Format(@"select top {0} * from view_seleusers where UserID not in
                                (select top {1} UserID from Users)", size, (index - 1) * size);

            int tiaoshu = DALBase.GetCount<view_seleusers>();
            int count = tiaoshu % size == 0 ? tiaoshu / size : tiaoshu / size + 1;
            List<view_seleusers> u = DALBase.SelectByWhere<view_seleusers>(sql);
            kong<view_seleusers> s = new kong<view_seleusers>();
            s.biao = u;
            s.count = count;
            return s;
        }

        [WebMethod]
        public int dleUsers(int id)
        {
            return DALBase.Delete<Users>(id);
        }

        [WebMethod]
        public bool upUsers(Users u)
        {
            return Convert.ToBoolean(DALBase.Update(u));
        }


        [WebMethod]
        public string selejuese(int id)
        {
            return DBHelp.ExecuteObject(string.Format(@"select RoleName from dbo.Role where ID = {0}", id)).ToString();
        }


        
        //客户销售机会管理
        [WebMethod]
        public kong<Chances> seleChances(int index, int size)
        {

            string sql = string.Format(@"select top {0} * from Chances where ChanID not in
                                (select top {1} ChanID from Chances) and Chanstate != 1", size, (index - 1) * size);

            int tiaoshu = DALBase.GetCount<Chances>();
            int count = tiaoshu % size == 0 ? tiaoshu / size : tiaoshu / size + 1;
            List<Chances> u = DALBase.SelectByWhere<Chances>(sql);
            kong<Chances> s = new kong<Chances>();
            s.biao = u;
            s.count = count;
            return s;
        }

        [WebMethod]
        public int delChances(int id)
        {
            return DALBase.Delete<Chances>(id);
        }
        [WebMethod]
        public int upChances(Chances c) {
            if (DALBase.Update(c) > 0)
            {
                return 0;
            }
            else {
                return -1;
            };
        }

        [WebMethod(EnableSession = true)]
        public int inChances(Chances c)
        {

            Chances b = new Chances() {
                ChanCreateMan = Convert.ToInt32(Session["id"]),
                ChanName = c.ChanName,
                ChanLinkMan = c.ChanLinkMan,
                ChanRate = c.ChanRate,
                ChanLinkTel = c.ChanLinkTel,
                ChanTitle = c.ChanTitle,
                ChanDesc = c.ChanDesc,

            };

             
            if (DALBase.Insert(b) > 0)
            {
                return Convert.ToInt32(DBHelp.ExecuteObject("select max(ChanID) from dbo.Chances"));
            }
            else {
                return -1;
            };
        }

        //客戶機會分配
        //[WebMethod]
        //public List<V_Chances> seleV_Chances() {
        //    return DALBase.Selects<V_Chances>();
        //}
        [WebMethod]
        public kong<V_Chances> seleV_Chances(int index, int size)
        {

            string sql = string.Format(@"select top {0} * from V_Chances where ChanID not in
                                (select top {1} ChanID from V_Chances)", size, (index - 1) * size);

            int tiaoshu = DALBase.GetCount<V_Chances>();
            int count = tiaoshu % size == 0 ? tiaoshu / size : tiaoshu / size + 1;
            List<V_Chances> u = DALBase.SelectByWhere<V_Chances>(sql);
            kong<V_Chances> s = new kong<V_Chances>();
            s.biao = u;
            s.count = count;
            return s;
        }

        [WebMethod]
        public List<V_Chances> seleV_Chancesmohu(string channame,string ChanLinkMan,string ChanCreateManName,string ChanDueManName)
        {
            string sql = string.Format(@"select * from V_Chances  
                                    where channame like '{0}'
                                    or ChanLinkMan like '{1}'
                                    or ChanCreateManName like '{2}'
                                    or ChanDueManName like '{3}'",
                                    channame != "" ? "%" + channame + "%" : "",
                                    ChanLinkMan != "" ? "%" + ChanLinkMan + "%" : "", 
                                    ChanCreateManName != "" ? "%" + ChanCreateManName + "%" : "",
                                    ChanDueManName != "" ? "%" + ChanDueManName + "%" : ""
                                    );
             
            return DALBase.SelectByWhere<V_Chances>(sql);
        }
        //开发失败
        [WebMethod]
        public List<Plans> selePlansNO(int id) {
            string sql = ("select * from Plans where ChanID = "+id+"");
            return DALBase.SelectByWhere<Plans>(sql);
        }


        //客户信息管理
        [WebMethod]
        public kong<V_Customers> seleV_Customers(int index, int size)
        {

            string sql = string.Format(@"select top {0} * from V_Customers where CusID not in
                                (select top {1} CusID from V_Customers)", size, (index - 1) * size);

            int tiaoshu = DALBase.GetCount<V_Customers>();
            int count = tiaoshu % size == 0 ? tiaoshu / size : tiaoshu / size + 1;
            List<V_Customers> u = DALBase.SelectByWhere<V_Customers>(sql);
            kong<V_Customers> s = new kong<V_Customers>();
            s.biao = u;
            s.count = count;
            return s;
        }

        [WebMethod]
        public bool upV_Customers(Customers c)
        {
            return Convert.ToBoolean(DALBase.Update(c));
        }

        [WebMethod]
        public kong<LinkMans> seleLinkMans(int index, int size, int id)
        {

            string sql = string.Format(@"select top {0} * from LinkMans where LMID not in
                                (select top {1} LMID from LinkMans) and cusid = {2}", size, (index - 1) * size,id);

            int tiaoshu = DALBase.GetCount<LinkMans>();
            int count = tiaoshu % size == 0 ? tiaoshu / size : tiaoshu / size + 1;
            List<LinkMans> u = DALBase.SelectByWhere<LinkMans>(sql);
            kong<LinkMans> s = new kong<LinkMans>();
            s.biao = u;
            s.count = count;
            return s;
        }

        [WebMethod]
        public int delLinkMans(int id)
        {
            return DALBase.Delete<LinkMans>(id);
        }

        [WebMethod]
        public kong<Activitys> seleActivitys(int index, int size, int id)
        {

            string sql = string.Format(@"select top {0} * from Activitys where ActID not in
                                (select top {1} ActID from Activitys) and cusid = {2}", size, (index - 1) * size, id);

            int tiaoshu = DALBase.GetCount<Activitys>();
            int count = tiaoshu % size == 0 ? tiaoshu / size : tiaoshu / size + 1;
            List<Activitys> u = DALBase.SelectByWhere<Activitys>(sql);
            kong<Activitys> s = new kong<Activitys>();
            s.biao = u;
            s.count = count;
            return s;
        }

        [WebMethod]
        public int delActivitys(int id)
        {
            return DALBase.Delete<Activitys>(id);
        }


        //客户流失管理
        [WebMethod]
        public kong<v_customlosts> seleCustomLosts(int index, int size)
        {

            string sql = string.Format(@"select top {0} * from v_customlosts where CLID not in
                                (select top {1} CLID from v_customlosts)", size, (index - 1) * size);

            int tiaoshu = DALBase.GetCount<v_customlosts>();
            int count = tiaoshu % size == 0 ? tiaoshu / size : tiaoshu / size + 1;
            List<v_customlosts> u = DALBase.SelectByWhere<v_customlosts>(sql);
            kong<v_customlosts> s = new kong<v_customlosts>();
            s.biao = u;
            s.count = count;
            return s;
        }

        //服务归档
        [WebMethod]
        public kong<view_CustomServices> seleview_CustomServices(int index, int size)
        {

            string sql = string.Format(@"select top {0} * from view_CustomServices where CSID not in
                                (select top {1} CSID from view_CustomServices)", size, (index - 1) * size);

            int tiaoshu = DALBase.GetCount<view_CustomServices>();
            int count = tiaoshu % size == 0 ? tiaoshu / size : tiaoshu / size + 1;
            List<view_CustomServices> u = DALBase.SelectByWhere<view_CustomServices>(sql);
            kong<view_CustomServices> s = new kong<view_CustomServices>();
            s.biao = u;
            s.count = count;
            return s;
        }

        [WebMethod]
        public int inCustomServices(CustomServices s)
        {
            s.CSCreateDate = DateTime.Now;
            return DALBase.Insert(s);
        }

        [WebMethod]
        public bool upCustomServices(int id, string name)
        {
            string sql = string.Format(@"select userid from dbo.Users where username = '{0}'",name);
            
            int idt = Convert.ToInt32(DBHelp.ExecuteObject(sql));
            //int idd = Convert.ToInt32(idt);
            //CustomServices s = new CustomServices() { CSID = id, CSDueID = idt };
            string sql2 = string.Format(@"update CustomServices set CSDueID = {0} where CSID={1}", idt, id);
            return Convert.ToBoolean(DBHelp.CUD(sql2));
        }

        [WebMethod]
        public List<CustomServices> seleCustomServices()
        {
            return DALBase.Selects<CustomServices>();
        }

        [WebMethod]
        public List<Users> sleServiceType()
        {
            return DALBase.Selects<Users>();
        }

        [WebMethod]
        public List<view_CustomServices> seleview_CustomServicesname(string name)
        {

            string sql = string.Format(@"select * from view_CustomServices where CusName like '%"+name+ "%'");

            //int tiaoshu = DALBase.GetCount<view_CustomServices>();
            //int count = tiaoshu % size == 0 ? tiaoshu / size : tiaoshu / size + 1;
            List<view_CustomServices> u = DALBase.SelectByWhere<view_CustomServices>(sql);
            //kong<view_CustomServices> s = new kong<view_CustomServices>();
            //s.biao = u;
            //s.count = count;

            return u;
        }

        [WebMethod]
        public List<view_CustomServices> seleview_CustomServicesfuwu(string fuwu)
        {

            string sql = string.Format(@"select * from view_CustomServices where STName = '" + fuwu + "'");

            //int tiaoshu = DALBase.GetCount<view_CustomServices>();
            //int count = tiaoshu % size == 0 ? tiaoshu / size : tiaoshu / size + 1;
            List<view_CustomServices> u = DALBase.SelectByWhere<view_CustomServices>(sql);
            //kong<view_CustomServices> s = new kong<view_CustomServices>();
            //s.biao = u;
            //s.count = count;

            return u;
        }
    }
}
