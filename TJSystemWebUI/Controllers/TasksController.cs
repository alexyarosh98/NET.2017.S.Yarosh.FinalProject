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
        public ActionResult AllTasks()
        {
            
            return View();
        }

        [ChildActionOnly]
        public PartialViewResult _RenderTaskList()
        {
            return PartialView(taskService.AllTasksShortInfo().Reverse());
        }

        //[AcceptAjax]
        public ActionResult _RenderTaskFullInfo(TaskEntity taskEntity)
        {
            
           taskEntity= taskService.GetTaskFullInfo(taskEntity);
            return  PartialView("_RenderTaskFullInfo", taskEntity); //Json(taskEntity, JsonRequestBehavior.AllowGet); 
        }
        
        [HttpPost]
        public ActionResult AllTasksUpdate()
        {
            return PartialView("_RenderTaskList", taskService.AllTasksShortInfo().Reverse());
        }
        public ActionResult NewTask()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewTask(TaskEntity task)
        {
            if (ModelState.IsValid)
            {
                task.Status=Status.Awating;
                task.CreatorUser = userService.GetUser("admin");
                taskService.CreateTask(task);

                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        
        
        public ActionResult TaskInfo(TaskEntity task)
        {
            return View(taskService.GetTaskFullInfo(task));
        }
    }
}