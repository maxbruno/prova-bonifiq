using Microsoft.EntityFrameworkCore;
using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Services
{
	public abstract class BaseService<T> : IBaseService<T> where T : class
	{
		protected readonly TestDbContext _ctx;

		protected BaseService(TestDbContext ctx)
		{
			_ctx = ctx;
		}

		protected abstract DbSet<T> GetDbSet();

		public virtual PagedList<T> GetPagedList(int page, int pageSize = 10)
		{
			if (page <= 0) page = 1;
			if (pageSize <= 0) pageSize = 10;

			var dbSet = GetDbSet();
			var totalCount = dbSet.Count();
			var skip = (page - 1) * pageSize;
			
			var items = dbSet
				.Skip(skip)
				.Take(pageSize)
				.ToList();

			return new PagedList<T>
			{
				Items = items,
				TotalCount = totalCount,
				CurrentPage = page,
				PageSize = pageSize,
				HasNext = skip + pageSize < totalCount
			};
		}
	}
}
