using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLLInterface.Models;
using BLLInterface.Services;
using BLLLogic.Mappers;
using DALInterface.Repos;

namespace BLLLogic.ConcreteServices
{
    public class CategoryService:ICategoryService
    {
        private ICategoryRepository categoryRepository;
        public CategoryService(ICategoryRepository _categoryRepository)
        {
            categoryRepository = _categoryRepository;
        }
        public IEnumerable<CategoryEntity> GetAll()
        {
            return categoryRepository.GetAll().Select(c => c.ToBllEntity());
        }
    }
}
