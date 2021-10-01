using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using api.Models;
using Microsoft.AspNetCore.Http;
using api.Abstractions;
using api.Interfaces;

namespace api.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/[Controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly ILogger<RecipesController> _logger;

        public readonly IFoodsService _foodsService;

        public readonly IDrinksService _drinksService;

        public readonly IDietService _dietService;

        private readonly ICacheService _cache;

        public RecipesController(ILogger<RecipesController> logger, IFoodsService foodsService, IDrinksService drinksService, IDietService dietService, ICacheService cache)
        {
            _logger = logger;
            _foodsService = foodsService;
            _drinksService = drinksService;
            _dietService = dietService;
            _cache = cache;
        }

        /// <summary>
        /// Random 10 foods recipes
        /// </summary>
        /// <returns>A list of 10 random foods recipes</returns>
        /// <response code="200">Returns the recipes list</response>
        /// <response code="500">If there was an error</response>
        [HttpGet("foods")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Recipe>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<Recipe>>> GetTenFoods()
        {
            try
            {
                var randomRecipes = await _foodsService.GetTenFoods();

                return Ok(_cache.Set<List<Recipe>>(CacheKeys.Recipes, randomRecipes));
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

        /// <summary>
        /// Food recipe by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A food recipe by id</returns>
        /// <response code="200">Returns the requested food recipe</response>
        /// <response code="500">If there was an error</response>
        [HttpGet("foods/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Recipe))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Recipe>> GetFoodById(string id)
        {
            try
            {
                return Ok(await _foodsService.GetFoodById(id));
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

        /// <summary>
        /// Random 10 drink recipes
        /// </summary>
        /// <returns>A list of 10 random drinks recipes</returns>
        /// <response code="200">Returns the recipes list</response>
        /// <response code="500">If there was an error</response>
        [HttpGet("drinks")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Recipe))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<Recipe>>> GetTenDrinks()
        {
            try
            {
                var drinks = await _drinksService.GetTenDrinks();

                return Ok(_cache.Set<List<Recipe>>(CacheKeys.Drinks, drinks));

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

        /// <summary>
        /// Drink recipe by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A drink recipe by id</returns>
        /// <response code="200">Returns the requested drink recipe</response>
        /// <response code="500">If there was an error</response>
        [HttpGet("drinks/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Recipe))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Recipe>> GetDrinksById(string id)
        {
            try
            {
                return Ok(await _drinksService.GetDrinkById(id));
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

        /// <summary>
        /// Random diet
        /// </summary>
        /// <returns>A diet</returns>
        /// <response code="200">Returns a diet</response>
        /// <response code="500">If there was an error</response>
        [HttpGet("diet")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Diet))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Diet>> GetDiet()
        {
            try
            {
                var diet = await _dietService.GetDiet();

                return Ok(_cache.Set<Diet>(CacheKeys.Diet, diet));
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