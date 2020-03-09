using Microsoft.EntityFrameworkCore;

namespace Commande.Model
{
    public class CommandeContext : DbContext
    {
        public CommandeContext(DbContextOptions<CommandeContext> options) : base(options)
        {
        }

        public DbSet<Commande> Commande { get; set; }
    }
}
