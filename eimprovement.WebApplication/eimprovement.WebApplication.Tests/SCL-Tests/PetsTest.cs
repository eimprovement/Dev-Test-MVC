using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using eimprovement.WebApplication.Models;

namespace eimprovement.WebApplication.Tests.SCL_Tests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class PetsTest
    {
        public PetsTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void Get_Pets_Available()
        {
            var pets = new SCL.Pets();
            List<Models.Pet> results = pets.GetAvailablePets();
            Assert.IsTrue(results.Any());
        }


        [TestMethod]
        public void Set_Pet_As_Sold_By_Id()
        {
            var pets = new SCL.Pets();
            var results = pets.SetFlagsPetAsSold(6598053714149417942);
            Assert.IsTrue(results);
        }



        [TestMethod]
        public void Add_New_Pet()
        {
            var pets = new SCL.Pets();
            var pUrls = new List<string>
            {
                "no pic"
            };
            var results = pets.AddNewPet(new Pet
            {
                category = new Category
                {
                    id = 0,
                    name = "category"
                },
                name = "Perrito",
                photoUrls = pUrls,
                status = "available",
                tags = new List<Tag>
                {
                    new Tag
                    {
                        id = 0,
                        name = "tag name"
                    }
                }

            });
            Assert.IsTrue(results.HasValue);
        }


        [TestMethod]
        public void Delete_Pet_By_id()
        {
            var pets = new SCL.Pets();
            var results = pets.DeletePet(6598053714149417942);
            Assert.IsTrue(results);
        }
    }
}
