using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public class kong<T>where T:ModelBase
    {
        public int count { get; set; }
        public List<T> biao { get; set; }
    }
}
