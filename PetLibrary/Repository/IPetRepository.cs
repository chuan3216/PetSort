using System;
using System.Collections.Generic;
using System.Text;
using PetLibrary.Model;

namespace PetLibrary.Repository
{
    public interface IPetRepository
    {
        List<PetOwner> GetPetOwners();
        string LoadPetOwnersJson(); 
    }
}
