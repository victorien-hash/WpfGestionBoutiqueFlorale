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

        public ObservableCollection<Facture> FacturesDisponibles { get; set; }

        public Utilisateur VendeurSelectionne { get; set; }

        public string CartePersonalise { get; set; }
        public string NomBouquet { get; set; }



        public ICommand CommanderFleursCommand { get; set; }
        public ICommand CreerBouquet { get; set; }



        // proprietes venant du vendeur qui nous servirons pour reutiliser les 
        // attibut de vendeur pour le proprietaire via clientviewmodel

        public ObservableCollection<Commande> CommandeDisponibles { get; set; }
        public Commande CommandeSelectionnee { get; set; }
        public Utilisateur VendeurSelectionnee { get; set; }

        public string MoyenPaiement { get; set; }

        public ICommand ValiderCommande { get; set; }
        public ICommand AnnulerCommande { get; set; }
        public ICommand GenererFacture { get; set; }



        public ClientViewModel()
        {
            using var db = new GestionFloraleDbContext();

            FleursDisponibles = new ObservableCollection<Fleur>(db.Fleurs.ToList());
            FacturesDisponibles = new ObservableCollection<Facture>(db.Factures.ToList());
            BouquetsDisponibles = new ObservableCollection<Bouquet>(db.Bouquets.ToList());
            VendeursDisponibles = new ObservableCollection<Utilisateur>(db.Utilisateurs.Where(u => u.role == "vendeur").ToList());


            CommanderFleursCommand = new RelayCommand(ExecuterCommande);
            CreerBouquet = new RelayCommand(ExecuterCreerBouquet);


            // venant du vendeur
            VendeursDisponibles = new ObservableCollection<Utilisateur>(db.Utilisateurs.Where(u => u.role == "vendeur").ToList());
            CommandeDisponibles = new ObservableCollection<Commande>(db.Commandes.ToList());


            ValiderCommande = new RelayCommand(ExecuterValiderCommande);
            AnnulerCommande = new RelayCommand(ExecuterAnnulerCommande);
            GenererFacture = new RelayCommand(ExecuterGenererFacture);
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

        // methodes venant du vendeur

        public void ExecuterValiderCommande()
        {
            if (CommandeSelectionnee != null)
            {
                using (var db = new GestionFloraleDbContext())
                {
                    var commande = db.Commandes.FirstOrDefault(c => c.IdCommande == CommandeSelectionnee.IdCommande);

                    if (commande != null)
                    {
                        commande.EstValidee = true;
                        db.SaveChanges();

                        // Mettre à jour la liste locale pour refléter le changement (facultatif selon ton affichage)
                        CommandeSelectionnee.EstValidee = true;
                    }
                }
                MessageBox.Show("Votre commande a bien été validée.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            else
            {
                // Tu peux aussi afficher un message si tu as une gestion d'erreurs ou de notifications
                MessageBox.Show("Veuillez sélectionner une commande à valider.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public void ExecuterAnnulerCommande()
        {
            if (CommandeSelectionnee != null)
            {
                using (var db = new GestionFloraleDbContext())
                {
                    var commande = db.Commandes.FirstOrDefault(c => c.IdCommande == CommandeSelectionnee.IdCommande);

                    if (commande != null)
                    {
                        commande.EstValidee = false;
                        db.SaveChanges();

                        // Mise à jour locale
                        CommandeSelectionnee.EstValidee = false;
                    }

                }

                MessageBox.Show("Votre commande a bien été annulée.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une commande à annuler.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public void ExecuterGenererFacture()
        {
            if (CommandeSelectionnee == null || VendeurSelectionnee == null || string.IsNullOrWhiteSpace(MoyenPaiement))
            {
                MessageBox.Show("Veuillez sélectionner une commande, un vendeur et un moyen de paiement.",
                                "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            using (var db = new GestionFloraleDbContext())
            {
                var facture = new Facture
                {
                    IdCommande = CommandeSelectionnee.IdCommande,
                    IdUtilisateur = VendeurSelectionnee.IdUtilisateur,
                    ModePaiement = MoyenPaiement,

                };

                db.Factures.Add(facture);
                db.SaveChanges();
            }

            MessageBox.Show("Facture générée avec succès !", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
        }





    }
}
