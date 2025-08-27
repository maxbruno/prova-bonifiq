using Microsoft.EntityFrameworkCore;
using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Services
{
	public class RandomService
	{
        TestDbContext _ctx;
        private readonly Random _random;
		public RandomService()
        {
            var contextOptions = new DbContextOptionsBuilder<TestDbContext>()
    .UseInMemoryDatabase("TesteInMemory")
    .Options;
            _random = new Random();

            _ctx = new TestDbContext(contextOptions);
            _ctx.Database.EnsureCreated();
        }
        public async Task<int> GetRandom()
		{
            int number;
            
            do
            {
                number = _random.Next(100);
            }
            while (await _ctx.Numbers.AnyAsync(n => n.Number == number));

            _ctx.Numbers.Add(new RandomNumber() { Number = number });
            await _ctx.SaveChangesAsync();
			return number;
		}

	}
}
