using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfGestionBoutiqueFlorale.Models;

namespace WpfGestionBoutiqueFlorale.ViewModels
{
    class VendeurViewModel : ViewModelBase
    {
        public ObservableCollection<Commande> CommandeDisponibles { get; set; }
        public ObservableCollection<Utilisateur> VendeursDisponibles { get; set; }
        public Commande CommandeSelectionnee { get; set; }
        public Utilisateur VendeurSelectionnee { get; set; }

        public string MoyenPaiement { get; set; }

        public ICommand ValiderCommande { get; set; }
        public ICommand AnnulerCommande { get; set; }
        public ICommand GenererFacture { get; set; }


        

            public VendeurViewModel()
            {
                using var db = new GestionFloraleDbContext();

                VendeursDisponibles = new ObservableCollection<Utilisateur>(db.Utilisateurs.Where(u => u.role == "vendeur").ToList());
                CommandeDisponibles = new ObservableCollection<Commande>(db.Commandes.ToList());


                ValiderCommande = new RelayCommand(ExecuterValiderCommande);
                AnnulerCommande = new RelayCommand(ExecuterAnnulerCommande);
                GenererFacture = new RelayCommand(ExecuterGenererFacture);
                
            }

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
