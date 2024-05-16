using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Entities.TypeOfReader.PropertiesOfTypes
{
    public class Department
    {
        int id;
        string name;
        int facultyId;
        Faculty? faculty;
        public Department()
        {
            name = string.Empty;
        }
        public int Id { get { return id; } set { id = value; } }
        public string Name { get { return name; } set { name = value; } }
        public int FacultyId { get {  return facultyId; } set {  facultyId = value; } }
        public Faculty? Faculty { get {  return faculty; } set { faculty = value; } }
    }
}
