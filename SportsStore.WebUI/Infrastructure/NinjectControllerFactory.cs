using System;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using SportsStore.Domain.Implementation;
using Moq;
using System.Collections.Generic;
using SportsStore.Domain.Entities;
using System.Linq;
using SportsStore.Domain.Implementation;
using SportsStore.Domain.Interface;
using System.Configuration;
using SportsStore.WebUI.Infrastructure.Interface;
using SportsStore.WebUI.Infrastructure.Implementation;

namespace SportsStore.WebUI.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        IKernel ninjectKernel;

        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext requestContext,
            Type controllerType)
        {
            return controllerType == null
                ? null
                : (IController)ninjectKernel.Get(controllerType);
        }

        private void AddBindings()
        {
            /*
                        Mock<IProductRepository> mock = new Mock<IProductRepository>();
                        mock.Setup(m => m.Products).Returns(new List<Product>
                        {
                            new Product {Name = "Football", Price= 25 },
                            new Product {Name = "Surf board", Price= 179 },
                            new Product {Name = "Running shoes", Price= 95},
                        }.AsQueryable());
            */
            ninjectKernel.Bind<IProductRepository>().To<EFProductRepository>();

            EmailSettings emailSettings = new EmailSettings
            {
                WriteAsFile = bool.Parse(ConfigurationManager.AppSettings["Email.WriteAsFile"] ?? "false")
            };
            ninjectKernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>().WithConstructorArgument("settings", emailSettings);

            ninjectKernel.Bind<IAuthProvider>().To<FormsAuthProvider>();
        }
    }
}