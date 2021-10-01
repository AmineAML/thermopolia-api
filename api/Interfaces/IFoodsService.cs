using System.Collections.Generic;
using System.Threading.Tasks;

namespace api.Interfaces
{
    public interface IFoodsService
    {
        Task<List<Recipe>> GetTenFoods();
        Task<Recipe> GetFoodById(string id);
    }
}