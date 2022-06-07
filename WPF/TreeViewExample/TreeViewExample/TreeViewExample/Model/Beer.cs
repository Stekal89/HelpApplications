using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeViewExample.Model
{
    public class Beer
    {
        private string _name = "";
        private string _alcolhol = "";

        public string Name { get => _name; set => _name = value; }
        public string Alcolhol { get => _alcolhol; set => _alcolhol = value; }
    }
}
