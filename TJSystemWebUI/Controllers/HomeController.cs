﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BLLInterface.Models;
using BLLInterface.Services;
using TJSystemWebUI.AuthProviders;

namespace TJSystemWebUI.Controllers
{
    public class HomeController : Controller
    {
        private IUserService userService;
      
        public HomeController(IUserService _userService)
        {
            userService = _userService;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [Authorize(Roles = "admin")]
        public ActionResult Users()
        {
            
            ViewBag.Roles = Roles.Provider.GetAllRoles();
            return View(userService.GetAllUserEntitiesShortInfo());
        }
    }
}