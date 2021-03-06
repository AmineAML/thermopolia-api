using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using api.Abstractions;
using api.Interfaces;
using api.Models;
using Microsoft.Extensions.Configuration;

namespace api.Services
{
    public class FoodsService : IFoodsService
    {
        private HttpClient _httpClient;

        private string _appId;

        private string _appKey;

        private string _ingredientOfTheWeek;

        public IConfiguration Configuration { get; }

        private readonly ICacheService _cache;

        public FoodsService(HttpClient httpClient, IConfiguration configuration, ICacheService cache)
        {
            _httpClient = httpClient;

            Configuration = configuration;

            _appId = Configuration.GetValue<string>("RecipesSearchAPI:AppId");

            _appKey = Configuration.GetValue<string>("RecipesSearchAPI:AppKey");

            _ingredientOfTheWeek = "chicken";

            _cache = cache;
        }

        public async Task<List<Recipe>> GetTenFoods()
        {
            List<Recipe> cached = _cache.GetCachedRecipesOrDrinks<List<Recipe>>(CacheKeys.Recipes);

            if (cached != null) return cached;

            // For the week days return recipes with a specific ingredient

            // For the weekends days return recipes with a specific ingredient and from a specific cuisine

            string APIURL = $"v2?type=public&q={_ingredientOfTheWeek}&app_id={_appId}&app_key={_appKey}&ingr=10&health=alcohol-free&health=pork-free&dishType=Main%20course&dishType=Salad&dishType=Sandwiches&dishType=Side%20dish&dishType=Soup&time=20&imageSize=REGULAR&random=true&field=uri&field=label&field=image&field=url&field=dietLabels&field=healthLabels&field=cautions&field=ingredientLines&field=ingredients&field=calories&field=totalTime&field=cuisineType&field=mealType&field=dishType&field=source";

            var res = await _httpClient.GetFromJsonAsync<RecipesSearchAPI>(APIURL);

            var recipes = res.hits.Select(h => h.recipe).ToList();//.ToArray();

            return recipes;
        }

        public async Task<Recipe> GetFoodById(string id)
        {
            string APIURL = $"v2/{id}?type=public&app_id={_appId}&app_key={_appKey}";

            var res = await _httpClient.GetFromJsonAsync<RecipeData>(APIURL);

            var recipe = res.recipe;

            return recipe;
        }
    }
}