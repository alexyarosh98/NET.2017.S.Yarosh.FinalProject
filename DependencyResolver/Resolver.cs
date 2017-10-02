using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLLInterface;
using BLLInterface.Services;
using BLLLogic;
using BLLLogic.ConcreteServices;
using DALInterface.Repos;
using DALLogic;
using EF_ORM;
using Ninject;

namespace DependencyResolver
{
    public static class Resolver
    {
        public static void Configure(this IKernel kernel)
        {
            kernel.Bind<DbContext>().To<TJSystemContextContainer>();
            kernel.Bind<IUserRepository>().To<UserRepos>();
            kernel.Bind<ITaskRepository>().To<TaskRepos>();
            kernel.Bind<ILogger>().To<ConcreteLogger>();
            kernel.Bind<ICategoryRepository>().To<CategoryRepos>();
            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<ITaskService>().To<TaskService>();
            kernel.Bind<ICategoryService>().To<CategoryService>();
        }
    }
}
