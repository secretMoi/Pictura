using Pictura.ClientAndroid.ViewModels;

namespace Pictura.ClientAndroid.Views
{
	public partial class ItemDetailPage
	{
		public ItemDetailPage()
		{
			InitializeComponent();
			BindingContext = new ItemDetailViewModel();
		}
	}
}