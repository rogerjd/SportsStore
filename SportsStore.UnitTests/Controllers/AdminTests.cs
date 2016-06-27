﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Interface;
using SportsStore.WebUI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SportsStore.UnitTests.Controllers
{
    [TestClass]
    public class AdminTests
    {
        [TestMethod]
        public void Index_Contains_All_Products()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID=1, Name="P1" },
                new Product {ProductID=2, Name="P2" },
                new Product {ProductID=3, Name="P3" }
            }.AsQueryable());

            AdminController target = new AdminController(mock.Object);

            Product[] prodArray = ((IEnumerable<Product>)target.Index().ViewData.Model).ToArray();
            Assert.IsTrue(prodArray.Length == 3);
            Assert.AreEqual(prodArray[0].Name, "P1");
            Assert.AreEqual(prodArray[1].Name, "P2");
            Assert.AreEqual(prodArray[2].Name, "P3");
        }

        [TestMethod]
        public void Can_Edit_Product()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID=1, Name="P1" },
                new Product {ProductID=2, Name="P2" },
                new Product {ProductID=3, Name="P3" }
            }.AsQueryable());

            AdminController target = new AdminController(mock.Object);

            Product p1 = target.Edit(1).ViewData.Model as Product;
            Product p2 = target.Edit(2).ViewData.Model as Product;
            Product p3 = target.Edit(3).ViewData.Model as Product;

            Assert.AreEqual(1, p1.ProductID);
            Assert.AreEqual(2, p2.ProductID);
            Assert.AreEqual(3, p3.ProductID);
        }

        [TestMethod]
        public void Cannot_Edit_Nonexistent_Product()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID=1, Name="P1" },
                new Product {ProductID=2, Name="P2" },
                new Product {ProductID=3, Name="P3" }
            }.AsQueryable());

            AdminController target = new AdminController(mock.Object);

            Product result = target.Edit(4).ViewData.Model as Product;

            Assert.IsNull(result);
        }

        [TestMethod]
        public void Can_Save_Valid_Changes()
        {
            //init
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            AdminController target = new AdminController(mock.Object);

            Product product = new Product { Name = "Test" };

            //run
            ActionResult result = target.Edit(product, null);

            //done
            Product p2 = new Product { Name = "p2" }; //must be same prod ref to pass
            mock.Verify(m => m.SaveProduct(product));
            Assert.IsNotInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Cannot_Save_Invalid_Changes()
        {
            //init
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            AdminController target = new AdminController(mock.Object);
            Product product = new Product { Name = "Test" };
            target.ModelState.AddModelError("error", "error");

            //run
            ActionResult result = target.Edit(product, null);

            //done
            mock.Verify(m => m.SaveProduct(It.IsAny<Product>()), Times.Never()); //must be same prod ref
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Can_Delete_Valid_Products()
        {
            //init
            Product prod = new Product { ProductID = 2, Name = "test" };

            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductID=1,Name="P1" },
                prod,
                new Product {ProductID=3,Name="P3" }
            }.AsQueryable());

            AdminController target = new AdminController(mock.Object);

            //run
            target.Delete(prod.ProductID);

            //done
            mock.Verify(m => m.DeleteProduct(prod));
            //note: nothing is actually deleted, but we only verify the func was called
            Assert.AreEqual(mock.Object.Products.ToArray().Length, 2);
        }

        [TestMethod]
        public void Cannot_Delete_Invalid_Products()
        {
            //init
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductID=1,Name="P1" },
                new Product {ProductID=2,Name="P2" },
                new Product {ProductID=3,Name="P3" }
            }.AsQueryable());

            AdminController target = new AdminController(mock.Object);

            //run
            target.Delete(100);

            //done
            mock.Verify(m => m.DeleteProduct(It.IsAny<Product>()), Times.Never());
        }
    }

}
