using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Interface;
using SportsStore.WebUI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.UnitTests.Controllers
{
    [TestClass]
    public class CategoriesTestClass
    {
        [TestMethod]
        public void Can_Create_Categories()
        {
            //init
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID=1, Name="P1", Category="Apples" },
                new Product {ProductID=2, Name="P2", Category="Apples" },
                new Product {ProductID=3, Name="P3", Category="Plums" },
                new Product {ProductID=4, Name="P4", Category="Oranges"},
            }.AsQueryable());

            NavController target = new NavController(mock.Object);

            //run
            string[] results = ((IEnumerable<string>)target.Menu().Model).ToArray();

            //done
            Assert.AreEqual(results.Length, 3);
            Assert.AreEqual(results[0], "Apples");
            Assert.AreEqual(results[1], "Oranges");
            Assert.AreEqual(results[2], "Plums");
        }

        [TestMethod]
        public void Indicates_Selected_Category()
        {
            //init
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID=1, Name="P1", Category="Apples" },
                new Product {ProductID=4, Name="P2", Category="Oranges"},
            }.AsQueryable());

            NavController target = new NavController(mock.Object);

            string categoryToSelect = "Apples";

            //run
            string result = target.Menu(categoryToSelect).ViewData["SelectedCategory"].ToString();

            //done
            Assert.AreEqual(categoryToSelect, result);
        }

    }
}
