using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface IDietService
    {
        Task<Diet> GetDiet();
    }
}