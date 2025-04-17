using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using Microsoft.EntityFrameworkCore;
using WpfGestionBoutiqueFlorale;

namespace WpfGestionBoutiqueFlorale.Models
{
    public class FleurImportation
    {
        public static void ImporterEtEnregistrerFleurs(string cheminFichierCsv)
        {
            try
            {
                using var reader = new StreamReader(cheminFichierCsv);
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

                // Ignore les colonnes non présentes dans le CSV
                csv.Context.Configuration.HeaderValidated = null;
                csv.Context.Configuration.MissingFieldFound = null;


                csv.Context.RegisterClassMap<FleurMap>();

                var fleurs = csv.GetRecords<Fleur>().ToList();

                // On force les clés étrangères à null
                foreach (var fleur in fleurs)
                {
                    fleur.IdBouquet = null;
                    fleur.IdCommande = null;
                    fleur.Bouquet = null;
                    fleur.Commande = null;
                }

                using var db = new GestionFloraleDbContext();
                db.Fleurs.AddRange(fleurs);
                db.SaveChanges();

                MessageBox.Show($"{fleurs.Count} fleurs importées avec succès.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'importation des fleurs : {ex.Message}");
            }
        }


    }
    public sealed class FleurMap : ClassMap<Fleur>
    {
        public FleurMap()
        {
            Map(f => f.Nom).Name("Nom");
            Map(f => f.PrixUnitaire).Name("Prix Unitaire (CAD)");
            Map(f => f.CouleurDominante).Name("Couleur");
            Map(f => f.Description).Name("Caractéristiques");
        }
    }

}
