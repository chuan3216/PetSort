using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using PetLibrary.Helper;
using PetLibrary.Model;
using PetLibrary.Repository;

namespace PetLibrary.Manager
{
    public class PetManager : IPetManager
    {        
        IPetRepository _repository;        
        public PetManager(IPetRepository repository)
        {
            _repository = repository;
        }
        
        public virtual List<KeyValuePair<string, string>> GetCatsByOwnerGenderAndPetName()
        {
            List<PetOwner> petOwners = _repository.GetPetOwners();
            List<KeyValuePair<string, string>> result = 
                (from o in petOwners
                from p in o.pets
                where p.type == "Cat"
                orderby o.gender descending, p.name
                select new KeyValuePair<string, string>(o.gender, p.name)).ToList();
            return result;             
        }    
    }
}
