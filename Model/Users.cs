using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
      [Keyinfo("UserID")]
      public class Users: ModelBase
    {
         public System.Int32 ? UserID { get; set; }
         public System.Int32 ? RoleID { get; set; }
         public System.String  UserLName { get; set; }
         public System.String  UserLPWD { get; set; }
         public System.String  UserName { get; set; }
    }
}
