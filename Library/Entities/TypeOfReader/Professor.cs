using Library.Entities.TypeOfReader.PropertiesOfTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Entities.TypeOfReader
{
    public class Professor : Reader
    {
        int departmentId;
        int positionId;
        int degreeId;
        int rankId;
        Department? department;
        Position? position;
        Degree? degree;
        Rank? rank;
        public Professor()
        {

        }
        public int DepartmentId { get { return departmentId; } set {  departmentId = value; } }
        public int PositionId { get { return positionId; } set {  positionId = value; } }
        public int DegreeId { get { return degreeId; } set {  degreeId = value; } }
        public int RankId { get { return rankId; } set {  rankId = value; } }
        public Department? Department { get {  return department; } set { department = value; } }
        public Position? Position { get { return position; } set { position = value; } }
        public Degree? Degree { get {  return degree; } set { degree = value; } }
        public Rank? Rank { get {  return rank; } set { rank = value; } }
    }
}
