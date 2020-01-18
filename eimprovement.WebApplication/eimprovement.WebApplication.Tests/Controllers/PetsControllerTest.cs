using System;
using System.Web.Mvc;
using eimprovement.WebApplication.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eimprovement.WebApplication.Tests.Controllers
{
    [TestClass]
    public class PetsControllerTest
    {
        [TestMethod]
        public void PetList()
        {
            // Arrange
            PetsController controller = new PetsController();

            // Act
            ViewResult result = controller.PetList() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void AddPetModal()
        {
            // Arrange
            PetsController controller = new PetsController();

            // Act
            PartialViewResult result = controller.AddPetModal() as PartialViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void EditPetModal()
        {
            // Arrange
            PetsController controller = new PetsController();

            // Act
            PartialViewResult result = controller.EditPetModal(55) as PartialViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

    }
}
