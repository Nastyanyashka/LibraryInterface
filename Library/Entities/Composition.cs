using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Entities
{
    public class Composition
    {
        int id;
        string name;
        int year;
        int amountInLibrary;
        int typeId;
        TypeOfComposition? type = new TypeOfComposition();
        List<Exampler> examplers = new List<Exampler>();
        List<Author> authors = new List<Author>();
        List<AuthorsAndCompositions> authorsAndCompositions = new List<AuthorsAndCompositions>();
        List<CompositionsAndPublishers> compositionsAndPublishers = new List<CompositionsAndPublishers>();
        List<PlaceOfPublication> placeOfPublications = new List<PlaceOfPublication>();
        public Composition()
        {
            name = string.Empty;
        }
        public int Id {  get { return id; } set { id = value; } }
        public string Name { get { return name; } set { name = value; } }
        public int Year { get {  return year; } set { year = value; } }
        public int AmountInLibrary { get {  return amountInLibrary; } set {  amountInLibrary = value; } }
        public int TypeId { get { return typeId;} set { typeId = value; } }
        public TypeOfComposition Type { get {  return type!; } set { type = value; } }
        public List<Exampler> Examplers { get {  return examplers; } set { examplers = value; } }
        public List<Author> Authors { get {  return authors; } set { authors = value; } }
        public List<AuthorsAndCompositions> AuthorsAndCompositions { get {  return authorsAndCompositions; } set { authorsAndCompositions = value; } }
        public List<CompositionsAndPublishers> CompositionsAndPublishers { get {  return compositionsAndPublishers; } set {  compositionsAndPublishers = value; } }
        public List<PlaceOfPublication> PlaceOfPublications { get {  return placeOfPublications; } set {  placeOfPublications = value; } }
    }
}
