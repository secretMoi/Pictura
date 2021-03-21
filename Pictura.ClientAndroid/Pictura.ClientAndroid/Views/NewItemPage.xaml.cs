using Pictura.ClientAndroid.Models;
using Pictura.ClientAndroid.ViewModels;
using Xamarin.Forms;

namespace Pictura.ClientAndroid.Views
{
	public partial class NewItemPage : ContentPage
	{
		public Item Item { get; set; }

		public NewItemPage()
		{
			InitializeComponent();
			BindingContext = new NewItemViewModel();
		}
	}
}