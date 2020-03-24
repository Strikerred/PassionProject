using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using BirthDayCards.Models;
using BirthDayCards.ResponseModel;
using BirthDayCards.service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace BirthDayCards.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class EmailSenderController : Controller
    {
        private BirthDayCard_dbContext _cardRepo;
        private EmailSettings _emailSettings;
        private IConfiguration _config;

        public object Configuration { get; private set; }

        public EmailSenderController(BirthDayCard_dbContext cardRepo, IOptions<EmailSettings> emailSettings, IConfiguration config)
        {
            _cardRepo = cardRepo;
            _emailSettings = emailSettings.Value;
            _config = config;
        }

        [HttpPost("{templateId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> SendInvitations(int templateId, [FromBody]RecipientsRM recipientsRM)
        {

            var recipients = await new EmailHelper(_config, _cardRepo).SendEmails(templateId, recipientsRM.recipients, recipientsRM.BdayId);

            if (!recipients.Item1)
            {
                return BadRequest(recipients.Item2);
            }

            return Ok(recipients.Item2);
        }
    }
}