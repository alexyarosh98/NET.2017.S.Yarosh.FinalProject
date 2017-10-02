using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLLInterface;
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
        private readonly ILogger logger;

        public TaskService(ITaskRepository _repository,ILogger _logger)
        {
            taskRepository = _repository;
            logger = _logger;
        }
        public void CreateTask(TaskEntity newTask)
        {
            taskRepository.Create(newTask.ToDALTask());
            logger.Info($"Task {newTask.Title} created by {newTask.CreatorUser.Nickname}");
        }

        public void DeleteTask(TaskEntity taskToDel)
        {
            taskRepository.Delete(taskToDel.ToDALTask());
            logger.Info($"Task {taskToDel.Title} deleted by {taskToDel.CreatorUser.Nickname}");
        }

        public void UpdateTask(TaskEntity task)
        {
            taskRepository.Update(task.ToDALTask());

            logger.Info($"Task {task.Title} updated by {task.CreatorUser.Nickname}");
        }

        public IEnumerable<TaskEntity> AllTasksShortInfo()
        {
            return taskRepository.GetAllShortInfo().Select(i => i.ToBllEntity());
        }

        public TaskEntity GetTaskFullInfo(int taskId)
        {
            return taskRepository.GetFullInfo(taskId).ToBllEntity();
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
