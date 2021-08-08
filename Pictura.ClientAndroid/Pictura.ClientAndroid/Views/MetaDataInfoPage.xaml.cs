using Pictura.ClientAndroid.ViewModels.Gallery;
using Xamarin.Forms.Xaml;

namespace Pictura.ClientAndroid.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MetaDataInfoPage
	{
		public MetaDataInfoPage()
		{
			BindingContext = App.DiServices.GetService<MetaDataInfoViewModel>();
			InitializeComponent();
		}
	}
}