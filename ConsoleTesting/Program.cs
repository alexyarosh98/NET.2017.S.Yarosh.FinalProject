using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLLInterface.Models;
using BLLInterface.Services;
using BLLLogic.ConcreteServices;
using DependencyResolver;
using Ninject;
using Ninject.Planning.Bindings.Resolvers;

namespace ConsoleTesting
{
    class Program
    {

        private static readonly IKernel resolver;
        static Program()
        {
            resolver=new StandardKernel();
            resolver.Configure();
        }
        static void Main(string[] args)
        {
            var userService = resolver.Get<IUserService>();
            var taskServiece = resolver.Get<ITaskService>();
            UserEntity user1=new UserEntity()
            {
                Nickname = "nick1",Possword = "nick1",Email = "nick1",Rating = 2.6,Role = Role.admin,Firstname = "nick1"
            };
            // userService.Register(user1);

            UserEntity user2 = new UserEntity()
            {
                Nickname = "nick2",
                Possword = "nick2",
                Email = "nick2",
                Role = Role.user,
               
            };
            //userService.Register(user2);

            UserEntity user3 = new UserEntity()
            {
                Nickname = "nick3",
                Possword = "nick3",
                Email = "nick3",
                Role = Role.user,
                Firstname = "nick3"

            };
            //userService.Register(user3);
            //userService.Unregister(user1);

            List<UserEntity> list =new List<UserEntity>(userService.GetAllUserEntitiesShortInfo());
            foreach (var i in list)
            {
                Console.WriteLine(i.Nickname+" and " + i.Firstname);
            }

          //  userService.GetAdditionalInfo(user3);
            Console.WriteLine(user3.Firstname);

            Console.WriteLine(userService.GetUser("nick3").Id);

            UserEntity userToUpdate = userService.GetUser("nick3");
            userToUpdate.Nickname = "nick3";
           // userService.Update(userToUpdate);










            TaskEntity task1 =new TaskEntity()
            {
                Title = "task1",
                Price = 1M,
                Category = "c#",
                Status = Status.awating,
                Description = "task1",
                Deadline = new DateTime(2020,2,2),
                Developer = userService.GetUser("nick3")
                ,CreatorUser = userService.GetUser("nick2")
            }; 
            
           // taskServiece.CreateTask(task1);

            TaskEntity task2 = new TaskEntity()
            {
                Title = "task2",
                Price = 2M,
                Category = "c#",
                Status = Status.awating,
                Description = "task2",
                Deadline = new DateTime(2020, 2, 2),
                CreatorUser = userService.GetUser("nick3")
                ,
                Developer = null
            };
           // taskServiece.CreateTask(task2);
           
           // DON"T WORK taskServiece.DeleteTask(task2);

            List<TaskEntity> taskList = new List<TaskEntity>(taskServiece.AllTasksShortInfo());

            foreach (var tasks in taskList)
            {
                Console.WriteLine(tasks.Title+" "+tasks.Category+ " "+tasks.Id);
            }

            TaskEntity t= taskServiece.GetTaskFullInfo(taskList.ElementAt(0));
            
            //taskServiece.DeleteTask(taskList.ElementAt(1));
            Console.WriteLine(t.Title + " "+t.Description+" "+t.Deadline);


            TaskEntity tasktoup = taskList.ElementAt(1);
            tasktoup=taskServiece.GetTaskFullInfo(tasktoup);
            Console.WriteLine(tasktoup.Deadline);
            tasktoup.Title = "TIIIIITLE";
            tasktoup.Description = "DEESCRIPTION";
            taskServiece.UpdateTask(tasktoup);

            Console.ReadKey();
        }
    }
}
