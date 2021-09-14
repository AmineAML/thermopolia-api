using System.Collections.Generic;
using System.Threading.Tasks;

namespace api.Interfaces
{
    public interface IRecipesService
    {
        Task<List<Recipe>> GetTenRecipes();
        Task<Recipe> GetRecipeById(string id);
    }
}