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
        int publishingHouseId;
        PublishingHouse publishingHouse = new PublishingHouse();
        List<CompositionsAndPublishers> compositionsAndPublishers = new List<CompositionsAndPublishers>();
        List<Composition> compositions = new List<Composition>();
        public PlaceOfPublication()
        {
            name = string.Empty;
        }
        public int Id {  get { return id; } set { id = value; } }
        public string Name { get { return name; } set { name = value; } }
        public PublishingHouse PublishingHouse { get { return publishingHouse; } set { publishingHouse = value; } }
        public List<Composition> Compositions { get { return compositions; } set { compositions = value; } }
        public List<CompositionsAndPublishers> CompositionsAndPublishers { get { return compositionsAndPublishers; } set { compositionsAndPublishers = value; } }
        public int PublishingHouseId { get {  return publishingHouseId; } set { publishingHouseId = value; } }
    }
}
