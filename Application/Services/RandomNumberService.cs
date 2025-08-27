using Microsoft.EntityFrameworkCore;
using ProvaPub.Domain.Entities;
using ProvaPub.Repository;
using ProvaPub.Application.Interfaces;

namespace ProvaPub.Application.Services
{
    public class RandomNumberService : IRandomNumberService
    {
        private readonly TestDbContext _context;
        private readonly Random _random;

        public RandomNumberService(TestDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _random = new Random();
        }

        public async Task<int> GetUniqueRandomNumberAsync()
        {
            int number;

            do
            {
                number = _random.Next(100);
            }
            while (await _context.Numbers.AnyAsync(n => n.Number == number));

            _context.Numbers.Add(new RandomNumber { Number = number });
            await _context.SaveChangesAsync();
            return number;
        }
    }
}
