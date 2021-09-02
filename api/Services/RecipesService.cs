using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace api.Services
{
    public interface IRecipesService
    {
        Task<User[]> GetTenRecipes();
        Task<User> GetRecipeById(int id);
    }

    public class RecipesService : IRecipesService
    {
        private HttpClient _httpClient;

        public RecipesService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<User[]> GetTenRecipes()
        {
            // var appid = ""

            // var appkey = ""

            string APIURL = "/users";
            var res = await _httpClient.GetFromJsonAsync<User[]>(APIURL);
            return res;
        }

        public async Task<User> GetRecipeById(int id)
        {
            // var appid = ""

            // var appkey = ""

            string APIURL = $"/users/{id}";
            var res = await _httpClient.GetFromJsonAsync<User>(APIURL);
            return res;
        }
    }
}