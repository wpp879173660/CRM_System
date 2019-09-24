using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class KeyinfoAttribute : Attribute
    {
        public string KeyName { get; set; }
        public KeyinfoAttribute(string name)
        {
            KeyName = name;
        }
    }
}
