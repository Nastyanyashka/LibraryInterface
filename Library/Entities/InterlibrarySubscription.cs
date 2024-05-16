using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Entities
{
    public class InterlibrarySubscription
    {
        DateTime arrivalDate;
        DateTime dateOfOrder;
        string name;
        string nameOfLibrary;
        int id;
        int readerId;
        Reader? reader;
        public InterlibrarySubscription()
        {
            name = string.Empty;
            nameOfLibrary = string.Empty;
        }
        public DateTime ArrivalDate { get { return arrivalDate; } set { arrivalDate = value; } }
        public DateTime DateOfOrder { get {  return dateOfOrder; } set {  dateOfOrder = value; } }
        public string Name { get { return name; } set { name = value; } }
        public string NameOfLibrary { get {  return nameOfLibrary; } set { nameOfLibrary = value; } }
        public int Id { get { return id; } set { id = value; } }
        public Reader? Reader { get { return reader; } set { reader = value; } }
        public int ReaderId { get { return readerId; } set {  readerId = value; } }

    }
}
