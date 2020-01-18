using System;
using eimprovement.WebApplication.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eimprovement.WebApplication.Tests.Controllers
{
    [TestClass]
    public class APIRequestsTest
    {
        [TestMethod]
        public void TestMethod1_Scenario_ExpectedBehavior()
        {
            //Arrange

            //Act

            //Assert
        }

        [TestMethod]
        public void getPetsByStatusTests_GetActivePets()
        {
            //Arrange

            //Act
            var pets = APIRequests.getPetsByStatus("active");

            //Assert
            Assert.IsNotNull(pets);
        }

        [TestMethod]
        public void getPetByIdTests_existingId()
        {
            //Arrange

            //Act
            var pets = APIRequests.getPetById(55);

            //Assert
            Assert.IsNotNull(pets);
        }

        [TestMethod]
        public void updatePetTests()
        {
            //Arrange

            //Act
            var pets = APIRequests.updatePet(0,"asd","active");

            //Assert
            Assert.IsNotNull(pets);
        }

        [TestMethod]
        public void addPetTests()
        {
            //Arrange

            //Act
            var pets = APIRequests.addPet("asd", "active");

            //Assert
            Assert.IsNotNull(pets);
        }

        [TestMethod]
        public void deletePetTests()
        {
            //Arrange

            //Act
            var pets = APIRequests.deletePet(0);

            //Assert
            Assert.IsNotNull(pets);
        }
    }
}
