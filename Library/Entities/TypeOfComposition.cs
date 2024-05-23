using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Entities
{
    public class TypeOfComposition
    {
        string name;
        int id;
        int daysOfIssuance;
        bool permissionOnIssuance = true;
        int storageId;
        Storage? storage = new Storage();
        List<Composition> compositions = new List<Composition>();

        public TypeOfComposition()
        { name = string.Empty; }

        public string Name { get { return name; } set { name = value; } }
        public int Id { get { return id; } set { id = value; } }
       

        public int StorageId { get { return storageId; } set { storageId = value; } }
        public int DaysOfIssuance { get { return daysOfIssuance; } set { daysOfIssuance = value; } }
        public bool PermissionOnIssuance { get { return permissionOnIssuance; } set { permissionOnIssuance = value; } }
        public Storage Storage { get { return storage!; } set { storage = value; } }

        public List<Composition> Compositions { get {  return compositions; } set { compositions = value; } }
    }
}
