using BirthDayCards.Models;
using BirthDayCards.ResponseModel;
using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BirthDayCards.Repositories
{
    public class CardRepo
    {
        private BirthDayCard_dbContext _context;
        private IConfiguration _config;

        public CardRepo(BirthDayCard_dbContext context)
        {
            _context = context;
        }
        public CardRepo(BirthDayCard_dbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
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

        public bool GetCard(int id, out TemplateRM response)
        {
            var target = _context.Template.SingleOrDefault(t => t.TemplateId == id);

            if (target != null)
            {
                response = new TemplateRM
                {
                    TemplateId = target.TemplateId,
                    TemplateUrl = target.TemplateUrl,
                    Gender = target.Gender
                };
                return true;
            }

            //template not found
            response = null;
            return false;
        }

        public async Task<Tuple<bool, object>> Purchase(PaymentRM paymentRM, string userName)
        {
            StripeConfiguration.ApiKey = _config["Stripe_SecretKey"];

            var optionsToken = new TokenCreateOptions
            {
                Card = new CreditCardOptions
                {
                    Number = paymentRM.CardNumber,
                    ExpMonth = paymentRM.Month,
                    ExpYear = paymentRM.Year,
                    Cvc = paymentRM.Cvc
                }
            };

            if (optionsToken == null)
            {
                return new Tuple<bool, object>(false, "Purchase cannot be completed");
            }

            var serviceToken = new TokenService();
            Token stripeToken = await serviceToken.CreateAsync(optionsToken);

            if (stripeToken == null)
            {
                return new Tuple<bool, object>(false, "Purchase cannot be completed");
            }

            var options = new ChargeCreateOptions
            {
                Amount = paymentRM.Amount * 100,
                Currency = "usd",
                Source = stripeToken.Id,
                Description = $"Success Purchase by {userName}"
            };

            var service = new ChargeService();
            Charge charge = await service.CreateAsync(options);

            if (!charge.Paid)
            {
                return new Tuple<bool, object>(false, "Purchase cannot be completed");
            }

            var purchase = new Payments
            {
                Amount = paymentRM.Amount,
                Description = options.Description,
                UserName = userName
            };

            _context.Payments.Add(purchase);

            await _context.SaveChangesAsync();

            var returnPurchase = new SuccessPaymentRM
            {
                Amount = purchase.Amount,
                Description = purchase.Description,
                UserName = purchase.UserName
            };

            return new Tuple<bool, object>(true, returnPurchase);
        }
    }
}
