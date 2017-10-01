﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALInterface.DTO;

namespace DALInterface.Repos
{
    public interface ITaskRepository:IRepository<DALTask>
    {
        DALTask GetFullInfo(int taskId);
        IEnumerable<DALTask> GetUserTasks(string email);
    }
}
