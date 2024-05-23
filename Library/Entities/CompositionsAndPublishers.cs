using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Entities
{
    public class CompositionsAndPublishers
    {
        int id;
        int compostionId;
        int placeOfPublicationId;
        Composition? composition;
        PlaceOfPublication? placeOfPublication;
        public CompositionsAndPublishers()
        {

        }
        public int Id { get { return id; }  set { id = value; } }
        public int CompostionId { get {  return compostionId; } set {  compostionId = value; } }
        public int PlaceOfPublicationId { get { return placeOfPublicationId; } set {  placeOfPublicationId = value; } }
        public Composition? Composition { get { return composition; } set {  composition = value; } }
        public PlaceOfPublication? PlaceOfPublication { get {  return placeOfPublication; } set { placeOfPublication = value; } }

    }
}
