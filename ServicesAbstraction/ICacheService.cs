namespace ServicesAbstraction
{
    public interface ICacheService
    {
        Task<string?> GetCachedAsync(string Key);
        Task SetCacheAsync(string Key, object Value, TimeSpan Duration);
    }
}
