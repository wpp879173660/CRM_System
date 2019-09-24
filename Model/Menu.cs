using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
      [Keyinfo("ID")]
      public class Menu: ModelBase
      {
         public System.Int32 ? ID { get; set; }
         public System.String  MenuName { get; set; }
         public System.Int32 ? ParentID { get; set; }
         public System.String  Url { get; set; }
         public System.String  Icon { get; set; }
      }
}
