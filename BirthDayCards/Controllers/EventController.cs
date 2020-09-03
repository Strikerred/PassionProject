using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BirthDayCards.Models;
using BirthDayCards.Repositories;
using BirthDayCards.ResponseModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BirthDayCards.Controllers
{
    [Route("api/auth/card/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class EventController : Controller
    {
        private BirthDayCard_dbContext _eventRepo;
        private IConfiguration _config;

        public EventController(BirthDayCard_dbContext eventRepo, IConfiguration config)
        {
            _eventRepo = eventRepo;
            _config = config;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<BdayPersonRM> BirthdayPerson([FromBody]BdayPersonRM bdayPersonRM)
        {
            var claim = HttpContext.User.Claims.ElementAt(0);
            string userName = claim.Value;
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var personEvent = new EventRepo(_eventRepo, _config);

            if (!personEvent.PersonEvent(bdayPersonRM, userName, out int response))
            {
                return NotFound();
            }

            return Ok(response);
        }
    }
}