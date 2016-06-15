using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;
using System.Web.Mvc;

namespace SportsStore.UnitTests.Controllers
{
    [TestClass]
    public class ProductCtrl
    {
        [TestMethod]
        public void Can_Paginate()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID=1, Name="P1" },
                new Product {ProductID=2, Name="P2" },
                new Product {ProductID=3, Name="P3" },
                new Product {ProductID=4, Name="P4" },
                new Product {ProductID=5, Name="P5" }
            }.AsQueryable());

            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            //            IEnumerable<Product> result = (IEnumerable<Product>)controller.List(2).Model; //ref: had trouble here; had to change callee

            //full
            IEnumerable<Product> result = (IEnumerable<Product>)(controller.List(1) as ViewResult).Model;
            Product[] prodArray = result.ToArray();
            Assert.IsTrue(prodArray.Length == 3);
            Assert.AreEqual(prodArray[0].Name, "P1");

            //part
            result = (IEnumerable<Product>)(controller.List(2) as ViewResult).Model;
            prodArray = result.ToArray();
            Assert.IsTrue(prodArray.Length == 2);
            Assert.AreEqual(prodArray[0].Name, "P4");
            Assert.AreEqual(prodArray[1].Name, "P5");

            //empty
            result = (IEnumerable<Product>)(controller.List(3) as ViewResult).Model;
            prodArray = result.ToArray();
            Assert.IsTrue(prodArray.Length == 0);

        }
    }
}
