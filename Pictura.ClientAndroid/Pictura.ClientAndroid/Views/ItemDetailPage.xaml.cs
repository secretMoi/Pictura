using Pictura.ClientAndroid.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace Pictura.ClientAndroid.Views
{
	public partial class ItemDetailPage : ContentPage
	{
		public ItemDetailPage()
		{
			InitializeComponent();
			BindingContext = new ItemDetailViewModel();
		}
	}
}