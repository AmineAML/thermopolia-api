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
using api.Data;
using Microsoft.EntityFrameworkCore;
using FluentEmail.Core;
using api.Interfaces;

namespace api.Controllers
{
    [ApiController]
    [Route("api/v1/[Controller]")]
    public class NewslettersController : ControllerBase
    {
        private readonly ILogger<RecipesController> _logger;

        private readonly DatabaseContext _context;

        private readonly IMailService _mailService;

        public NewslettersController(ILogger<RecipesController> logger, DatabaseContext context, IMailService mailService)
        {
            _logger = logger;
            _context = context;
            _mailService = mailService;
        }

        // Description: save new suscription
        // Returns: request status
        [HttpPost("subscribe")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult> addNewSubscription(EmailList emailList)
        {
            _context.EmailLists.Add(emailList);

            try
            {
                await _context.SaveChangesAsync();

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

        // Description: gets an email list subscriber by id or the whole list
        // Returns: subscriber object or an array
        [HttpGet("subscribers/{id?}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetEmailLists(int? id)
        {
            try
            {
                if (id == null)
                {
                    var newsletterSubscribers = await _context.EmailLists.ToListAsync();

                    return Ok(newsletterSubscribers);
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

        public async Task<IActionResult> SendSingleEmail(string email, string fullName, string subject, string template)
        {
            try
            {
                Console.WriteLine(email);
                MailRequest model = new MailRequest
                {
                    To = email,
                    Subject = subject,
                    FullName = fullName,
                    Template = template
                };


                await _mailService.SendEmail(model);

                return Ok();
            }
            catch (HttpRequestException httpRequestException)
            {
                Console.WriteLine(httpRequestException);
                return StatusCode(StatusCodes.Status500InternalServerError, httpRequestException);
            }
        }
    }
}