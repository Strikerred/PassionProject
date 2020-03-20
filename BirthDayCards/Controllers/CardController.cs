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

namespace BirthDayCards.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class CardController : Controller
    {
        private BirthDayCard_dbContext _cardRepo;

        public CardController(BirthDayCard_dbContext cardRepo)
        {
            _cardRepo = cardRepo;
        }

        [HttpGet]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<List<Template>> GetAll()
        {
            var cards = new CardRepo(_cardRepo);

            if (!cards.GetCards(out List<TemplateRM> response))
            {
                return NotFound();
            }

            return Ok(response);
        }

        //[HttpPost]
        //public async Task<ActionResult<>>
    }
}