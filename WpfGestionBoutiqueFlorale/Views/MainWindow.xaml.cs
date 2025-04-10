using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfGestionBoutiqueFlorale.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    private void AjouterClientMenu_Click(object sender, RoutedEventArgs e)
    {
        // Vérifie si l'onglet existe déjà
        foreach (TabItem tab in MainTabControl.Items)
        {
            if (tab.Header.ToString() == "Ajouter un client")
            {
                MainTabControl.SelectedItem = tab;
                return;
            }
        }

        // Crée le contrôle client
        ClientView clientView = new ClientView();

        // Crée un nouvel onglet
        TabItem tabItem = new TabItem
        {
            Header = "Ajouter un client",
            Content = clientView
        };

        MainTabControl.Items.Add(tabItem);
        MainTabControl.SelectedItem = tabItem;
    }
}