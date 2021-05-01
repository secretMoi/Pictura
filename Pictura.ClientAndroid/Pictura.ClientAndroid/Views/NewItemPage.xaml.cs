using Pictura.ClientAndroid.Models;
using Pictura.ClientAndroid.ViewModels;

namespace Pictura.ClientAndroid.Views
{
	public partial class NewItemPage
	{
		public Item Item { get; set; }

		public NewItemPage()
		{
			InitializeComponent();
			BindingContext = new NewItemViewModel();
		}
	}
}