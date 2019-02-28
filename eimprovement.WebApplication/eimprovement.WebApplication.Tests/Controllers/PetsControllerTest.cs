using eimprovement.WebApplication.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace eimprovement.WebApplication.Tests.Controllers
{
    [TestClass]
    public class PetsControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            PetsController controller = new PetsController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
