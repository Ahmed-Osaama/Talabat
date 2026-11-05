using Domain.Contracts;
using Domain.Entities.Basket;
using StackExchange.Redis;
using System.Text.Json;

namespace Persistence.Repositories
{
    public class BasketRepository(IConnectionMultiplexer _connection) : IBasketRepository
    {
        private readonly IDatabase _database = _connection.GetDatabase();
        public async Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket, TimeSpan? TimeToLive = null)
        {
            var serializedBasket = JsonSerializer.Serialize(basket);
            var result =  _database.StringSet(basket.Id, serializedBasket, TimeToLive ?? TimeSpan.FromDays(30));
            return result ? await GetBasketAsync(basket.Id) : null;
        }

        public async Task<bool> DeleteBasketAsync(string Id)
        => await _database.KeyDeleteAsync(Id);

        public async Task<CustomerBasket?> GetBasketAsync(string Id)
        {
            var result = await _database.StringGetAsync(Id);
            if (result.IsNullOrEmpty) return null;
            return JsonSerializer.Deserialize<CustomerBasket>(result!);
        }
    }
}
