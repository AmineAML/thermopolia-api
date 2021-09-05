using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using api.Models;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace api.Services
{
    public interface IDrinksService
    {
        Task<Recipe[]> GetTenDrinks();
        Task<Recipe> GetDrinkById(string id);
    }

    public class DrinksService : IDrinksService
    {
        private HttpClient _httpClient;

        private string _appId;

        private string _appKey;

        private string _ingredientOfTheWeek;

        public IConfiguration Configuration { get; }

        public DrinksService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;

            Configuration = configuration;

            _appId = Configuration["RecipesSearchAPI:AppId"];

            _appKey = Configuration["RecipesSearchAPI:AppKey"];

            _ingredientOfTheWeek = "orange";
        }

        public async Task<Recipe[]> GetTenDrinks()
        {
            string APIURL = $"v2?type=public&q={_ingredientOfTheWeek}&app_id={_appId}&app_key={_appKey}&ingr=10&health=alcohol-free&health=pork-free&dishType=Main%20course&dishType=Salad&dishType=Sandwiches&dishType=Side%20dish&dishType=Soup&time=20&imageSize=REGULAR&random=true&field=uri&field=label&field=image&field=url&field=dietLabels&field=healthLabels&field=cautions&field=ingredientLines&field=ingredients&field=calories&field=totalTime&field=cuisineType&field=mealType&field=dishType&field=source";

            var res = await _httpClient.GetFromJsonAsync<RecipesSearchAPI>(APIURL);

            var recipes = res.hits.Select(h => h.recipe).ToArray();

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