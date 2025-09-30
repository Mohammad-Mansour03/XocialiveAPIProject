using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using MoalejilAcademy.Data;

namespace MoalejilAcademy.Repository
{
	public class GenericRepository<T> : IGenericRepository<T> where T : class
	{
		private readonly AppDbContext _context;
		private readonly DbSet<T> _dbSet;

		public GenericRepository(AppDbContext context)
		{
			_context = context;
			_dbSet = context.Set<T>();
		}

		public async Task<List<T>> GetAllAsync(Func<IQueryable<T>, IQueryable<T>>? include = null)
		{
			var query = _dbSet.AsQueryable();

			if (include != null)
				query = include(query);

			return await query.ToListAsync();
		}


		public async Task<T?> GetById(int id, Func<IQueryable<T>, IQueryable<T>>? include)
		{
			var query = _dbSet.AsQueryable();

			if (include != null)
				query = include(query);

			var item = await query.FirstOrDefaultAsync(x => EF.Property<int>(x, "Id") == id);

			return item;
		}

		public async Task<bool> AddAsync(T entity)
		{
			try
			{
				await _dbSet.AddAsync(entity);
				return true;
			}
			catch (Exception ex) 
			{
				return false;
			}
		}

		public  bool Remove(T entity)
		{
			try
			{
				_dbSet.Remove(entity);
				return true;
			}
			catch (Exception ex) 
			{
				return false;
			}
		}

		public  bool Update(T entity)
		{
			try
			{
				_dbSet.Update(entity);
				return true;
			}
			catch (Exception ex) 
			{
				return false;
			}
		}

		public async Task<T?> SelectAsync(Expression<Func<T, bool>> predicate)
		{
			var query = await _dbSet.Where(predicate).SingleOrDefaultAsync();

			return query;
		}

		public async Task<List<T>> SelectAllAsync(Expression<Func<T, bool>> predicate)
		{
			var query = await _dbSet.Where(predicate).ToListAsync();

			return query;
		}

		public async Task<bool> SaveAsync()
		{

			var result = await _context.SaveChangesAsync();

			return result > 0;
		}

		public IQueryable<T> ToIQuerable() 
		{
			return _context.Set<T>().AsQueryable();
		}

		public  DbContext GetContext()
		{
			return _context;
		}

		public async Task<List<T>> GetData(Expression<Func<T, bool>>? filter, Func<IQueryable<T>, 
			IOrderedQueryable<T>>? sort,int? skip = null, int? take = null)
		{

			var data = _context.Set<T>().AsQueryable();

			if (filter != null)
			{
				data = data.Where(filter);
			}

			if(sort != null)
			{
				data = sort(data);
			}

			if(skip != null)
			{
				data = data.Skip(skip.Value);
			}

			if (take != null) 
			{
				data = data.Take(take.Value);
			}

			return await data.ToListAsync();
		}
	}
}
