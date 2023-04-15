using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part3_Mod24_Console
{
    class Table
    {
        public string Name { get; set; }
        public List<string> Fields { get; set; }
        public string ImportantField { get; set; }
        public Table()
        {
            Fields = new();
        }
    }
}
