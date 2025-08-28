using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace XocialiveProject.Repository
{
	public interface IGenericRepository<T> where T : class
	{
		Task<List<T>> GetAllAsync(Func<IQueryable<T> , IQueryable<T>>? include = null);
		Task<T?> GetById(int id , Func<IQueryable<T> , IQueryable<T>>? include = null);

		public Task<List<T>> GetAll(Func<IQueryable<T>, IQueryable<T>>? include = null);
		Task<bool> AddAsync(T entity);
		bool Remove(T entity);
		bool Update(T entity);

		public Task<List<T>> GetData(Expression<Func<T, bool>>? filter, 
			Func<IQueryable<T>, IOrderedQueryable<T>>? sort
			, int? skip = null, int? take = null);
		Task<T?> SelectAsync(Expression<Func<T , bool>> predicate);
		Task<List<T>> SelectAllAsync(Expression<Func<T , bool>> predicate);
		 IQueryable<T> ToIQuerable();
		DbContext GetContext();
		Task<bool> SaveAsync();

		
	}
}
