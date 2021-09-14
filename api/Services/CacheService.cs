using api.Models;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace api.Services
{
    public interface ICacheService
    {
        List<Recipe> GetCachedRecipesOrDrinks<T>(string key);

        Diet GetCachedDiet<T>(string key);

        T Set<T>(string key, T value);
    }

    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _cache;

        public CacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public List<Recipe> GetCachedRecipesOrDrinks<T>(string key)
        {
            var value = _cache.GetString(key);

            if (value != null)
            {
                return JsonConvert.DeserializeObject<List<Recipe>>(value);
            }

            return default;
        }

        public Diet GetCachedDiet<T>(string key)
        {
            var value = _cache.GetString(key);

            if (value != null)
            {
                return JsonConvert.DeserializeObject<Diet>(value);
            }

            return default;
        }

        public T Set<T>(string key, T value)
        {
            DateTime utcDate = DateTime.UtcNow;
            int hour = utcDate.Hour;
            int hoursPerDay = 24;
            int timeUntilNewDayInHours = hoursPerDay - hour;
            var options = new DistributedCacheEntryOptions
            {
                // between now and twelve midnight and set the expiration of cache with it
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(timeUntilNewDayInHours)
            };

            _cache.SetString(key, JsonConvert.SerializeObject(value), options);

            return value;
        }
    }
}