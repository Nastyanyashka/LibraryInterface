using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Library.Entities
{
    public class GivenExamplers
    {
        int id;
        int examplerId;
        Exampler? exampler;
        int readerId;
        Reader? reader;
        DateTime dateOfIssue;
        DateTime? dateOfReturn;
        bool returned;
        public GivenExamplers() { }
        public int Id { get { return id; } set { id = value; } }
        public int ExamplerId { get {  return examplerId; } set { examplerId = value; } }
        public Exampler? Exampler { get {  return exampler; } set { exampler = value; } }
        public int ReaderId { get { return readerId; } set {  readerId = value; } }
        public Reader? Reader { get { return reader; } set { reader = value; } }
        public DateTime DateOfIssue { get {  return dateOfIssue; } set {  dateOfIssue = value; } }
        public DateTime? DateOfReturn { get {  return dateOfReturn; } set { dateOfReturn = value; } }
        public bool Returned { get { return returned; } set {  returned = value; } }
    }
}
