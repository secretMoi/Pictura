using Pictura.ClientAndroid.Views;
using System;
using Xamarin.Forms;

namespace Pictura.ClientAndroid
{
	public partial class AppShell : Xamarin.Forms.Shell
	{
		public AppShell()
		{
			InitializeComponent();
			Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
			Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
			Routing.RegisterRoute(nameof(GalleryPage), typeof(GalleryPage));
			Routing.RegisterRoute(nameof(PictureFullScreenPage), typeof(PictureFullScreenPage));
		}

		private async void OnMenuItemClicked(object sender, EventArgs e)
		{
			await Shell.Current.GoToAsync("//LoginPage");
		}
	}
}
