using Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryInterface.Classes
{
    public class ExamplerForComboBox
    {
        int id;
        Exampler exampler;
        string fullInfo;
        int compositionId;
        public ExamplerForComboBox(Exampler exampler)
        {
            id = exampler.Id;
            this.exampler = exampler;
            fullInfo = "Инвентарный номер: " + exampler.Id + " Номер зала: " + exampler.StorageId + " Номер полки: " + exampler.NumberOfShelf + " Номер стеллажа: " + exampler.NumberOfRack; 
        }

        public int CompositionId {  get { return compositionId; } set { compositionId = value; } }
        public string FullInfo { get { return fullInfo; } }
        public int Id { get { return id; } set { id = value; } }
    }
}
