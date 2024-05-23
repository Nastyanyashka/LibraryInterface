using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Entities
{
    public class PlaceOfPublication
    {
        int id;
        string name;
        int cityId;
        City? city = new City();
        List<CompositionsAndPublishers> compositionsAndPublishers = new List<CompositionsAndPublishers>();
        List<Composition> compositions = new List<Composition>();
        public PlaceOfPublication()
        {
            name = string.Empty;
        }
        public int CityId { get { return cityId; } set { cityId = value; } }
        public City City { get { return city!; } set { city = value; } }
        public int Id {  get { return id; } set { id = value; } }
        public string Name { get { return name; } set { name = value; } }
        public List<Composition> Compositions { get { return compositions; } set { compositions = value; } }
        public List<CompositionsAndPublishers> CompositionsAndPublishers { get { return compositionsAndPublishers; } set { compositionsAndPublishers = value; } }
    }
}
