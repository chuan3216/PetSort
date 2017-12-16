using System;
using System.Collections.Generic;
using System.Text;
using PetLibrary.Model;

namespace PetLibrary.Manager
{
    public interface IPetManager
    {        
        List<KeyValuePair<string, string>> GetCatsByOwnerGenderAndPetName();
    }
}
