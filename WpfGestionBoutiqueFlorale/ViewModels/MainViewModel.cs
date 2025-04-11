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

		public MainViewModel()
		{
			ShowRegistrationCommand = new RelayCommand(ShowRegistration);
		}

		private void ShowRegistration(object parameter = null)
		{
			// Votre logique pour afficher la vue d'inscription
			var registrationView = new InscriptionView();
			registrationView.Show();

			// Fermer la fenêtre actuelle si nécessaire
			Application.Current.Windows.OfType<MainWindow>().FirstOrDefault()?.Close();
		}
	}
}
