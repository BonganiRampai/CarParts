using CarParts.Data.DataAccess;
using CarParts.Models;
using System.Linq.Expressions;

namespace CarParts.Data
{
    public interface IRepositoryBase<T>
    {
        T GetById(int id);
        IEnumerable<T> FindAll();
        IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression);

        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
