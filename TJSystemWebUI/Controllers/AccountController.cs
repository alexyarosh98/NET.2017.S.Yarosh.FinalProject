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


        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            var type = HttpContext.User.GetType();
            var iden = HttpContext.User.Identity.GetType();
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LogingUserModel viewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(viewModel.Email, viewModel.Password))
                    //Проверяет учетные данные пользователя и управляет параметрами пользователей
                {
                    FormsAuthentication.SetAuthCookie(viewModel.Email, viewModel.RememberMe);
                    //Управляет службами проверки подлинности с помощью форм для веб-приложений
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect login or password.");
                }
            }
            return View(viewModel);
        }







        //[HttpPost]
        //public ActionResult Login(LogingUserModel model,string returnUrl)
        //{
        //    UserEntity authUser;
        //    try
        //    {
        //         authUser = userService.GetUser(model.Email);

        //    }
        //    catch (Exception e)
        //    {
        //        ModelState.AddModelError("", "Incorrect possword or email");

        //        return View();
        //    }

        //    if (authUser.Possword == model.Possword)
        //    {
        //        FormsAuthentication.SetAuthCookie(authUser.Email,false);
        //        return Redirect(returnUrl ?? Url.Action("Index","Home"));
        //    }
        //    else
        //    {
        //        ModelState.AddModelError("","Incorrect possword or email");

        //        return View();
        //    }
        //}


        public ActionResult ViewAccount(string Email)
        {
            Debug.WriteLine("!!!!!!!!!" + Email.GetType());
            UserEntity user = userService.GetUser(Email);
            userService.GetAdditionalInfo(user);
            return View(user);
        }
    }
}