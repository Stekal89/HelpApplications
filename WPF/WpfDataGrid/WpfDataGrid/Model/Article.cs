using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDataGrid.Model
{
    public enum Group
    {
        Obi,
        Baumax,
        Hervis,
        Intersport
    }
    public class Article
    {
        public Group Group { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public Article(Group group, string name, int count)
        {
            Group = group;
            Name = name;
            Count = count;
        }
    }
}
