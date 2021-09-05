using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Net.Http.Json;
using System;
using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;
using api.Services;

namespace api.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class RecipesController : ControllerBase
    {
        private readonly ILogger<RecipesController> _logger;

        public readonly IRecipesService _recipesService;

        public readonly IDrinksService _drinksService;

        public RecipesController(ILogger<RecipesController> logger, IRecipesService recipesService, IDrinksService drinksService)
        {
            _logger = logger;
            _recipesService = recipesService;
            _drinksService = drinksService;
        }

        [HttpGet("foods")]
        public async Task<Recipe[]> GetTenRecipes()
        {
            try
            {
                var res = await _recipesService.GetTenRecipes();

                return res;

            }
            catch (HttpRequestException)
            {
                Console.WriteLine("Errro");
            }

            return null;

            // if (!res.IsSuccessStatusCode)
            // {
            //     throw new Exception("Error");
            // }

            // var content = await res.Content
        }

        [HttpGet("foods/{id}")]
        public async Task<Recipe> GetRecipeById(string id)
        {
            try
            {
                var res = await _recipesService.GetRecipeById(id);

                return res;
            }
            catch (HttpRequestException)
            {
                Console.WriteLine("Error");
            }

            return null;

            // if (!res.IsSuccessStatusCode)
            // {
            //     throw new Exception("Error");
            // }

            // var content = await res.Content
        }

        [HttpGet("drinks")]
        public async Task<Recipe[]> GetTenDrinks()
        {
            try
            {
                var res = await _drinksService.GetTenDrinks();

                return res;

            }
            catch (HttpRequestException)
            {
                Console.WriteLine("Errro");
            }

            return null;

            // if (!res.IsSuccessStatusCode)
            // {
            //     throw new Exception("Error");
            // }

            // var content = await res.Content
        }

        [HttpGet("drinks/{id}")]
        public async Task<Recipe> GetDrinksById(string id)
        {
            try
            {
                var res = await _drinksService.GetDrinkById(id);

                return res;
            }
            catch (HttpRequestException)
            {
                Console.WriteLine("Error");
            }

            return null;

            // if (!res.IsSuccessStatusCode)
            // {
            //     throw new Exception("Error");
            // }

            // var content = await res.Content
        }
    }
}