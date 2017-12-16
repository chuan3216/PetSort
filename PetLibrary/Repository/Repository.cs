using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using PetLibrary.Helper;
using PetLibrary.Model;


namespace PetLibrary.Repository
{
    public class PetRepository : IPetRepository
    {
        private AppSettings _settings;
        public PetRepository(AppSettings settings)
        {
            _settings = settings;
        }

        public virtual List<PetOwner> GetPetOwners()
        {
            string jsonString = LoadPetOwnersJson();
            List<PetOwner> owners = JsonConvert.DeserializeObject<List<PetOwner>>(jsonString);
            return owners;
        }

        public virtual string LoadPetOwnersJson()
        {
            HttpHelper helper = new HttpHelper();
            string result = helper.ConsumeGet(_settings.BasePetUrl, _settings.PetJsonUrl);
            return result;
        }        
    }
}
