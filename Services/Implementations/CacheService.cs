using Domain.Contracts;
using ServicesAbstraction;

namespace Services.Implementations
{
    public class CacheService(ICacheRepository _cacheRepository) : ICacheService
    {
        public async Task<string?> GetCachedAsync(string Key)
            => await _cacheRepository.GetAsync(Key);

        public async Task SetCacheAsync(string Key, object Value, TimeSpan Duration)
            => await _cacheRepository.SetAsync(Key, Value, Duration);

    }
}
