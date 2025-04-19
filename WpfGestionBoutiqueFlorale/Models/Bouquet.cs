using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfGestionBoutiqueFlorale.ViewModels;

namespace WpfGestionBoutiqueFlorale.Models
{
    public class Bouquet:ViewModelBase
    {
        public Bouquet() { 
            Fleurs = new List<Fleur>();
        }

        [Key]
        public int IdBouquet { get; set; }
        public string NomBouquet { get; set; }
        public string CartePersonnalise { get; set; }

        public ICollection<Fleur> Fleurs { get; set; }

        public int? IdCommande { get; set; }
        public Commande? Commande { get; set; }

        [NotMapped]
        public bool _estSelectionnee { get; set; }

        public bool EstSelectionnee
        {
            get => _estSelectionnee;
            set
            {
                if (_estSelectionnee != value)
                {
                    _estSelectionnee = value;
                    OnPropertyChanged(nameof(EstSelectionnee));
                }
            }
        }


    }

}
