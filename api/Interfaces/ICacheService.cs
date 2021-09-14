using System.Collections.Generic;
using api.Models;

namespace api.Interfaces
{
    public interface ICacheService
    {
        List<Recipe> GetCachedRecipesOrDrinks<T>(string key);

        Diet GetCachedDiet<T>(string key);

        T Set<T>(string key, T value);
    }
}