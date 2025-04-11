using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfGestionBoutiqueFlorale.Views;

namespace WpfGestionBoutiqueFlorale.ViewModels
{
	// MainViewModel.cs
	public class MainViewModel
	{
		public ICommand ShowRegistrationCommand { get; }
		public ICommand ShowLoginCommand { get; }

		public MainViewModel()
		{
			ShowRegistrationCommand = new RelayCommand(ShowRegistration);
            ShowLoginCommand = new RelayCommand(ShowLogin);
		}

		private void ShowRegistration(object parameter = null)
		{
			// Votre logique pour afficher la vue d'inscription
			var registrationView = new InscriptionView();
			registrationView.Show();

			// Fermer la fenêtre actuelle si nécessaire
			Application.Current.Windows.OfType<MainWindow>().FirstOrDefault()?.Close();
		}

        private void ShowLogin(object parameter = null)
        {
            // Votre logique pour afficher la vue de connexion
            var loginView = new ConnexionView();
            loginView.Show();

            // Fermer la fenêtre actuelle sinécessaire
            Application.Current.Windows.OfType<MainWindow>().FirstOrDefault()?.Close();
        }
	}
}
