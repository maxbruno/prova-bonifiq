using ProvaPub.Models;

namespace ProvaPub.Services
{
	public interface IProductService : IBaseService<Product>
	{
		PagedList<Product> ListProducts(int page);
	}
}
