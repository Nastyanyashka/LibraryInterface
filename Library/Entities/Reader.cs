using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Library.ApplicationContext;

namespace Library.Entities
{
    public class Reader
    {
        int id;
        string firstName;
        string lastName;
        string? middleName;
        //string fullName;
        string readerTicket;
        DateTime registrationDate;
        DateTime? pereregistrationDate;
        //int categoryId;
        //Category? category;
        List<Exampler> examplers = new List<Exampler>();
        List<GivenExamplers> givenExamplers = new List<GivenExamplers>();
        List<Penalty> penaltys = new List<Penalty>();
        List<ReadersAndPenaltys> readersAndPenaltys = new List<ReadersAndPenaltys>();
        List<InterlibrarySubscription> interlibrarySubscription = new List<InterlibrarySubscription>();
        public Reader()
        {
            //fullName = string.Empty;
            firstName = "";
            lastName = "";
            middleName = string.Empty;
            readerTicket = string.Empty;
            registrationDate = new DateTime();
            pereregistrationDate = new DateTime();
            //category = new Category();
        }

        public int Id { get { return id; } set { id = value; } }
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
        //public Category Category { get { return category!; } set { category = value; } }
        //public int CategoryId { get { return categoryId; } set { categoryId = value; } }
        public string ReaderTicket { get { return readerTicket; } set { readerTicket = value; } }
        public DateTime RegistrationDate { get { return registrationDate; } set { registrationDate = value; } }
        public DateTime? PereregistrationDate { get { return pereregistrationDate; } set { pereregistrationDate = value; } }
        public List<Exampler> Examplers { get { return examplers; } set { examplers = value; } }
        public List<GivenExamplers> GivenExamplers { get { return givenExamplers; } set { givenExamplers = value; } }
        public List<Penalty> Penaltys { get { return penaltys; } set { penaltys = value; } }
        public List<ReadersAndPenaltys> ReadersAndPenaltys { get { return readersAndPenaltys; } set {  readersAndPenaltys = value; } }

        public List<InterlibrarySubscription> InterlibrarySubscriptions { get { return interlibrarySubscription; } set { interlibrarySubscription = value; } }
    } 
}
