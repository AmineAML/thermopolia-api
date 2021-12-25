using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using api.Models;
using Microsoft.AspNetCore.Http;
using api.Data;
using Microsoft.EntityFrameworkCore;
using api.Interfaces;
using api.Abstractions;
using System.Linq;
using Hangfire;

namespace api.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/[Controller]")]
    [ApiController]
    public class NewslettersController : ControllerBase
    {
        private readonly ILogger<RecipesController> _logger;

        private readonly DatabaseContext _context;

        private readonly IMailService _mailService;

        public readonly IFoodsService _foodsService;

        public readonly IDrinksService _drinksService;

        public readonly IDietService _dietService;

        public NewslettersController(ILogger<RecipesController> logger, DatabaseContext context, IMailService mailService, IFoodsService foodsService, IDrinksService drinksService, IDietService dietService)
        {
            _logger = logger;
            _context = context;
            _mailService = mailService;
            _foodsService = foodsService;
            _drinksService = drinksService;
            _dietService = dietService;
        }

        /// <summary>
        /// Subcribe to the newsletter
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        /// Post /subscribers
        /// {
        ///      "FullName": "Martian Mars",
        ///       // I don't own the solarsystem.space domain, I thought the name is cool and when I research it I've foudn that it was already bought
        ///      "Email": "martian.mars@solarsystem.space"
        /// }
        /// 
        /// </remarks>
        /// <param name="emailList"></param>
        /// <returns>New subscriber</returns>
        /// <response code="200">Returns the recipes list</response>
        /// <response code="409">If email is duplicate</response>
        /// <response code="500">If there was an error</response>
        [HttpPost("subscribers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> addNewSubscription(EmailList emailList)
        {
            _context.EmailLists.Add(emailList);

            try
            {
                await _context.SaveChangesAsync();

                UrlToken randomString = new UrlToken
                {
                    // GUIDs are good for testing, better create a function for generating a random string (no package)
                    RandomGeneratedString = Guid.NewGuid().ToString(),
                    Subscriber = emailList.ID
                };

                _context.UrlTokens.Add(randomString);

                await _context.SaveChangesAsync();

                MailRequest model = new MailRequest
                {
                    To = emailList.Email,
                    Subject = "Welcome, confirm your email address",
                    FullName = emailList.FullName,
                    Template = EmailTemplatesAbstractions.ConfirmEmail,
                    RandomString = randomString.RandomGeneratedString
                };

                await _mailService.SendEmail(model);

                return CreatedAtAction("GetEmailLists", new { id = emailList.ID }, emailList);
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                return Conflict();
            }
            catch (HttpRequestException httpRequestException)
            {
                Console.WriteLine(httpRequestException);
                return StatusCode(StatusCodes.Status500InternalServerError, httpRequestException);
            }
        }

        /// <summary>
        /// Subscribers list or subscriber
        /// </summary>
        /// <param name="id" required="false"></param>
        /// <returns>Subscribers' list or if id is provided then the requested subsriber</returns>
        /// <response code="200">Returns the subscribers list or the subscriber by their id if it was provided</response>
        /// <response code="404">If id not found</response>
        /// <response code="500">If there was an error</response>
        [HttpGet("subscribers/{id?}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetEmailLists(int? id)
        {
            try
            {
                if (id == null)
                {
                    return Ok(await _context.EmailLists.ToListAsync());
                }

                var newsletterSubscriber = await _context.EmailLists.FindAsync(id);

                if (newsletterSubscriber == null)
                {
                    return NotFound();
                }

                return Ok(newsletterSubscriber);
            }
            catch (HttpRequestException httpRequestException)
            {
                Console.WriteLine(httpRequestException);
                return StatusCode(StatusCodes.Status500InternalServerError, httpRequestException);
            }
        }

        /// <summary>
        /// Verify the subscriber's email
        /// </summary>
        /// <param name="token"></param>
        /// <returns>Email verification status</returns>
        /// <response code="200">Returns status of email verification</response>
        /// <response code="500">If there was an error</response>
        [HttpGet("subscribers/validation/{token}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ConfirmEmailSubscription(string token)
        {
            try
            {
                var validToken = _context.UrlTokens.Where(c => c.RandomGeneratedString == token).FirstOrDefault();

                if (validToken == null) return NotFound();

                var subscriber = await _context.EmailLists.FindAsync(validToken.Subscriber);

                if (subscriber == null) return NotFound();

                subscriber.IsVerified = true;

                _context.UrlTokens.Remove(validToken);

                await _context.SaveChangesAsync();

                await Newsletter(subscriber.Email);

                return Ok("Email verified!");
            }
            catch (HttpRequestException httpRequestException)
            {
                Console.WriteLine(httpRequestException);
                return StatusCode(StatusCodes.Status500InternalServerError, httpRequestException);
            }
        }

        private async Task Newsletter(string email)
        {
            List<Recipe> cachedRecipes = await _foodsService.GetTenFoods();

            List<Recipe> cachedDrinks = await _drinksService.GetTenDrinks();

            Diet cachedDiet = await _dietService.GetDiet();

            var content = new Newsletter
            {
                food = cachedRecipes[0],
                drink = cachedDrinks[0],
                diet = cachedDiet
            };

            MailRequest model = new MailRequest
            {
                To = email,
                Subject = "Your daily list of recommendations",
                Template = EmailTemplatesAbstractions.NewsletterEmail,
                Content = content
            };

            RecurringJob.AddOrUpdate($"{email} subscriber", () => _mailService.SendEmail(model), "0 0 * * *");
        }
    }
}