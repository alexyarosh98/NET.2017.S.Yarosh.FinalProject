using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DALInterface.DTO;

namespace DALInterface.Repos
{
    public interface IRepository<TEntity> where TEntity:IDALEntity
    {
       // IEnumerable<TEntity> GetAllFullInfo();
        IEnumerable<TEntity> GetAllShortInfo();
        IEnumerable<TEntity> FindByPredicate(Expression<Func<TEntity, bool>> func);
        TEntity GetAdditionalInfo(TEntity user);

        void Create(TEntity e);
        void Delete(TEntity e);
        void Update(TEntity entity);
    }
}
