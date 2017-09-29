using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALInterface.DTO;

namespace DALInterface.Repos
{
    public interface ITaskRepository:IRepository<DALTask>
    {
        DALTask GetFullInfo(DALTask task);
        IEnumerable<DALTask> GetUserTasks(string email);
    }
}
