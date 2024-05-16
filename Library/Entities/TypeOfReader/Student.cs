using Library.Entities.TypeOfReader.PropertiesOfTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Entities.TypeOfReader
{
    public class Student : Reader
    {
        int facultyId;
        Faculty? faculty;
        string group;
        public Student()
        {
            group = string.Empty;
        }
        public string Group { get { return group; } set { group = value; } }
        public Faculty? Faculty {  get { return faculty; } set {  faculty = value; } }
        public int FacultyId { get { return facultyId; } set { facultyId = value; } }
    }
}
