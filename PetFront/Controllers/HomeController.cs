using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PetLibrary;
using PetLibrary.Manager;
using PetSort.Models;
using System.Collections.Generic;
using System.Diagnostics;

namespace PetSort.Controllers
{
    public class HomeController : Controller
    {
        private AppSettings _settings;
        private IPetManager _manager;
        public HomeController(IOptions<AppSettings> settings, IPetManager manager)
        {
            _settings = settings.Value;
            _manager = manager;
        }

        public IActionResult Cats()
        {
            List<KeyValuePair<string, string>> petOwners = _manager.GetCatsByOwnerGenderAndPetName();
            return View(petOwners);
        }        

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
