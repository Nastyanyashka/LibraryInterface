﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Entities.TypeOfReader.PropertiesOfTypes
{
    public class Faculty
    {
        int id;
        string name;
        public Faculty()
        {
            name = string.Empty;
        }
        public int Id { get { return id; } set { id = value; } }
        public string Name { get { return name; } set { name = value; } }
    }
}
