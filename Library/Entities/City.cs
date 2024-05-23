using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Entities
{
    public class City
    {
        string name;
        int id;
        List<PlaceOfPublication> placeOfPublications = new List<PlaceOfPublication>();
        public City()
        { name = string.Empty; }
        public string Name { get { return name; } set { name = value; } }
        public int Id { get { return id; } set { id = value; } }
        public List<PlaceOfPublication> PlaceOfPublications { get { return placeOfPublications; } set { placeOfPublications = value; } }
    }
}
