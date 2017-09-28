using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLLInterface.Models;
using DALInterface.DTO;

namespace BLLLogic.Mappers
{
    public static class CategoryEntityMapper
    {
        public static CategoryEntity ToBllEntity(this DALCategory category)
        {
            return new CategoryEntity()
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        public static DALCategory ToDallEntity(this CategoryEntity category)
        {
            return new DALCategory()
            {
                Id = category.Id,
                Name = category.Name
            };
        }
    }
}
