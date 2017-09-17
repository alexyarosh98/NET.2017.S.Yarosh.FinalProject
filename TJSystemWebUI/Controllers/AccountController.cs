using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BLLInterface.Models;
using BLLInterface.Services;
using TJSystemWebUI.Infastructure;
using TJSystemWebUI.Models;

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
            return View();
        }

        [HttpPost]
        public ActionResult Create(UserEntity newUser)
        {
            if (ModelState.IsValid)
            {
                userService.Register(newUser);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }


        public ActionResult Login()
        {            
            return View();
        }


        [HttpPost]
        public ActionResult Login(LogingUserModel model,string returnUrl)
        {
            UserEntity authUser;
            try
            {
                 authUser = userService.GetUser(model.Email);

            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Incorrect possword or email");

                return View();
            }

            if (authUser.Possword == model.Possword)
            {
                FormsAuthentication.SetAuthCookie(authUser.Email,false);
                return Redirect(returnUrl ?? Url.Action("Index","Home"));
            }
            else
            {
                ModelState.AddModelError("","Incorrect possword or email");

                return View();
            }
        }
        

        public ActionResult ViewAccount(UserEntity user)
        {
            userService.GetAdditionalInfo(user);
            return View(user);
        }
    }
}