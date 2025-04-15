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

        string cheminFichier = @"C:\\Users\\victorien\\Desktop\\WpfGestionBoutiqueFlorale\\WpfGestionBoutiqueFlorale\\bin\\Debug\\net8.0-windows\\fleurs_db.csv"; //  adapte le chemin selon ton projet
        FleurImportation.ImporterEtEnregistrerFleurs(cheminFichier);
    }
}

