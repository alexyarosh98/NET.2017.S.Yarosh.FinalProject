using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLLInterface.Models;
using BLLInterface.Services;

namespace TJSystemWebUI.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        private IUserService userService;
        public AccountController(IUserService _service)
        {
            userService = _service;
        }
        // GET: Account
        public ActionResult Create()
        {
            UserEntity newUser = new UserEntity();
            return View(newUser);
        }

        [HttpPost]
        public ActionResult Create(UserEntity newUser)
        {
            if (ModelState.IsValid)
            {
                userService.Register(newUser);
                return RedirectToAction("Index", "Home");
            }
            else return View();
        }
    }
}