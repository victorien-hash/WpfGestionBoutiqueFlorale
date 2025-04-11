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
using WpfGestionBoutiqueFlorale.ViewModels;

namespace WpfGestionBoutiqueFlorale.Views
{
	/// <summary>
	/// Logique d'interaction pour InscriptionView.xaml
	/// </summary>
	public partial class InscriptionView : Window
	{
		
		public InscriptionView()
		{
			InitializeComponent();
			DataContext = new InscriptionViewModel();
		}

		private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
		{
			if (DataContext is InscriptionViewModel vm)
				vm.MotDePasse = MotPasswordBox.Password;
		}

		private void ConfirmPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
		{
			if (DataContext is InscriptionViewModel vm)
				vm.ConfirmationMotDePasse = ConfirmationPasswordBox.Password;
		}
	}
}
