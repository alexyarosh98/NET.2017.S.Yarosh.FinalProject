using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
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
            
           taskEntity= taskService.GetTaskFullInfo(taskEntity.Id);
            ViewBag.Users = userService.GetAllUserEntitiesShortInfo()
                .Where(u => u.Role == Role.user)
                .Select(u => new {Email = u.Email, Nickname = u.Nickname, Rating = u.Rating});
            return  PartialView("_RenderTaskFullInfo", taskEntity); //Json(taskEntity, JsonRequestBehavior.AllowGet); 
        }

     //   [HttpPost]
     [AjaxRequestOnly]
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
        public ActionResult NewTask(TaskEntity task)
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
                
                return RedirectToAction("AllTasks");
            }
            return View();
        }
        
        
        //public ActionResult TaskInfo(TaskEntity task)
        //{
        //    return View(taskService.GetTaskFullInfo(task));
        //}
       // [AjaxRequestOnly]
       [HttpPost]
        public ActionResult _BecomeADeveloper(TaskEntity task,string developerEmail)
        {
            //task.Developer = userService.GetUser(User.Identity.Name);
            task.Developer = userService.GetUser(developerEmail);
            task.Status=Status.Developing;
            taskService.UpdateTask(task);
            return RedirectToAction("AllTasks");
            // return PartialView(task.Developer);
        }
        [AjaxRequestOnly]
        public void _DeleteTask(TaskEntity taskToDelete)
        {
            taskService.DeleteTask(taskToDelete);
            
        }

        public ActionResult _RenderTaskToUpdate(TaskEntity taskToUpdate)
        {

            ViewBag.Categories = categoryService.GetAll().Select(c => c.Name);
            return PartialView(taskToUpdate);
        }
        [HttpPost]
        public ActionResult _UpdateTask(TaskEntity model,int oldTaskId)
        {
            TaskEntity oldTask = taskService.GetTaskFullInfo(oldTaskId);

            if (User.IsInRole("admin"))
            {
                
                oldTask.Title = model.Title;
                oldTask.Description = model.Description;
                oldTask.Price = model.Price;
                oldTask.Category = model.Category;
                if (model.Deadline != default(DateTime)) oldTask.Deadline = model.Deadline;

                taskService.UpdateTask(oldTask);
                return PartialView("_RenderTaskUpdatedInfo", oldTask);
            }

            oldTask.Implementation = model.Implementation;
            if(oldTask.Implementation==100) oldTask.Status=Status.Completed;
            if(oldTask.Implementation!=100 && oldTask.Status==Status.Completed) oldTask.Status=Status.Developing;
            taskService.UpdateTask(oldTask);

            return PartialView("_RenderTaskUpdatedInfo", oldTask);
            //return PartialView("_RenderTaskFullInfo", oldTask);


        }
        [AjaxRequestOnly]
        public ActionResult CheckDate(DateTime deadline)
        {
            return Json(deadline > DateTime.Now, JsonRequestBehavior.AllowGet);
        }
    }
}