using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
      [Keyinfo("STID")]
      public class ServiceType: ModelBase
      {
         public System.Int32 ? STID { get; set; }
         public System.String  STName { get; set; }
      }
}
