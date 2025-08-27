using ProvaPub.Domain.Entities;

namespace ProvaPub.Application.Interfaces
{
    public interface IBaseService<T>
    {
        PagedList<T> GetPagedList(int page, int pageSize = 10);
    }
}
