using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using eimprovement.WebApplication;
using eimprovement.WebApplication.Controllers;;
using System.Threading.Tasks;

namespace eimprovement.WebApplication.Tests.Controllers
{
    [TestClass]
    public class PetShopControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            PetShopController controller = new PetShopController();

            // Act
           Task<ActionResult> result = controller.Index() as Task<ActionResult>;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
