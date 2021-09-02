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

        public RecipesController(ILogger<RecipesController> logger, IRecipesService recipesService)
        {
            _logger = logger;
            _recipesService = recipesService;
        }

        [HttpGet]
        public async Task<User[]> GetTenRecipes()
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

        [HttpGet("{id:int}")]
        public async Task<User> GetRecipeById(int id)
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
    }
}