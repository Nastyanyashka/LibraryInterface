using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Entities
{
    public class AuthorsAndCompositions
    {
        int id;
        int authorId;
        Author? author;
        int compositionId;
        Composition? composition;
        public AuthorsAndCompositions()
        {

        }
        public int Id { get { return id; } set { id = value; } }
        public int AuthorId { get { return authorId; } set {  authorId = value; } }
        public int CompositionId { get { return compositionId; } set {  compositionId = value; } }
        public Composition? Composition { get {  return composition; } set {  composition = value; } }
        public Author? Author { get { return author; } set { author = value; } }
    }
}
