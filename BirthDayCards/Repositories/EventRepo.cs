using BirthDayCards.Models;
using BirthDayCards.ResponseModel;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BirthDayCards.Repositories
{
    public class EventRepo
    {
        private BirthDayCard_dbContext _context;
        private IConfiguration _config;

        public EventRepo(BirthDayCard_dbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public bool PersonEvent(BdayPersonRM bdayPersonRM, string userName, out int response)
        {
            try
            {
                BdayPerson bdayPerson = new BdayPerson()
                {
                    BdFirstName = bdayPersonRM.BdFirstName,
                    BdLastName = bdayPersonRM.BdLastName,
                    BdComingAge = bdayPersonRM.BdComingAge,
                    UserName = userName
                };

                _context.BdayPerson.Add(bdayPerson);

                _context.SaveChanges();

                response = _context.BdayPerson.Where(f => f.BdFirstName == bdayPersonRM.BdFirstName).SingleOrDefault(a => a.BdComingAge == bdayPersonRM.BdComingAge).BdayId;

                return true;
            }
            catch(Exception e)
            {
                response =  null;
                return false;
            }
            
        }
    }
}
