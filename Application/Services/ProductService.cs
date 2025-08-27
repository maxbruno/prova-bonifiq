using Microsoft.EntityFrameworkCore;
using ProvaPub.Domain.Entities;
using ProvaPub.Repository;
using ProvaPub.Application.Interfaces;

namespace ProvaPub.Application.Services
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
