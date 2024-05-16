using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Entities
{
    public class TypeOfComposition
    {
        string name;
        int id;
        List<Composition> compositions = new List<Composition>();
        public TypeOfComposition()
        { name = string.Empty; }
        public string Name { get { return name; } set { name = value; } }
        public int Id { get { return id; } set { id = value; } }
        public List<Composition> Compositions { get {  return compositions; } set { compositions = value; } }
    }
}
