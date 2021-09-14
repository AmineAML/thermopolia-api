using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using api.Models;
using Microsoft.Extensions.Configuration;

namespace api.Services
{
    public interface IRecipesService
    {
        Task<List<Recipe>> GetTenRecipes();
        Task<Recipe> GetRecipeById(string id);
    }

    public class RecipesService : IRecipesService
    {
        private HttpClient _httpClient;

        private string _appId;

        private string _appKey;

        private string _ingredientOfTheWeek;

        public IConfiguration Configuration { get; }

        public RecipesService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;

            Configuration = configuration;

            _appId = Configuration["RecipesSearchAPI:AppId"];

            _appKey = Configuration["RecipesSearchAPI:AppKey"];

            _ingredientOfTheWeek = "tuna";
        }

        public async Task<List<Recipe>> GetTenRecipes()
        {
            string APIURL = $"v2?type=public&q={_ingredientOfTheWeek}&app_id={_appId}&app_key={_appKey}&ingr=10&health=alcohol-free&health=pork-free&dishType=Main%20course&dishType=Salad&dishType=Sandwiches&dishType=Side%20dish&dishType=Soup&time=20&imageSize=REGULAR&random=true&field=uri&field=label&field=image&field=url&field=dietLabels&field=healthLabels&field=cautions&field=ingredientLines&field=ingredients&field=calories&field=totalTime&field=cuisineType&field=mealType&field=dishType&field=source";

            var res = await _httpClient.GetFromJsonAsync<RecipesSearchAPI>(APIURL);

            var recipes = res.hits.Select(h => h.recipe).ToList();//.ToArray();

            return recipes;
        }

        public async Task<Recipe> GetRecipeById(string id)
        {
            string APIURL = $"v2/{id}?type=public&app_id={_appId}&app_key={_appKey}";

            var res = await _httpClient.GetFromJsonAsync<RecipeData>(APIURL);

            var recipe = res.recipe;
            
            return recipe;
        }
    }
}