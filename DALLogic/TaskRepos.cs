using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using DALInterface.DTO;
using DALInterface.Repos;
using EF_ORM;

namespace DALLogic
{
    public class TaskRepos:ITaskRepository
    {
        private readonly DbContext context;

        public TaskRepos(DbContext _context)
        {
            context = _context;
        }
        //public IEnumerable<DALTask> GetAllFullInfo()
        //{
        //    return context.Set<Task>().Include("TaskInfoes").Include("Categories")
        //        .Select(task => new DALTask()
        //        {
        //            Title = task.Title,
        //            Price = task.Price,
        //            Status = task.Status,
        //            Category = task.Category.Name,
        //            Description = task.TaskInfo.Description,
        //            Deadline = task.TaskInfo.Deadline,
        //            Implementation = task.TaskInfo.Implementation,
        //            CreatorUser = new DALUser()
        //            {
        //                Nickname = task.TaskInfo.CreatorUser.Nickname,
        //                Email = task.TaskInfo.CreatorUser.Email,
        //                Rating = task.TaskInfo.CreatorUser.Rating,
        //                Role = task.TaskInfo.CreatorUser.Role

        //            },
        //            Developer = new DALUser()
        //            {
        //                Nickname = task.TaskInfo.CreatorUser.Nickname,
        //                Email = task.TaskInfo.CreatorUser.Email,
        //                Rating = task.TaskInfo.CreatorUser.Rating,
        //                Role =  task.TaskInfo.CreatorUser.Role

        //            }

        //        });
        //}

        public IEnumerable<DALTask> GetAllShortInfo()
        {
            return context.Set<Task>()
                .Include("Categories")
                .Select(task => new DALTask()
                {
                    Id = task.TaskId,
                    Title = task.Title,
                    Price = task.Price,
                    Status =  task.Status,
                    Category = task.Category.Name
                });
        }


        public DALTask GetAdditionalInfo(DALTask user)
        {
            throw new NotImplementedException();
        }

        public void Create(DALTask task)
        {
            TaskInfo newTaskInfo=new TaskInfo()
            {
                Description = task.Description,
                Deadline = task.Deadline,
                Implementation = task.Implementation,
               // CreatorUser = context.Set<User>().FirstOrDefault(user=>user.Email==task.CreatorUser.Email),
                CreatorUserId = context.Set<User>().FirstOrDefault(user=>user.Email==task.CreatorUser.Email).UserId,
                Developer = task.Developer==null?null:context.Set<User>().FirstOrDefault(user => user.Email == task.Developer.Email)
                };
            Task newTask = new Task()
            {
                Title = task.Title,
                Price = task.Price,
                Status = task.Status,
                CategoryId = context.Set<Category>().FirstOrDefault(categ => categ.Name == task.Category).CategoryId,
               // Category = context.Set<Category>().FirstOrDefault(categ => categ.Name == task.Category),
                TaskInfo = newTaskInfo
            };
           // newTaskInfo.Task = newTask;

            context.Set<Task>().Add(newTask);
            context.Set<TaskInfo>().Add(newTaskInfo);

            context.SaveChanges();
        }

        public void Delete(DALTask task)
        {
            Task taskToDel = context.Set<Task>().FirstOrDefault(t=>t.TaskId==task.Id);


            if (ReferenceEquals(taskToDel,null)) throw new InvalidOperationException();
                context.Set<TaskInfo>().Remove(taskToDel.TaskInfo);
                context.Set<Task>().Remove(taskToDel);

            context.SaveChanges();
        }

        public void Update(DALTask taskToUp)
        {
            Task task = context.Set<Task>().Find(taskToUp.Id);
            //context.Entry(task).CurrentValues.SetValues(taskToUp);
            task.Title = taskToUp.Title;
            task.Category = context.Set<Category>().First(c=>c.Name==taskToUp.Category);
            task.Price = taskToUp.Price;
            task.Status = taskToUp.Status;
            
            TaskInfo taskInfo = task.TaskInfo;
            taskInfo.Developer = taskToUp.Developer==null?null:context.Set<User>().FirstOrDefault(u => u.UserId == taskToUp.Developer.Id);
            //context.Entry(taskInfo).CurrentValues.SetValues(taskToUp);
            taskInfo.Deadline = taskToUp.Deadline;
            taskInfo.Description = taskToUp.Description;
            taskInfo.Implementation = taskToUp.Implementation;
            

            context.SaveChanges();
        }

        public DALTask GetFullInfo(DALTask tasktoUp)
        {
            Task task = context.Set<Task>().Include("Category").Include("TaskInfo").FirstOrDefault(t => t.TaskId == tasktoUp.Id);
            if(ReferenceEquals(task,null)) throw new InvalidOperationException();

            return new DALTask()
            {
                Id = task.TaskId,
                Title = task.Title,
                Price = task.Price,
                Status = task.Status,
                Category = task.Category.Name,
                Deadline = task.TaskInfo.Deadline,
                Description = task.TaskInfo.Description,
                Implementation = task.TaskInfo.Implementation,
                CreatorUser = new DALUser()
                {
                    Id = task.TaskInfo.CreatorUser.UserId,
                    Nickname = task.TaskInfo.CreatorUser.Nickname,
                    Password = task.TaskInfo.CreatorUser.Password,
                    Email = task.TaskInfo.CreatorUser.Email,
                    Role = task.TaskInfo.CreatorUser.Role,
                    Rating = task.TaskInfo.CreatorUser.Rating
                },
                Developer = ReferenceEquals(task.TaskInfo.Developer,null)?null:new DALUser()
                {
                    Id = task.TaskInfo.Developer.UserId,
                    Nickname = task.TaskInfo.Developer.Nickname,
                    Password = task.TaskInfo.Developer.Password,
                    Email = task.TaskInfo.Developer.Email,
                    Role = task.TaskInfo.Developer.Role,
                    Rating = task.TaskInfo.Developer.Rating
                }


            };
        }

        public IEnumerable<DALTask> GetUserTasks(string email)
        {
            return context.Set<Task>()
                .Include("Categories")
                .Include("TaskInfoes")
                .Include("Users")
                .Where(task => task.TaskInfo.Developer.Email == email)
                .Select(task => new DALTask()
                {
                    Id = task.TaskId,
                    Title = task.Title,
                    Price = task.Price,
                    Status = task.Status,
                    Category = task.Category.Name
                });
        }

        public IEnumerable<DALTask> FindByPredicate(Expression<Func<DALTask, bool>> func)
        {
           throw new NotImplementedException();
        }
    }
}
