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
    class FournisseurViewModel: ViewModelBase
    {
        public ObservableCollection<Fleur> FleursDisponibles { get; set; }

        public ICommand ReapprovisionnerFleur { get; set; }
        public FournisseurViewModel() 
        {
            using var db = new GestionFloraleDbContext();

            FleursDisponibles = new ObservableCollection<Fleur>(db.Fleurs.ToList());

            ReapprovisionnerFleur = new RelayCommand(ExecuterReapprovisionnerFleur);
        }

        private void ExecuterReapprovisionnerFleur()
        {
            try
            {
                using var db = new GestionFloraleDbContext();

                var fleurSelectionnee = FleursDisponibles.Where(f => f.EstSelectionnee).ToList();

                if (fleurSelectionnee == null)
                {
                    MessageBox.Show("Veuillez sélectionner une fleur à reapprovisionner.");
                    return;
                }
                else
                {
                    //stocker les fleurs dans la base de données
                    foreach (var fleur in fleurSelectionnee)
                    {
                        var fleurAjouter = new Fleur()
                        {
                            Nom = fleur.Nom,
                            PrixUnitaire = fleur.PrixUnitaire,
                            CouleurDominante = fleur.CouleurDominante,
                            Description = fleur.Description
                        };
                        db.Fleurs.Add(fleurAjouter);
                        db.SaveChanges();
                    }
                    MessageBox.Show($"{fleurSelectionnee.Count} reapprovisionnées avec succès !");
                }

            }
            catch (DbUpdateException dbEx)
            {
                MessageBox.Show($"Erreur lors de l enregistrement: {dbEx.InnerException?.Message ?? dbEx.Message}");
            }

        }
    }

}
