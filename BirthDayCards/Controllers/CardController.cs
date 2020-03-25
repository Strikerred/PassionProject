using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using BirthDayCards.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BirthDayCards.Repositories;
using BirthDayCards.ResponseModel;
using Stripe;
using Microsoft.Extensions.Configuration;

namespace BirthDayCards.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class CardController : Controller
    {
        private BirthDayCard_dbContext _cardRepo;
        private IConfiguration _config;

        public object Configuration { get; private set; }

        public CardController(BirthDayCard_dbContext cardRepo, IConfiguration config)
        {
            _cardRepo = cardRepo;
            _config = config;
        }

        [HttpGet]
        public ActionResult<List<Template>> GetAll()
        {
            var cards = new CardRepo(_cardRepo);

            if (!cards.GetCards(out List<TemplateRM> response))
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<TemplateRM> Get(int id)
        {
            var card = new CardRepo(_cardRepo);

            if (!card.GetCard(id, out TemplateRM response))
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Charge([FromBody]PaymentRM paymentRM)
        {
            var claim = HttpContext.User.Claims.ElementAt(0);
            string userName = claim.Value;
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var purchase = await new CardRepo(_cardRepo, _config).Purchase(paymentRM, userName);

            if (!purchase.Item1)
            {
                return BadRequest(purchase.Item2);
            }

            return Ok(purchase.Item2);
        }
    }
}