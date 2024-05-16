using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Entities
{
    public class Exampler
    {
        int id;
        int storageId;
        Storage? storage;
        Composition? composition;
        int compositionId;
        int numberOfShelf;
        int numberOfRack;
        List<Reader> readers = new List<Reader>();
        List<GivenExamplers> givenExamplers = new List<GivenExamplers>();
        public Exampler() { }
        public int Id { get { return id; } set { id = value; } }
        public int StorageId { get {  return storageId; } set {  storageId = value; } }
        public Storage? Storage { get { return storage; } set { storage = value; } }
        public Composition? Composition { get {  return composition; } set {  composition = value; } }
        public int CompositionId { get { return compositionId; } set {  compositionId = value; } }
        public int NumberOfShelf {  get { return numberOfShelf; } set {  numberOfShelf = value; } }
        public int NumberOfRack { get {  return numberOfRack; } set { numberOfRack = value; } }
        public List<Reader> Readers { get { return readers; } set { readers = value; } }
        public List<GivenExamplers> GivenExamplers { get { return givenExamplers; } set { givenExamplers = value; } }
    }
}
