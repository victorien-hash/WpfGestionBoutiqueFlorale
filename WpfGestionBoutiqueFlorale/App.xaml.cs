using System.Configuration;
using System.Data;
using System.Windows;
using WpfGestionBoutiqueFlorale.Models;

namespace WpfGestionBoutiqueFlorale;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        string cheminFichier = "fleurs_db.csv"; // Chemin vers le fichier CSV
        FleurImportation.ImporterEtEnregistrerFleurs(cheminFichier);
        UserImportationApi.BtnImport();
    }
}

