using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
      [Keyinfo("CLID")]
      public class CustomLosts: ModelBase
      {
         public System.Int32 ? CLID { get; set; }
         public System.Int32 ? CusID { get; set; }
         public System.DateTime ? CLOrderDate { get; set; }
         public System.DateTime ? CLDate { get; set; }
         public System.String  CLMeasures { get; set; }
         public System.DateTime ? CLEnterDate { get; set; }
         public System.String  CLReason { get; set; }
         public System.Int32 ? CLState { get; set; }
      }
}
