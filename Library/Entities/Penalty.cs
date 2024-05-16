using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Entities
{
    public class Penalty
    {
        int id;
        string name;
        int sum;
        int numOfSuspensionDays;   
        List<Reader> readers = new List<Reader>();
        List<ReadersAndPenaltys> readersAndPenaltys = new List<ReadersAndPenaltys> { };
        public Penalty() {
            name = string.Empty;
        }
        public int Id { get { return id; } set {  id = value; } }
        public string Name { get { return name; } set { name = value; } }
        public int Sum { get { return sum; } set { sum = value; } }
        public int NumOfSuspensionDays {  get { return numOfSuspensionDays; } set { numOfSuspensionDays = value;} }
        public List<Reader> Readers { get {  return readers; } set { readers = value; } }
        public List<ReadersAndPenaltys > ReadersAndPenaltys { get {  return readersAndPenaltys; } set { readersAndPenaltys = value; } }
    }
}
