using Flagsmith;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace _Flagsmith.Adapters
{
    public interface IFlagsmithAdapter
    {
        Task<bool> IsEnabledAsync(string flagName);
    }
    public class FlagsmithAdapter : IFlagsmithAdapter
    {
        private const string CACHE_KEY = "FEATURE_FLAG_";
        private TimeSpan CACHE_TIME = TimeSpan.FromSeconds(10);
        private readonly IMemoryCache _cache;
        private readonly FlagsmithClient _client;

        public FlagsmithAdapter(FlagsmithClient client,
            IMemoryCache cache)
        {
            _client = client;
            _cache = cache;
        }

        public async Task<bool> IsEnabledAsync(string flagName)
        {
            var key = $"{CACHE_KEY}{flagName}";
            var isActive = _cache.Get<bool?>(key);

            if (!isActive.HasValue)
            {
                isActive = await _client.HasFeatureFlag(flagName);
                _cache.Set(key, isActive.GetValueOrDefault(), CACHE_TIME);
            }

            return isActive.GetValueOrDefault();
        }
    }
}
