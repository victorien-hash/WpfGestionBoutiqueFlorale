using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfGestionBoutiqueFlorale.Views
{
    public partial class ClientView : UserControl
    {
        public ClientView()
        {
            InitializeComponent();
        }

        private void AjouterClient_Click(object sender, RoutedEventArgs e)
        {
            string nom = NomClientTextBox.Text;
            string prenom =PrenomClientTextBox.Text;
            Int64 telephone = 0;
            string adresse =AdresseClientTextBox.Text;


            if (string.IsNullOrWhiteSpace(nom) || string.IsNullOrWhiteSpace(prenom) || string.IsNullOrWhiteSpace(prenom))
            {
                MessageBox.Show("Veuillez remplir tous les champs.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                MessageBox.Show($"Client ajouté : {nom} {prenom}", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }

}
