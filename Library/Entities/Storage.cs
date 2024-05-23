using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Entities
{
    public class Storage
    {
        int id;
        string type;
        List<Exampler> examplers = new List<Exampler>();
        List<TypeOfComposition> typesOfCompositions = new List<TypeOfComposition>();
        public Storage() {
        type = string.Empty;
        }
        public int Id { get { return id; } set { id = value; } }
        public string Type { get { return type; } set { type = value; } }
        public List<Exampler> Examplers { get {  return examplers; } set { examplers = value; } }
        public List<TypeOfComposition> TypesOfCompositions { get { return typesOfCompositions; } set { typesOfCompositions = value; } }
    }
}
