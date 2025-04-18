using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using WpfGestionBoutiqueFlorale.Views;
using WpfGestionBoutiqueFlorale.Models;

namespace WpfGestionBoutiqueFlorale.ViewModels
{

	public class InscriptionViewModel : ViewModelBase
	{

		private string _username;
		private string _firstname;
		private string _lastname;
		private string _phone;
		private string _role;
		private string _password;
		private string _confirmationpassword;
		private string _email;

		public string Username { get => _username; set => SetProperty(ref _username, value); }
		public string Firstname { get => _firstname; set => SetProperty(ref _firstname, value); }
		public string Lastname { get => _lastname; set => SetProperty(ref _lastname, value); }
		public string Phone { get => _phone; set => SetProperty(ref _phone, value); }
		public string Role { get => _role; set => SetProperty(ref _role, value); }
		public string Password { get => _password; set => SetProperty(ref _password, value); }
        public string ConfirmationPassword { get => _confirmationpassword; set => SetProperty(ref _confirmationpassword, value); }
        public string Email { get => _email; set => SetProperty(ref _email, value); }

		// Commandes
		public ICommand RegisterCommand { get; }
		public ICommand NavigateToLoginCommand { get; }

		public InscriptionViewModel()
		{
			RegisterCommand = new RelayCommand(ExecuteRegister);
			NavigateToLoginCommand = new RelayCommand(ExecuteNavigateToLogin);
		}

		public void ExecuteRegister(object parameter)
		{
			// Validation des champs obligatoires
			if (string.IsNullOrWhiteSpace(Username) ||
                string.IsNullOrWhiteSpace(Firstname) ||
                string.IsNullOrWhiteSpace(Lastname) ||
                string.IsNullOrWhiteSpace(Phone) ||
                string.IsNullOrWhiteSpace(Role) ||
                string.IsNullOrWhiteSpace(Email))
				
			{
				ShowErrorMessage("Tous les champs obligatoires (*) doivent être remplis");
				return;
			}

			// Validation des mots de passe
			if (string.IsNullOrWhiteSpace(Password))
			{
				ShowErrorMessage("Le mot de passe ne peut pas être vide");
				return;
			}

			if (Password != ConfirmationPassword)
			{
				ShowErrorMessage("Les mots de passe ne correspondent pas");
				return;
			}

			// Création de l'utilisateur
            using var user = new GestionFloraleDbContext();

            // Ajout de l'utilisateur dans la base de données
			var utilisateur = new Utilisateur
            {
                username = Username,
                firstname = Firstname,
                lastname = Lastname,
                phone = Phone,
                role = Role,
                password = Password,
                email = Email
            };
            user.Utilisateurs.Add(utilisateur);
            user.SaveChanges();





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
