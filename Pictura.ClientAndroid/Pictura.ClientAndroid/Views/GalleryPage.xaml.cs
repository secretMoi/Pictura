using Pictura.ClientAndroid.ViewModels.Gallery;
using Xamarin.Forms.Xaml;

namespace Pictura.ClientAndroid.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GalleryPage
	{
		public GalleryPage()
		{
			BindingContext = App.DiServices.GetService<GalleryViewModel>();
			InitializeComponent();
		}
	}
}