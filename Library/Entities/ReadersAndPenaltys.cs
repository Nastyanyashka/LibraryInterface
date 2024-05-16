using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Entities
{
    public class ReadersAndPenaltys
    {
        int id;
        int readerId;
        int penaltyId;
        Reader? reader;
        Penalty? penalty;
        DateTime dateOfGetting;
        DateTime dateOfEnding;
        public ReadersAndPenaltys() { }
        public int Id { get { return id; } set {  id = value; } }
        public int ReaderId { get { return readerId; } set { readerId = value; } }
        public int PenaltyId { get { return penaltyId; } set {  penaltyId = value; } }
        public Reader? Reader { get { return reader; } set { reader = value; } }
        public DateTime DateOfGetting { get {  return dateOfGetting; } set {  dateOfGetting = value; } }
        public DateTime DateOfEnding { get {  return dateOfEnding; }
            set { dateOfEnding = value; } }
        public Penalty? Penalty { get {  return penalty; } set { penalty = value; } }
    }
}
