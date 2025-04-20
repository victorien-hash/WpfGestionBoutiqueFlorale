using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfGestionBoutiqueFlorale.Models
{
    public class Commande
    {
        public Commande()
        {
            Fleurs = new List<Fleur>();
            Bouquets = new List<Bouquet>();
        }
        [Key]
        public int IdCommande { get; set; }

        public int? IdUtilisateur { get; set; }
        public Utilisateur? Utilisateur { get; set; }
        
        public double MontantTotal { get; set; }
        public bool EstValidee { get; set; }

        public ICollection<Fleur> Fleurs { get; set; }
        public ICollection<Bouquet> Bouquets { get; set; }
       
    }
}
