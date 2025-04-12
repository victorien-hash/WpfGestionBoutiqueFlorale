using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using WpfGestionBoutiqueFlorale.Views;

namespace WpfGestionBoutiqueFlorale.ViewModels
{
	public class ConnexionViewModel: ViewModelBase
	{
        private string username;
        private string password;
        public string Username { get => username; set => SetProperty(ref username, value); }
        public string Password { get => password; set => SetProperty(ref password, value); }

        // Commandes
        public ICommand OpenPage { get; }
        

        public ConnexionViewModel()
        {
            OpenPage = new RelayCommand(ExecuteConnect);
        }

        private void ExecuteConnect(object parameter)
        {
           
            //if (string.IsNullOrWhiteSpace(username) ||
            //    string.IsNullOrWhiteSpace(password))
                
            //{
            //    ShowErrorMessage("Tous les champs obligatoires doivent être remplis");
            //    return;
            //}
            //validation des champs


            if (string.IsNullOrWhiteSpace(username))
            {
                ShowErrorMessage("Le nom d'utilisateur ne peut pas etre vide");
                return;
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                ShowErrorMessage("Le mot de passe ne peut pas etre vide");
                return;
            }

            // redirection a la page client
            if (username == "client" && password == "client")
            {
                var clientWindow = new ClientView();
                clientWindow.Show();
                Application.Current.Windows.OfType<ConnexionView>().FirstOrDefault()?.Close();
            }
            // redirection a la page vendeur
            else if (username == "vendeur" && password == "vendeur")
            {
                var vendeurWindow = new VendeurView();
                vendeurWindow.Show();
                Application.Current.Windows.OfType<ConnexionView>().FirstOrDefault()?.Close();
            }
            // redirection a la page fournisseur
            else if (username == "fournisseur" && password == "fournisseur")
            {
                var fournisseurWindow = new FournisseurView();
                fournisseurWindow.Show();
                Application.Current.Windows.OfType<ConnexionView>().FirstOrDefault()?.Close();
            }
            // redirection a la page proprietaire
            else if (username == "proprietaire" && password == "proprietaire")
            {
                var proprietaireWindow = new ProprietaireView();
                proprietaireWindow.Show();
                Application.Current.Windows.OfType<ConnexionView>().FirstOrDefault()?.Close();
            }
            else
            {
                ShowErrorMessage("Nom d'utilisateur ou mot de passe incorrect");

       
            }
        }

        // Méthode helper pour afficher les erreurs
        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(
                message,
                "Erreur d'inscription",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }

        


    }
}

