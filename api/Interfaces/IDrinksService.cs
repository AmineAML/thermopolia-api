using System.Collections.Generic;
using System.Threading.Tasks;

namespace api.Interfaces
{
    public interface IDrinksService
    {
        Task<List<Recipe>> GetTenDrinks();
        Task<Recipe> GetDrinkById(string id);
    }
}