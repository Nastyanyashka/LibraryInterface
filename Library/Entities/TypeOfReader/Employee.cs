using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Entities.TypeOfReader.PropertiesOfTypes;
namespace Library.Entities.TypeOfReader
{
    public class Employee: Reader
    {
        int departmentId;
        int positionId;
        Department? department;
        Position? position;
        public Employee()
        {

        }
        public int DepartmentId { get { return departmentId; } set {  departmentId = value; } }
        public int PositionId { get { return positionId; } set {  positionId = value; } }
        public Department? Department { get {  return department; } set { department = value; } }
        public Position? Position { get { return position; } set { position = value; } }
    }
}
