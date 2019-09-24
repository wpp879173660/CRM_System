using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
      [Keyinfo("ID")]
      public class Power: ModelBase
      {
         public System.Int32 ? ID { get; set; }
         public System.Int32 ? RoleID { get; set; }
         public System.Int32 ? MenuID { get; set; }
      }
}
