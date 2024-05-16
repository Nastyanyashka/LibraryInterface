using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Library.Entities
{
    public class Category
    {
        string name;
        List<Reader> readers = new List<Reader>();
        int id;
        public Category()
        {
            name = string.Empty;
        }

        public string Name { get { return name; } set { name = value; } }
        public int Id { get { return id; } set { id = value; } }
        public List<Reader> Readers { get { return readers; } set { readers = value; } }
    }
}
