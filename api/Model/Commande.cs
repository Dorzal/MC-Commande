using System;
using System.Collections.Generic;
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
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public List<Article> Articles { get; set; }
        public DateTime ShipDate { get; set; }
        public EnumStatus Status { get; set; }
        public float Total { get; set; }
        public int Store { get; set; }
    }
}
