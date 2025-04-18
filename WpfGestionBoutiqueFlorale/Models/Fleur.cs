using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration.Attributes;

namespace WpfGestionBoutiqueFlorale.Models
{
    public class Fleur
    {
        [Key]
        public int IdFleur { get; set; }

        [Name("Nom")]
        public string Nom { get; set; }

        [Name("Prix Unitaire (CAD)")]
        public double PrixUnitaire { get; set; }

        [Name("Couleur")]
        public string CouleurDominante { get; set; }

        [Name("Caractéristiques")]
        public string Description { get; set; }

        public int? IdBouquet { get; set; }
        public Bouquet? Bouquet { get; set; }

        public int? IdCommande { get; set; }
        public Commande? Commande { get; set; }

        [NotMapped]
        public bool EstSelectionnee { get; set; }
      
    }
}
