using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLLInterface.Models;

namespace BLLInterface.Services
{
    public interface ITaskService
    {
        void CreateTask(TaskEntity newTask);
        void DeleteTask(TaskEntity taskToDel);
        void UpdateTask(TaskEntity task);
        IEnumerable<TaskEntity> AllTasksShortInfo();
        TaskEntity GetTaskFullInfo(TaskEntity task);

    }
}
