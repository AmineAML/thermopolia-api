using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Net.Http.Json;
using System;
using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;
using api.Services;
using api.Models;
using Microsoft.AspNetCore.Http;

namespace api.Controllers
{
    [ApiController]
    [Route("api/v1/[Controller]")]
    public class RecipesController : ControllerBase
    {
        private readonly ILogger<RecipesController> _logger;

        public readonly IRecipesService _recipesService;

        public readonly IDrinksService _drinksService;

        public readonly IDietService _dietService;

        public RecipesController(ILogger<RecipesController> logger, IRecipesService recipesService, IDrinksService drinksService, IDietService dietService)
        {
            _logger = logger;
            _recipesService = recipesService;
            _drinksService = drinksService;
            _dietService = dietService;
        }

        // Description: gets list of ten random recipes
        // Returns: list of recipes
        [HttpGet("foods")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Recipe>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<Recipe>>> GetTenRecipes()
        {
            try
            {
                var randomRecipes = _recipesService.GetTenRecipes();

                return Ok(await randomRecipes);

            }
            catch (HttpRequestException httpRequestException)
            {
                Console.WriteLine(httpRequestException);
                return StatusCode(StatusCodes.Status500InternalServerError, httpRequestException);
            }

            // if (!res.IsSuccessStatusCode)
            // {
            //     throw new Exception("Error");
            // }

            // var content = await res.Content
        }

        // Description: gets a recipes by its id
        // Param: "id" unique value of the recipe
        // Returns: recipe object
        [HttpGet("foods/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Recipe))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Recipe>> GetRecipeById(string id)
        {
            try
            {
                var recipe = _recipesService.GetRecipeById(id);

                return Ok(await recipe);
            }
            catch (HttpRequestException httpRequestException)
            {
                Console.WriteLine(httpRequestException);
                return StatusCode(StatusCodes.Status500InternalServerError, httpRequestException);
            }

            // if (!res.IsSuccessStatusCode)
            // {
            //     throw new Exception("Error");
            // }

            // var content = await res.Content
        }

        // Description: gets list of ten random drinks
        // Returns: list of recipes
        [HttpGet("drinks")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Recipe))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<Recipe>>> GetTenDrinks()
        {
            try
            {
                var drinks = _drinksService.GetTenDrinks();

                return Ok(await drinks);

            }
            catch (HttpRequestException httpRequestException)
            {
                Console.WriteLine(httpRequestException);
                return StatusCode(StatusCodes.Status500InternalServerError, httpRequestException);
            }

            // if (!res.IsSuccessStatusCode)
            // {
            //     throw new Exception("Error");
            // }

            // var content = await res.Content
        }

        // Description: gets a drink by its id
        // Param: "id" unique value of the drink
        // Returns: drink object
        [HttpGet("drinks/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Recipe))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Recipe>> GetDrinksById(string id)
        {
            try
            {
                var drink = _drinksService.GetDrinkById(id);

                return Ok(await drink);
            }
            catch (HttpRequestException httpRequestException)
            {
                Console.WriteLine(httpRequestException);
                return StatusCode(StatusCodes.Status500InternalServerError, httpRequestException);
            }

            // if (!res.IsSuccessStatusCode)
            // {
            //     throw new Exception("Error");
            // }

            // var content = await res.Content
        }

        // Description: gets a random diet
        // Returns: diet object
        [HttpGet("diet")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Diet))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Diet>> GetDiet()
        {
            try
            {
                var diet = _dietService.GetDiet();

                return Ok(await diet);
            }
            catch (HttpRequestException httpRequestException)
            {
                Console.WriteLine(httpRequestException);
                return StatusCode(StatusCodes.Status500InternalServerError, httpRequestException);
            }

            // if (!res.IsSuccessStatusCode)
            // {
            //     throw new Exception("Error");
            // }

            // var content = await res.Content
        }
    }
}