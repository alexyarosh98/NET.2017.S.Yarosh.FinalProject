using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLLInterface.Models;
using BLLInterface.Services;
using BLLLogic.Mappers;
using DALInterface.DTO;
using DALInterface.Repos;

namespace BLLLogic.ConcreteServices
{
    public class TaskService:ITaskService
    {
        private readonly ITaskRepository taskRepository;

        public TaskService(ITaskRepository _repository)
        {
            taskRepository = _repository;
        }
        public void CreateTask(TaskEntity newTask)
        {
            taskRepository.Create(newTask.ToDALTask());
        }

        public void DeleteTask(TaskEntity taskToDel)
        {
            taskRepository.Delete(taskToDel.ToDALTask());
        }

        public void UpdateTask(TaskEntity task)
        {
            taskRepository.Update(task.ToDALTask());
        }

        public IEnumerable<TaskEntity> AllTasksShortInfo()
        {
            return taskRepository.GetAllShortInfo().Select(i => i.ToBllEntity());
        }

        public TaskEntity GetTaskFullInfo(TaskEntity task)
        {
            return taskRepository.GetFullInfo(task.ToDALTask()).ToBllEntity();
        }

        public IEnumerable<TaskEntity> GetUserTasks(string email)
        {
            return taskRepository.GetUserTasks(email).Select(t => t.ToBllEntity());
        }

        public IEnumerable<TaskEntity> AllTasksFullInfo()
        {
            throw new NotImplementedException();
        }
    }
}
