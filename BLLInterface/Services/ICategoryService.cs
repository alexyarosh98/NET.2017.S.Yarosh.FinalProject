using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLLInterface.Models;

namespace BLLInterface.Services
{
    public interface ICategoryService
    {
        IEnumerable<CategoryEntity> GetAll();
    }
}
