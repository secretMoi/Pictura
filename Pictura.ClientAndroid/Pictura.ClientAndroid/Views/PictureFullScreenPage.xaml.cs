using Pictura.ClientAndroid.ViewModels.Gallery;
using Xamarin.Forms.Xaml;

namespace Pictura.ClientAndroid.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PictureFullScreenPage
	{
		public PictureFullScreenPage()
		{
			BindingContext = App.DiServices.GetService<PictureFullScreenViewModel>();
			InitializeComponent();
		}
	}
}