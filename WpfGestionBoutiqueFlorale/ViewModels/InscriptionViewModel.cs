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

	public class InscriptionViewModel : ViewModelBase
	{
		private string _nom;
		private string _prenom;
		private string _adresse;
		private string _telephone;
		private string _motDePasse;
		private string _confirmationMotDePasse;

		public string Nom { get => _nom; set => SetProperty(ref _nom, value); }
		public string Prenom { get => _prenom; set => SetProperty(ref _prenom, value); }
		public string Adresse { get => _adresse; set => SetProperty(ref _adresse, value); }
		public string Telephone { get => _telephone; set => SetProperty(ref _telephone, value); }
		public string MotDePasse { get => _motDePasse; set => SetProperty(ref _motDePasse, value); }
		public string ConfirmationMotDePasse { get => _confirmationMotDePasse; set => SetProperty(ref _confirmationMotDePasse, value); }

		// Commandes
		public ICommand RegisterCommand { get; }
		public ICommand NavigateToLoginCommand { get; }

		public InscriptionViewModel()
		{
			RegisterCommand = new RelayCommand(ExecuteRegister);
			NavigateToLoginCommand = new RelayCommand(ExecuteNavigateToLogin);
		}

		private void ExecuteRegister(object parameter)
		{
			// Validation des champs obligatoires
			if (string.IsNullOrWhiteSpace(Nom) ||
				string.IsNullOrWhiteSpace(Prenom) ||
				string.IsNullOrWhiteSpace(Adresse))
			{
				ShowErrorMessage("Tous les champs obligatoires (*) doivent être remplis");
				return;
			}

			// Validation des mots de passe
			if (string.IsNullOrWhiteSpace(MotDePasse))
			{
				ShowErrorMessage("Le mot de passe ne peut pas être vide");
				return;
			}

			if (MotDePasse != ConfirmationMotDePasse)
			{
				ShowErrorMessage("Les mots de passe ne correspondent pas");
				return;
			}

			// Simulation d'inscription réussie
			var result = MessageBox.Show(
				"Inscription réussie !\n\nVotre compte a été créé avec succès.",
				"Succès",
				MessageBoxButton.OK,
				MessageBoxImage.Information);

			if (result == MessageBoxResult.OK)
			{
				// Redirection vers la page d'accueil
				var mainWindow = new MainWindow();
				mainWindow.Show();

				// Fermeture de la fenêtre actuelle
				Application.Current.Windows.OfType<InscriptionView>().FirstOrDefault()?.Close();
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

		private void ExecuteNavigateToLogin(object parameter)
		{
			var loginWindow = new ConnexionView();
			loginWindow.Show();
			Application.Current.Windows.OfType<InscriptionView>().FirstOrDefault()?.Close();
		}
	}
}
