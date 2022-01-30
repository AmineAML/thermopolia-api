using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using api.Models;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Collections.Generic;
using api.Interfaces;
using api.Abstractions;

namespace api.Services
{
    public class DrinksService : IDrinksService
    {
        private HttpClient _httpClient;

        private string _appId;

        private string _appKey;

        private string _ingredientOfTheWeek;

        public IConfiguration Configuration { get; }

        private readonly ICacheService _cache;

        public DrinksService(HttpClient httpClient, IConfiguration configuration, ICacheService cache)
        {
            _httpClient = httpClient;

            Configuration = configuration;

            _appId = Configuration.GetValue<string>("RecipesSearchAPI:AppId");

            _appKey = Configuration.GetValue<string>("RecipesSearchAPI:AppKey");

            _ingredientOfTheWeek = "strawberry";

            _cache = cache;
        }

        public async Task<List<Recipe>> GetTenDrinks()
        {
            List<Recipe> cached = _cache.GetCachedRecipesOrDrinks<List<Recipe>>(CacheKeys.Drinks);

            if (cached != null) return cached;

            // For the week days return recipes with a specific ingredient

            // For the weekends days return recipes with a specific ingredient and from a specific cuisine

            string APIURL = $"v2?type=public&q={_ingredientOfTheWeek}&app_id={_appId}&app_key={_appKey}&ingr=10&health=alcohol-free&health=pork-free&dishType=Drinks&time=20&imageSize=REGULAR&random=true&field=uri&field=label&field=image&field=url&field=dietLabels&field=healthLabels&field=cautions&field=ingredientLines&field=ingredients&field=calories&field=totalTime&field=cuisineType&field=mealType&field=dishType&field=source";

            var res = await _httpClient.GetFromJsonAsync<RecipesSearchAPI>(APIURL);

            var recipes = res.hits.Select(h => h.recipe).ToList();//.ToArray();

            return recipes;
        }

        public async Task<Recipe> GetDrinkById(string id)
        {
            string APIURL = $"v2/{id}?type=public&app_id={_appId}&app_key={_appKey}";

            var res = await _httpClient.GetFromJsonAsync<RecipeData>(APIURL);

            var recipe = res.recipe;

            return recipe;
        }
    }
}