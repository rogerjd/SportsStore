using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.WebUI.Controllers;
using SportsStore.WebUI.Infrastructure.Interface;
using SportsStore.WebUI.Models;
using System.Web.Mvc;

namespace SportsStore.UnitTests.Controllers
{
    [TestClass]
    public class AccountControllerTst
    {
        [TestMethod]
        public void Can_Login_With_Valid_Credentials()
        {
            //init
            Mock<IAuthProvider> mock = new Mock<IAuthProvider>();
            mock.Setup(m => m.Authenticate("admin", "secret")).Returns(true);

            LogOnViewModel model = new LogOnViewModel
            {
                UserName = "admin",
                Password = "secret"
            };

            AccountController target = new AccountController(mock.Object);

            //run
            ActionResult result = target.LogOn(model, "/MyUrl");

            //done
            Assert.IsInstanceOfType(result, typeof(RedirectResult));
            Assert.AreEqual("/MyUrl", ((RedirectResult)result).Url);
        }

        [TestMethod]
        public void Cannot_Login_With_Invalid_Credentials()
        {
            //init
            Mock<IAuthProvider> mock = new Mock<IAuthProvider>();
            mock.Setup(m => m.Authenticate("admin", "secret")).Returns(true);

            LogOnViewModel model = new LogOnViewModel
            {
                UserName = "admin",
                Password = "guess"
            };

            AccountController target = new AccountController(mock.Object);

            //run
            ActionResult result = target.LogOn(model, "/MyUrl");

            //done
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsFalse(((ViewResult)result).ViewData.ModelState.IsValid);

        }
    }
}
