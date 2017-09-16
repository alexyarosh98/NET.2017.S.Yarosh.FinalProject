using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLLInterface.Models;
using BLLInterface.Services;

namespace TJSystemWebUI.Controllers
{
    public class TasksController : Controller
    {
        private ITaskService taskService;

        public TasksController(ITaskService _taskService)
        {
            taskService = _taskService;
        }
        // GET: Tasks
        public ActionResult AllTasks()
        {
            
            return View(taskService.AllTasksShortInfo().Reverse());
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
                task.CreatorUser=new UserEntity(){Email = "admin",Nickname = "admin",Role = Role.admin,Possword = "admin"};
                taskService.CreateTask(task);

                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult TaskInfo(TaskEntity task)
        {
            return View(task);
        }
    }
}