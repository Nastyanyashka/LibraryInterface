using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace LibraryInterface.Classes
{
    internal class ButttonWInfo: Button
    {
        private int someId;
        public int SomeId { get { return someId; } set { someId = value; } }
    }
}
