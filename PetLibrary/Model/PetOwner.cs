using System;
using System.Collections.Generic;

namespace PetLibrary.Model
{
    public class PetOwner
    {
        private List<Pet> _pets { get; set; }

        public string name { get; set; }
        public string gender { get; set; }
        public int age { get; set; }

        public List<Pet> pets {
            get { return _pets; }
            set { _pets = value == null ? new List<Pet>() : value; }    // so list of pets won't be null
        }
    }
}
