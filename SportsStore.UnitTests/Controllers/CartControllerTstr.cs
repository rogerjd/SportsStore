using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Interface;
using SportsStore.WebUI.Controllers;
using SportsStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SportsStore.UnitTests.Controllers
{
    [TestClass]
    public class CartControllerTstr
    {
        [TestMethod]
        public void Can_Add_To_Cart()
        {
            //init
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID=1, Name="P1", Category="Apples" }
            }.AsQueryable());

            Cart cart = new Cart();

            CartController target = new CartController(mock.Object);

            //run
            target.AddToCart(cart, 1, null);

            //done
            Assert.AreEqual(cart.Lines.Count(), 1);
            Assert.AreEqual(cart.Lines.ToArray()[0].Product.ProductID, 1);
        }

        [TestMethod]
        public void Can_Remove_From_Cart()
        {
            //init
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID=1, Name="P1", Category="Apples" }
            }.AsQueryable());

            Cart cart = new Cart();

            CartController target = new CartController(mock.Object);

            //run
            target.AddToCart(cart, 1, null);
            Assert.AreEqual(cart.Lines.Count(), 1);
            target.RemoveFromCart(cart, 1, "");

            //done
            Assert.AreEqual(cart.Lines.Count(), 0);
        }

        [TestMethod]
        public void Adding_Product_To_Cart_Goes_To_Cart_Screen()
        {
            //init
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID=1, Name="P1", Category="Apples" }
            }.AsQueryable());

            Cart cart = new Cart();

            CartController target = new CartController(mock.Object);

            //run
            RedirectToRouteResult result = target.AddToCart(cart, 1, "myUrl");

            //done
            Assert.AreEqual(result.RouteValues["action"], "Index");
            Assert.AreEqual(result.RouteValues["returnUrl"], "myUrl");
        }

        [TestMethod]
        public void Can_View_Cart_Contents()
        {
            //init
            Cart cart = new Cart();
            CartController target = new CartController(null);

            //run
            CartIndexViewModel result = (CartIndexViewModel)target.Index(cart, "myUrl").ViewData.Model;

            //done
            Assert.AreEqual(result.Cart, cart);
            Assert.AreEqual(result.ReturnUrl, "myUrl");
        }
    }
}
