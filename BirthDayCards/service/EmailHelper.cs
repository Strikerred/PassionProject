﻿using BirthDayCards.Models;
using BirthDayCards.ResponseModel;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;

namespace BirthDayCards.service
{
    public class EmailHelper
    {
        private BirthDayCard_dbContext _context;
        private IConfiguration _config;

        public EmailHelper(IConfiguration config, BirthDayCard_dbContext context)
        {
            _context = context;
            _config = config;
        }


        public async Task<Tuple<bool, object>> SendEmails(int templateId, IEnumerable<string> emails, int bdayId)
        {
            var emailsDb = "";
            var recipients = new List<string>();

            foreach (string email in emails)
            {
                recipients.Add(email);
            }

            try
            {
                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(_config["EmailSettings_FromEmail"], _config["EmailSettings_DisplayName"])
                };

                foreach (string recipient in recipients)
                {
                    mail.To.Add(new MailAddress(recipient));
                }

                var personEvent = _context.BdEvent.SingleOrDefault(e => e.Bday.BdayId == bdayId);

                var bdayEvent = _context.BdayPerson.SingleOrDefault(e => e.BdayId == bdayId);

                mail.Subject = personEvent.Ename;

                var targetTemplate = _context.Template.SingleOrDefault(t => t.TemplateId == templateId); 


                string text = $"My name is {bdayEvent.BdFirstName} {bdayEvent.BdLastName} and I want you to come to my birthday party on {personEvent.Edate} at " +
                              $"{personEvent.Eaddress} {personEvent.Ecity} {personEvent.Eprovince} {personEvent.EpostalCode}. " + 
                              $"this is phone number if you have any question {personEvent.EphoneNumber} {targetTemplate.TemplateUrl}";

                string html = $@"<p>My name is {personEvent.Bday.BdFirstName} {personEvent.Bday.BdLastName} and I want you to come to my birthday party on {personEvent.Edate} at " +
                              $"{personEvent.Eaddress} {personEvent.Ecity} {personEvent.Eprovince} {personEvent.EpostalCode}. " +
                              $"this is phone number if you have any question {personEvent.EphoneNumber} {targetTemplate.TemplateUrl} </p>";

                mail.AlternateViews.Add(
                    AlternateView.CreateAlternateViewFromString(text,
                    null, MediaTypeNames.Text.Plain));

                mail.AlternateViews.Add(
                   AlternateView.CreateAlternateViewFromString(html,
                   null, MediaTypeNames.Text.Html));

                mail.Priority = MailPriority.High;

                SmtpClient smtp = new SmtpClient(_config["EmailSettings_Domain"], 587);
                smtp.Credentials = new NetworkCredential(_config["EmailSettings_UsernameLogin"], _config["EmailSettings_UsernamePassword"]);
                smtp.EnableSsl = false;

                smtp.Send(mail);

                foreach (string recipient in recipients)
                {
                    emailsDb += recipient + ", ";
                }            

                Recipients recipientDb = new Recipients()
                {
                    Recipients1 = emailsDb,
                    BdayId = bdayId
                };

                _context.Recipients.Add(recipientDb);

                await _context.SaveChangesAsync();

                return new Tuple<bool, object>(true, recipients);
                
            }
            catch (Exception e)
            {
                return new Tuple<bool, object>(false, e);
            }
        }
    }
}
