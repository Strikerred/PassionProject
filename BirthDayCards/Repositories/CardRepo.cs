using BirthDayCards.Models;
using BirthDayCards.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BirthDayCards.Repositories
{
    public class CardRepo
    {
        private BirthDayCard_dbContext _context;

        public CardRepo(BirthDayCard_dbContext context)
        {
            _context = context;
        }

        public bool GetCards(out List<TemplateRM> response)
        {
            var target = _context.Template.ToList();

            if (target.Any())
            {
                response = new List<TemplateRM>();

                foreach (Template tp in target)
                {
                    response.Add(new TemplateRM
                    {
                        TemplateId = tp.TemplateId,
                        TemplateUrl = tp.TemplateUrl,
                        Gender = tp.Gender
                    });
                }

                return true;
            }

            //no templates found
            response = null;
            return false;
        }
    }
}
