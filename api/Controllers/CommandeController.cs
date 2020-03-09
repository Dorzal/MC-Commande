using Commande.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

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
            return commande;
        }

        [HttpPut]
        public void EditCommande([FromBody]Model.Commande commande)
        {
            _context.Entry(commande).State = EntityState.Modified;
            _context.SaveChanges();
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