using Commande.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace Commande.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandeController : ControllerBase
    {
        private readonly CommandeContext _context;

        public CommandeController(CommandeContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<Model.Commande> Get()
        {
            return _context.Commande.Include(x => x.Articles).ToList();
        }

        [HttpGet("{id}", Name = "id")]
        public Model.Commande GetCommandeById(int id)
        {
            return _context.Commande.Where(x => x.Id == id).FirstOrDefault();
        }

        [HttpPost]
        public Model.Commande CreateCommande([FromBody]Model.Commande commande)
        {
            _context.Commande.Add(commande);
            _context.SaveChanges();
            SendMail(commande);
            return commande;
        }

        [HttpPut]
        public void EditCommande([FromBody]Model.Commande commande)
        {
            _context.Entry(commande).State = EntityState.Modified;
            _context.SaveChanges();
        }

        private void SendMail(Model.Commande commande)
        {
            MailAddress fromAddress = new MailAddress("verretech1@gmail.com", "Verre Tech");
            MailAddress toAddress = new MailAddress(commande.Email.Trim(), commande.LastName);
            const string fromPassword = "V3rretech!";
            const string subject = "VERRE-TECH Validation Commande";
            string body = "Bonjour " + commande.LastName.ToUpper().Trim() + " " + commande.FirstName.Trim() + ",<br> Votre commande a bien été validée."; ;

            SmtpClient smtp = new SmtpClient
            {
                Host = "mc-smtp",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (MailMessage message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                try
                {

                    {
                        smtp.Send(message);
                    }
                }
                catch
                {
                    throw;
                }
            }
            //try
            //{
            //    MailMessage message = new MailMessage();
            //    SmtpClient smtp = new SmtpClient();
            //    message.From = new MailAddress("verretech1@gmail.com");
            //    message.To.Add(new MailAddress(commande.Email));
            //    message.CC.Add(new MailAddress("verretech1@gmail.com"));
            //    message.Subject = "VERRE-TECH Validation Commande";
            //    message.IsBodyHtml = true;
            //    message.Body = @"Bonjour " + commande.LastName.ToUpper().Trim() + " " + commande.FirstName.Trim() + ",<br> Votre commande a bien été validée.";
            //    smtp.Port = 587;
            //    smtp.Host = "smtp.gmail.com";
            //    smtp.EnableSsl = true;
            //    smtp.UseDefaultCredentials = false;
            //    smtp.Credentials = new NetworkCredential("verretech1@gmail.com", "V3rretech!");
            //    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            //    smtp.Send(message);
            //}
            //catch (Exception e)
            //{
            //    throw e;
            //}
        }

        [HttpDelete]
        public void DeleteCommande(int id)
        {
            Model.Commande commandeToDelete = _context.Commande
                .Where(x => x.Id == id)
                .FirstOrDefault();

            _context.Commande.Remove(commandeToDelete);
            _context.SaveChanges();
        }
    }
}