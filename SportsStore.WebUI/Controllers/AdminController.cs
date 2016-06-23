﻿using SportsStore.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers
{
    public class AdminController : Controller
    {
        IProductRepository repository;

        public AdminController(IProductRepository repos)
        {
            repository = repos;
        }

        public ViewResult Index()
        {
            return View(repository.Products);
        }
    }
}