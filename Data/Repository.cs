using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace LibApp.Data
{
    public class Repository<T> : IRepository<T> where T : class
    {
		private readonly ApplicationDbContext dbContext;

		public Repository(ApplicationDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public IQueryable<T> FindAll()
		{
			return dbContext.Set<T>()
				.AsNoTracking();
		}

		public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
		{
			if (expression is null)
				return dbContext.Set<T>().AsNoTracking();

			return dbContext.Set<T>()
				.Where(expression)
				.AsNoTracking();
		}

		public T FindObject(Expression<Func<T, bool>> expression)
        {
			return dbContext.Set<T>()
				.FirstOrDefault(expression);
		}

		public void Create(T entity)
		{
			dbContext.Set<T>().Add(entity);
			dbContext.SaveChanges();
		}

		public void Update(T entity)
		{
			dbContext.Set<T>().Update(entity);
			dbContext.SaveChanges();
		}

		public void Delete(T entity)
		{
			dbContext.Set<T>().Remove(entity);
			dbContext.SaveChanges();
		}
	}
}