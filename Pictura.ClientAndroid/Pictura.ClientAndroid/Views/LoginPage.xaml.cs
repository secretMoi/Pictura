using Pictura.ClientAndroid.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pictura.ClientAndroid.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
		public LoginPage()
		{
			InitializeComponent();
			this.BindingContext = new LoginViewModel();
		}
	}
}