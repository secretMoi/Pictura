using Pictura.ClientAndroid.Views;
using System;
using Pictura.ClientAndroid.Helpers.Routes;
using Pictura.ClientAndroid.ViewModels.Gallery;
using Xamarin.Forms;

namespace Pictura.ClientAndroid
{
	public partial class AppShell
	{
		public AppShell(IRoute route)
		{
			InitializeComponent();

			Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
			Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
			route.RegisterRoute<GalleryPage>();
			route.RegisterRoute<PictureFullScreenPage>();
			route.RegisterRoute<MetaDataInfoPage>();
		}

		private async void OnMenuItemClicked(object sender, EventArgs e)
		{
			await Current.GoToAsync("//LoginPage");
		}
	}
}
