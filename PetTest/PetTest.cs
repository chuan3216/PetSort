using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PetLibrary;
using PetLibrary.Manager;
using PetLibrary.Model;
using PetLibrary.Repository;
using Moq;

namespace PetTest
{
    [TestClass]
    public class PetTest
    {        
        [TestMethod]
        public void TestLoadPetOwnersJson()
        {
            AppSettings settings = new AppSettings { BasePetUrl = "http://agl-developer-test.azurewebsites.net", PetJsonUrl = "people.json" };
            IPetRepository repository = new PetRepository(settings);
            string result = repository.LoadPetOwnersJson();
            Assert.IsNotNull(result);
        }
        
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestLoadPetOwnersJson_WrongUrl()
        {
            AppSettings settings = new AppSettings { BasePetUrl = "http://agl-developer-test.azurewebsites.net", PetJsonUrl = "nosuch.json" };
            IPetRepository repository = new PetRepository(settings);
            string result = repository.LoadPetOwnersJson();            
        }

        [TestMethod]
        public void TestGetPetOwners()
        {
            // mock json
            AppSettings settings = new AppSettings();
            Mock<PetRepository> mockRepository = new Mock<PetRepository>(settings);
            mockRepository.CallBase = true;
            mockRepository.Setup(p => p.LoadPetOwnersJson()).Returns("[{ 'name':'Bob','gender':'Male','age':23,'pets':[{'name':'Garfield','type':'Cat'},{'name':'Fido','type':'Dog'}]},{'name':'Jennifer','gender':'Female','age':18,'pets':[{'name':'Garfield','type':'Cat'}] }]");

            // test load pet owners            
            List<PetOwner> owners = mockRepository.Object.GetPetOwners();
            Assert.IsNotNull(owners);            
            Assert.AreEqual(2, owners.Count);
            Assert.AreEqual("Bob", owners[0].name);
            Assert.AreEqual("Male", owners[0].gender);
            Assert.AreEqual(23, owners[0].age);

            Assert.AreEqual(2, owners[0].pets.Count);
            Assert.AreEqual("Garfield", owners[0].pets[0].name);
            Assert.AreEqual("Cat", owners[0].pets[0].type);
            Assert.AreEqual("Fido", owners[0].pets[1].name);
            Assert.AreEqual("Dog", owners[0].pets[1].type);

            Assert.AreEqual("Jennifer", owners[1].name);
            Assert.AreEqual("Female", owners[1].gender);
            Assert.AreEqual(18, owners[1].age);

            Assert.AreEqual(1, owners[1].pets.Count);
            Assert.AreEqual("Garfield", owners[1].pets[0].name);
            Assert.AreEqual("Cat", owners[1].pets[0].type);
        }

        [TestMethod]
        public void TestGetCatsByOwnerGenderAndPetName()
        {
            // mock pet owners
            List<PetOwner> petOwners = new List<PetOwner>();
            petOwners.Add(new PetOwner{ name = "Bob", gender = "Male", age = 21, pets = new List<Pet> { new Pet { name = "Heathcliff", type = "Cat" }, new Pet { name = "Rocky", type = "Dog" } } });
            petOwners.Add(new PetOwner { name = "Jennifer", gender = "Female", age = 22, pets = new List<Pet> { new Pet { name = "Garfield", type = "Cat" }, new Pet { name = "Odie", type = "Dog" } } });
            petOwners.Add(new PetOwner { name = "Billy", gender = "Male", age = 23, pets = new List<Pet> { new Pet { name = "Wilson", type = "Cat" }, new Pet { name = "Lion", type = "Cat" } } });
            petOwners.Add(new PetOwner { name = "Rey", gender = "Female", age = 24, pets = new List<Pet> { new Pet { name = "Boots", type = "Cat" }, new Pet { name = "Dory", type = "Fish" } } });
            AppSettings settings = new AppSettings();
            Mock<IPetRepository> mockRepository = new Mock<IPetRepository>();
            mockRepository.CallBase = true;
            mockRepository.Setup(p => p.GetPetOwners()).Returns(petOwners);

            PetManager manager = new PetManager(mockRepository.Object);

            // test get cats
            List<KeyValuePair<string, string>> result = manager.GetCatsByOwnerGenderAndPetName();
            Assert.IsNotNull(result);
            Assert.AreEqual(5, result.Count);
            Assert.AreEqual("Male", result[0].Key);
            Assert.AreEqual("Heathcliff", result[0].Value);            
            Assert.AreEqual("Male", result[1].Key);
            Assert.AreEqual("Lion", result[1].Value);
            Assert.AreEqual("Male", result[2].Key);
            Assert.AreEqual("Wilson", result[2].Value);

            Assert.AreEqual("Female", result[3].Key);
            Assert.AreEqual("Boots", result[3].Value);
            Assert.AreEqual("Female", result[4].Key);
            Assert.AreEqual("Garfield", result[4].Value);
        }
    }
}
