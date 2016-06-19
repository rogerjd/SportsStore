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
using SportsStore.WebUI.Models;

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
            ProductListViewModel result = (ProductListViewModel)(controller.List(null, 1) as ViewResult).Model;
            Product[] prodArray = result.Products.ToArray();
            Assert.IsTrue(prodArray.Length == 3);
            Assert.AreEqual(prodArray[0].Name, "P1");

            //part
            result = (ProductListViewModel)(controller.List(null, 2) as ViewResult).Model;
            prodArray = result.Products.ToArray();
            Assert.IsTrue(prodArray.Length == 2);
            Assert.AreEqual(prodArray[0].Name, "P4");
            Assert.AreEqual(prodArray[1].Name, "P5");

            //empty
            result = (ProductListViewModel)(controller.List(null, 3) as ViewResult).Model;
            prodArray = result.Products.ToArray();
            Assert.IsTrue(prodArray.Length == 0);
        }

        [TestMethod]
        public void Can_Filter_Products()
        {
            //init
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID=1, Name="P1", Category= "Cat1" },
                new Product {ProductID=2, Name="P2", Category= "Cat2"},
                new Product {ProductID=3, Name="P3", Category= "Cat1"},
                new Product {ProductID=4, Name="P4", Category= "Cat2"},
                new Product {ProductID=5, Name="P5", Category= "Cat3"}
            }.AsQueryable());

            ProductController controller = new ProductController(mock.Object);

            //run
            Product[] result = ((ProductListViewModel) (controller.List("Cat2", 1) as ViewResult).Model).Products.ToArray();

            //done
            Assert.AreEqual(result.Length, 2);
            Assert.IsTrue(result[0].Name == "P2" && result[0].Category == "Cat2");
            Assert.IsTrue(result[1].Name == "P4" && result[1].Category == "Cat2");
        }

        [TestMethod]
        public void Generate_Category_Specific_Product_Count()
        {
            //init
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID=1, Name="P1", Category= "Cat1" },
                new Product {ProductID=2, Name="P2", Category= "Cat2"},
                new Product {ProductID=3, Name="P3", Category= "Cat1"},
                new Product {ProductID=4, Name="P4", Category= "Cat2"},
                new Product {ProductID=5, Name="P5", Category= "Cat3"}
            }.AsQueryable());

            ProductController target = new ProductController(mock.Object);
            target.PageSize = 3;

            //run
            int res1 = ((ProductListViewModel)(target.List("Cat1") as ViewResult).Model).PagingInfo.TotalItems;
            int res2 = ((ProductListViewModel)(target.List("Cat2") as ViewResult).Model).PagingInfo.TotalItems;
            int res3 = ((ProductListViewModel)(target.List("Cat3") as ViewResult).Model).PagingInfo.TotalItems;
            int resAll = ((ProductListViewModel)(target.List(null) as ViewResult).Model).PagingInfo.TotalItems;
            int resNone = ((ProductListViewModel)(target.List("xyz") as ViewResult).Model).PagingInfo.TotalItems;

            //done
            Assert.AreEqual(res1, 2);
            Assert.AreEqual(res2, 2);
            Assert.AreEqual(res3, 1);
            Assert.AreEqual(resAll, 5);
            Assert.AreEqual(resNone, 0);

        }
    }
}
