using SportsStore.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers
{
    public class NavController : Controller
    {
        IProductRepository repository;

        public NavController(IProductRepository repos)
        {
            repository = repos;
        }

        // GET: Nav
        public PartialViewResult Menu(string category = null)
        {
            //            ViewData["SelectedCategory"] = category;
            ViewBag.SelectedCategory = category;

            IEnumerable<string> categories = repository.Products
                .Select(x => x.Category)
                .Distinct()
                .OrderBy(x => x);

            return PartialView(categories);
        }
    }
}