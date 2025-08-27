using ProvaPub.Models;

namespace ProvaPub.Services
{
	public interface IBaseService<T>
	{
		PagedList<T> GetPagedList(int page, int pageSize = 10);
	}
}
