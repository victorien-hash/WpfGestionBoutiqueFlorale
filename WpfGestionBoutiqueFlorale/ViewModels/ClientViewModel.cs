using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using WpfGestionBoutiqueFlorale.Models;

namespace WpfGestionBoutiqueFlorale.ViewModels
{
    public class ClientViewModel
    {
        public ObservableCollection<Fleur> FleursDisponibles { get; set; }
        public ObservableCollection<Bouquet> BouquetsDisponibles { get; set; }
        public ObservableCollection<Utilisateur> VendeursDisponibles { get; set; }

        public Utilisateur VendeurSelectionne { get; set; }

        public string CartePersonalise { get; set; }
        public string NomBouquet { get; set; }



        public ICommand CommanderFleursCommand { get; set; }
        public ICommand CreerBouquet { get; set; }

        public ClientViewModel()
        {
            using var db = new GestionFloraleDbContext();

            FleursDisponibles = new ObservableCollection<Fleur>(db.Fleurs.ToList());
            BouquetsDisponibles = new ObservableCollection<Bouquet>(db.Bouquets.ToList());
            VendeursDisponibles = new ObservableCollection<Utilisateur>(db.Utilisateurs.ToList());

            CommanderFleursCommand = new RelayCommand(ExecuterCommande);
            CreerBouquet = new RelayCommand(ExecuterCreerBouquet);
        }
        private void ExecuterCommande()
        { 

        }

        private void ExecuterCreerBouquet()
        {
            try
            {
            var fleursCommandees = FleursDisponibles.Where(f => f.EstSelectionnee).ToList();

            if (fleursCommandees.Any())
            {
                MessageBox.Show("Veuillez sélectionner au moins une fleur ou un bouquet.");
                return;
            }


           

            using var db = new GestionFloraleDbContext();

                var bouquet = new Bouquet
                {
                    NomBouquet = NomBouquet,
                    CartePersonnalise = CartePersonalise,
                    IdCommande = null

                };
            db.Bouquets.Add(bouquet);
            db.SaveChanges();

           

            MessageBox.Show("Bouquet ajouté avec succès !");


            }
            catch (DbUpdateException dbEx)
            {
                MessageBox.Show($"Erreur lors de l enregistrement: {dbEx.InnerException?.Message ?? dbEx.Message}");
            }

        }
    }
}
