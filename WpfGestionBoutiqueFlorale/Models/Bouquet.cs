using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfGestionBoutiqueFlorale.Models
{
    class Bouquet
    {
        public Bouquet() { 
            Fleurs = new List<Fleur>();
        }

        [Key]
        public int IdBouquet { get; set; }
        public string NomBouquet { get; set; }
        public string CartePersonnalise { get; set; }

        public ICollection<Fleur> Fleurs { get; set; }

        public int IdCommande { get; set; }
        public Commande Commande { get; set; }

    }

}
