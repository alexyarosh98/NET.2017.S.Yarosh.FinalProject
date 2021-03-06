﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using BLLInterface.Models;
using BLLInterface.Services;
using TJSystemWebUI.AuthProviders;
using TJSystemWebUI.Infastructure;
using TJSystemWebUI.Models;

namespace TJSystemWebUI.Controllers
{
    public class AccountController : Controller
    {
        private IUserService userService;
        public AccountController(IUserService _service)
        {
            userService = _service;
        }
            
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(UserEntity viewModel)
        {
            if (userService.GetUser(viewModel.Email) != null)
            {
                ModelState.AddModelError("","This email is already registered");
            }
            if (ModelState.IsValid)
            {
                var membershipUser = ((TJSMembershipProvider)Membership.Provider)
                    .CreateUser(viewModel);

                if (membershipUser != null)
                {
                    FormsAuthentication.SetAuthCookie(viewModel.Email, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Error registration.");
                }
            }
            return View(viewModel);
        }


        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
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
                {
                    FormsAuthentication.SetAuthCookie(viewModel.Email, viewModel.RememberMe);
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
        
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login", "Account");
        }

        [ChildActionOnly]
        public ActionResult LoginPartial()
        {
            return PartialView("_LoginPartial");
        }
        public ActionResult ViewAccount(string Email)
        {
            UserEntity user = userService.GetUser(Email);
            userService.GetAdditionalInfo(ref user);
            return View(user);
        }

        [AjaxRequestOnly]
        public ActionResult CheckUsersEmail(string email)
        {
            UserEntity user = userService.GetUser(email);
           return Json(user==null, JsonRequestBehavior.AllowGet);
           
        }

        [HttpPost]
        public ActionResult UpdateUser(UserEntity user)
        {
            userService.Update(user);
            return RedirectToAction("Users","Home");
        }

    }
}