using ProvaPub.Domain.Entities;

namespace ProvaPub.Application.Interfaces
{
    public interface IProductService : IBaseService<Product>
    {
        PagedList<Product> ListProducts(int page);
    }
}
