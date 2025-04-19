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
    public class ClientViewModel: ViewModelBase
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
            VendeursDisponibles = new ObservableCollection<Utilisateur>(db.Utilisateurs.Where(u => u.role == "vendeur").ToList());


            CommanderFleursCommand = new RelayCommand(ExecuterCommande);
            CreerBouquet = new RelayCommand(ExecuterCreerBouquet);
        }
        private void ExecuterCommande()
        {
            try
            {
                var fleursCommandees = FleursDisponibles.Where(f => f.EstSelectionnee).ToList();
                var BouquetsCommandees = BouquetsDisponibles.Where(f => f.EstSelectionnee).ToList();

                if (VendeurSelectionne == null || (!fleursCommandees.Any() && !BouquetsCommandees.Any()))
                {
                    MessageBox.Show("Veuillez sélectionner un vendeur et au moins une fleur ou un bouquet.");
                    return;
                }




                // MessageBox.Show($"Fleurs sélectionnées : {fleursCommandees.Count}, Bouquets sélectionnés : {BouquetsCommandees.Count}  {VendeurSelectionne.IdUtilisateur}");


                else
                {
                    using var db = new GestionFloraleDbContext();
                    double montantTotalFleurs = 0;
                    double montantTotalBouquets = 0;

                    foreach (var fleur in fleursCommandees)
                    {
                        // calcul de la somme des prix des fleurs commandées
                        montantTotalFleurs += fleur.PrixUnitaire;
                    }

                    foreach (var bouquet in BouquetsCommandees)
                    {
                        foreach (var fleur in bouquet.Fleurs)
                        {
                            // calcul de la somme des prix des fleurs commandées
                            montantTotalBouquets += fleur.PrixUnitaire;
                        }

                        if (!string.IsNullOrEmpty(bouquet.CartePersonnalise))
                        {

                            montantTotalBouquets += 3;
                        }
                        else
                        {
                            montantTotalBouquets += 2;
                        }
                    }


                    var commande = new Commande
                    {

                        IdUtilisateur = VendeurSelectionne.IdUtilisateur,
                        MontantTotal = montantTotalFleurs + montantTotalBouquets,
                        EstValidee = false,
                        IdFacture = null,



                    };
                    db.Commandes.Add(commande);
                    db.SaveChanges();



                    MessageBox.Show("Commande passé avec succès !");


                }

            }
            catch (DbUpdateException dbEx)
            {
                MessageBox.Show($"Erreur lors de l enregistrement: {dbEx.InnerException?.Message ?? dbEx.Message}");
            }

        }



        private void ExecuterCreerBouquet()
        {
            try
            {
            var fleursBouquet = FleursDisponibles.Where(f => f.EstSelectionnee).ToList();

            if (fleursBouquet.Any())
            {
                MessageBox.Show("Veuillez sélectionner au moins une fleur.");
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
