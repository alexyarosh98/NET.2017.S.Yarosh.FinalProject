using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLLInterface.Models;
using DALInterface.DTO;

namespace BLLLogic.Mappers
{
    public static class TaskEntityMapper
    {
        public static TaskEntity ToBllEntity(this DALTask task)
        {
            return new TaskEntity()
            {
                Id = task.Id,
                Title = task.Title,
                Price = task.Price,
                Status = (Status)task.Status,
                Category = task.Category,
                Description = task.Description,
                Deadline = task.Deadline,
                Implementation = task.Implementation,
                CreatorUser = task.CreatorUser == null ? null : task.CreatorUser.ToBllUser(),
                Developer = task.Developer == null ? null : task.Developer.ToBllUser()
            };
        }
        public static DALTask ToDALTask(this TaskEntity task)
        {
            return new DALTask()
            {
                Id = task.Id,
                Title = task.Title,
                Price = task.Price,
                Status = (byte)task.Status,
                Category = task.Category,
                Description = task.Description,
                Deadline = task.Deadline,
                Implementation = task.Implementation,
                CreatorUser = task.CreatorUser == null ? null : task.CreatorUser.ToDalUser(),
                Developer = task.Developer==null?null:task.Developer.ToDalUser()
            };
        }
    }
}
