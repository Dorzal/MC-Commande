using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Commande.Model.Enum.EStatus;

namespace Commande.Model
{
    public class Commande
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public long ArticleId { get; set; }
        public int Quantity { get; set; }
        public DateTime ShipDate { get; set; }
        public EnumStatus Status { get; set; }
        public bool Complete { get; set; }
    }

}
