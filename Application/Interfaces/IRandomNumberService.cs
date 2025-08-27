namespace ProvaPub.Application.Interfaces
{
    public interface IRandomNumberService
    {
        Task<int> GetUniqueRandomNumberAsync();
    }
}
