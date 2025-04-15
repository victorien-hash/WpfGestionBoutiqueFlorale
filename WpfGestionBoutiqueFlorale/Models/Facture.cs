using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfGestionBoutiqueFlorale.Models
{
    public class Facture
    {
        [Key]
        public int IdFacture { get; set; }
        public string ModePaiement { get; set; }


    }
}
