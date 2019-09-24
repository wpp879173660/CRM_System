using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
      [Keyinfo("UserID")]
      public class view_CustomServices: ModelBase
      {
         public System.Int32 ? CSID { get; set; }
         public System.String  CusName { get; set; }
         public System.String  CSTitle { get; set; }
         public System.String  STName { get; set; }
         public System.DateTime ? CSCreateDate { get; set; }
         public System.String  UserName { get; set; }
         public System.DateTime ? CSDueDate { get; set; }
         public System.String  name { get; set; }
         //public System.Int32 ? STID { get; set; }
         //public System.Int32 ? CusID { get; set; }
         //public System.Int32 ? UserID { get; set; }
         //public System.Int32 ? UserID { get; set; }
      }
}
