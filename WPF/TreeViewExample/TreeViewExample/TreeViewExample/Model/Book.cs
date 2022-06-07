using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeViewExample.Model
{
    public class Book
    {
        private string _name = "";
        private string _isbn = "";

        public string Name { get => _name; set => _name = value; }
        public string Isbn { get => _isbn; set => _isbn = value; }
    }
}
