using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Entities
{
    public class PublishingHouse
    {
        int id;
        string name;
        int daysOfIssuance;
        bool permissionOnIssuance = true;
        List<CompositionsAndPublishers> compositionsAndPublishers = new List<CompositionsAndPublishers>();
        List<PlaceOfPublication> placeOfPublications = new List<PlaceOfPublication>();
        public PublishingHouse()
        {
            name = string.Empty;
        }
        public int Id { get { return id; } set { id = value; } }
        public string Name { get { return name; } set { name = value; } }
        public int DaysOfIssuance { get {  return daysOfIssuance; } set {  daysOfIssuance = value; } }
        public bool PermissionOnIssuance { get { return permissionOnIssuance; } set {  permissionOnIssuance = value; } }
        public List<PlaceOfPublication> PlaceOfPublications { get { return placeOfPublications; } set { placeOfPublications = value; } }
        public List<CompositionsAndPublishers> CompositionsAndPublishers { get {  return compositionsAndPublishers; }set { compositionsAndPublishers = value; } }
    }
}
