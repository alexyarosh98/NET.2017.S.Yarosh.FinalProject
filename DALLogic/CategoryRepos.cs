using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALInterface.DTO;
using DALInterface.Repos;
using EF_ORM;

namespace DALLogic
{
    public class CategoryRepos:ICategoryRepository
    {
        private readonly DbContext context;

        public CategoryRepos(DbContext _context)
        {
            context = _context;
        }
        public IEnumerable<DALCategory> GetAll()
        {
            return context.Set<Category>()
                .Select(c => new DALCategory()
                {
                    Id = c.CategoryId,
                    Name = c.Name
                });
        }
    }
}
