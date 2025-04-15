using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CsvHelper;
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

                // Ignore les propriétés non présentes dans le CSV (comme IdFleur, IdBouquet, IdCommande)
                csv.Context.TypeConverterOptionsCache.GetOptions<double>().NumberStyles = NumberStyles.Any;

                var fleurs = csv.GetRecords<Fleur>().ToList();

                // On force les clés étrangères à null pour éviter tout lien non voulu
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

    }
