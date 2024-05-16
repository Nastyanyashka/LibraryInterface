using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Entities
{
    public class Author
    {
        //string fullName;
        int id;
        string firstName;
        string lastName;
        string? middleName;
        List<Composition> compositions = new List<Composition>();
        List<AuthorsAndCompositions> authorsAndCompositions = new List<AuthorsAndCompositions>();
        public Author()
        {
            //fullName = string.Empty;
            firstName = "";
            lastName = "";
            middleName = string.Empty;
        }
        //public string FullName { get { return fullName; } set { fullName = value; } }
        public string FirstName { get { return firstName; } set { firstName = value; } }
        public string LastName { get { return lastName; } set { lastName = value; } }
        public string MiddleName
        {
            get
            {
                if (middleName == null)
                { return ""; }
                return middleName;
            }
            set { middleName = value; }
        }
        public int Id { get { return id; } set { id = value; } }
        public List<Composition> Compositions { get {  return compositions; } set { compositions = value; } }
        public List<AuthorsAndCompositions> AuthorsAndCompositions { get { return authorsAndCompositions; } set { authorsAndCompositions = value; } }

    }
}
