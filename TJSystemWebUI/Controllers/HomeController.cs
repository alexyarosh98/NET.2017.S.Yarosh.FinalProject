using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
            ViewBag.Message = "TAsk Job System description page.";

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
        [ChildActionOnly]
        public ActionResult RenderCarousel()
        {
            int numberOfImages=0;
            DirectoryInfo imageDirectoryInfo = new DirectoryInfo(@"D:\TaskJobSystem\TJSystemWebUI\Images");
            if (imageDirectoryInfo.Exists)
            {
                numberOfImages=imageDirectoryInfo.GetFiles("*.jpg",SearchOption.AllDirectories).Length;
            }
            


            return PartialView("_RenderCarousel",numberOfImages/2+1);
        }
    }
}