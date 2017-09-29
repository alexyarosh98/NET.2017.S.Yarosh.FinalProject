using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLLInterface.Models;
using BLLInterface.Services;
using TJSystemWebUI.Infastructure;

namespace TJSystemWebUI.Controllers
{
    [Authorize]
    public class TasksController : Controller
    {
        private ITaskService taskService;
        private IUserService userService;
        private ICategoryService categoryService;

        public TasksController(ITaskService _taskService,IUserService _userService,ICategoryService _categoryService)
        {
            taskService = _taskService;
            userService = _userService;
            categoryService = _categoryService;
        }
        // GET: Tasks
        public ActionResult AllTasks()
        {
            
            return View();
        }

        [ChildActionOnly]
        public PartialViewResult _RenderTaskList()
        {
            if (User.IsInRole("admin") || User.IsInRole("manager"))
            {
                return PartialView(taskService.AllTasksShortInfo().Reverse());
            }
            else return PartialView(taskService.GetUserTasks(User.Identity.Name));
        }

        // [AjaxRequestOnly]
        public ActionResult _RenderTaskFullInfo(TaskEntity taskEntity)
        {
            
           taskEntity= taskService.GetTaskFullInfo(taskEntity);
            return  PartialView("_RenderTaskFullInfo", taskEntity); //Json(taskEntity, JsonRequestBehavior.AllowGet); 
        }

        [HttpPost]
        public ActionResult AllTasksUpdate()
        {
            if (User.IsInRole("admin") || User.IsInRole("manager"))
            {
                return PartialView("_RenderTaskList", taskService.AllTasksShortInfo().Reverse());
            }
            else return PartialView("_RenderTaskList", taskService.GetUserTasks(User.Identity.Name).Reverse());
        }

        public ActionResult NewTask()
        {
            ViewBag.Categories = categoryService.GetAll().Select(c=>c.Name);
            return View();
        }

        [HttpPost]
        public ActionResult NewTask(TaskEntity task,string returnUrl)
        {
            if (task.Deadline <= DateTime.Now)
            {
                ModelState.AddModelError("","Enter date in the future");
            }
            if (ModelState.IsValid)
            {
                task.Status=Status.Awating;
                task.CreatorUser = userService.GetUser(User.Identity.Name);
                taskService.CreateTask(task);

                if (Url.IsLocalUrl(returnUrl)) return RedirectToRoute(returnUrl);
                return RedirectToAction("AllTasks");
            }
            return View();
        }
        
        
        //public ActionResult TaskInfo(TaskEntity task)
        //{
        //    return View(taskService.GetTaskFullInfo(task));
        //}
        [AjaxRequestOnly]
        public ActionResult _BecomeADeveloper(TaskEntity task)
        {
            task.Developer = userService.GetUser(User.Identity.Name);
            task.Status=Status.Developing;
            taskService.UpdateTask(task);
            return PartialView(task.Developer);
        }
        [AjaxRequestOnly]
        public void _DeleteTask(TaskEntity taskToDelete)
        {
            taskService.DeleteTask(taskToDelete);
            
        }

        public ActionResult _RenderTaskToUpdate(TaskEntity taskToUpdate)
        {
            return PartialView(taskToUpdate);
        }
        [HttpPost]
        public ActionResult _UpdateTask(TaskEntity updatedTask)
        {
            taskService.UpdateTask(updatedTask);

            return PartialView("_RenderTaskFullInfo", updatedTask);
        }
    }
}