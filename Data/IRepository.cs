using System;
using System.Linq;
using System.Linq.Expressions;

namespace LibApp.Data
{
    public interface IRepository<T>
    {
        void Create(T entity);
        void Delete(T entity);
        IQueryable<T> FindAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        T FindObject(Expression<Func<T, bool>> expression);
        void Update(T entity);
    }
}