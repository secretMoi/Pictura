using Pictura.ClientAndroid.ViewModels;
using Xamarin.Forms.Xaml;

namespace Pictura.ClientAndroid.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage
	{
		public LoginPage()
		{
			InitializeComponent();
			BindingContext = new LoginViewModel();
		}
	}
}