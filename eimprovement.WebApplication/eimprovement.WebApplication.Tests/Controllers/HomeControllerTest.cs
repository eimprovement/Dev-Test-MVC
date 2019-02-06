using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using eimprovement.WebApplication.Controllers;

namespace eimprovement.WebApplication.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
      
    }
}
