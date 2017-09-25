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

        public TasksController(ITaskService _taskService,IUserService _userService)
        {
            taskService = _taskService;
            userService = _userService;
        }
        // GET: Tasks
        [AllowAnonymous]
        public ActionResult AllTasks()
        {
            
            return View();
        }

        [ChildActionOnly]
        public PartialViewResult _RenderTaskList()
        {
            return PartialView(taskService.AllTasksShortInfo().Reverse());
        }

       // [AjaxRequestOnly]
        [AllowAnonymous]
        public ActionResult _RenderTaskFullInfo(TaskEntity taskEntity)
        {
            
           taskEntity= taskService.GetTaskFullInfo(taskEntity);
            return  PartialView("_RenderTaskFullInfo", taskEntity); //Json(taskEntity, JsonRequestBehavior.AllowGet); 
        }
        
        [HttpPost]
        [AllowAnonymous]
        public ActionResult AllTasksUpdate()
        {
            return PartialView("_RenderTaskList", taskService.AllTasksShortInfo().Reverse());
        }
        [Authorize]
        public ActionResult NewTask()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewTask(TaskEntity task,string returnUrl)
        {
            if (ModelState.IsValid)
            {
                task.Status=Status.Awating;
                task.CreatorUser = userService.GetUser(User.Identity.Name);
                taskService.CreateTask(task);

                return RedirectToRoute(returnUrl);
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
    }
}