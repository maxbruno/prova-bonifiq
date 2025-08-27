using Microsoft.EntityFrameworkCore;
using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Services
{
	public class ProductService : BaseService<Product>, IProductService
	{
		public ProductService(TestDbContext ctx) : base(ctx)
		{
		}

		protected override DbSet<Product> GetDbSet()
		{
			return _ctx.Products;
		}

		public PagedList<Product> ListProducts(int page)
		{
			return GetPagedList(page);
		}
	}
}
