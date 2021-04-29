using System.Collections.ObjectModel;
using Pictura.ClientAndroid.Views;
using Xamarin.Forms;
using INavigation = Pictura.ClientAndroid.Helpers.Navigation.INavigation;

namespace Pictura.ClientAndroid.ViewModels.Gallery
{
	public class GalleryViewModel
	{
		private readonly INavigation _navigation;
		public ObservableCollection<Monkey> Monkeys { get; set; }
		
		public Command<string> PictureTapped { get; }
		
		public GalleryViewModel(INavigation navigation)
		{
			_navigation = navigation;
			Monkeys = new ObservableCollection<Monkey>
			{
				new() {Name = "coucou", Location = "dans ton q", ImageUrl = "https://i.insider.com/5f8865662a400c0019debda6"},
				new() {Name = "coucou", Location = "dans ton q", ImageUrl = "https://i.guim.co.uk/img/media/02088fb2247b13df646907d47f552dc69a236bc7/0_748_3235_1940/master/3235.jpg?width=1200&height=1200&quality=85&auto=format&fit=crop&s=172ccbaa7535c9e16d0455138d20a07c"},
				new() {Name = "coucou", Location = "dans ton q", ImageUrl = "https://i.guim.co.uk/img/media/02088fb2247b13df646907d47f552dc69a236bc7/0_748_3235_1940/master/3235.jpg?width=1200&height=1200&quality=85&auto=format&fit=crop&s=172ccbaa7535c9e16d0455138d20a07c"},
				new() {Name = "coucou", Location = "dans ton q", ImageUrl = "https://i.guim.co.uk/img/media/02088fb2247b13df646907d47f552dc69a236bc7/0_748_3235_1940/master/3235.jpg?width=1200&height=1200&quality=85&auto=format&fit=crop&s=172ccbaa7535c9e16d0455138d20a07c"},
				new() {Name = "coucou", Location = "dans ton q", ImageUrl = "https://i.guim.co.uk/img/media/02088fb2247b13df646907d47f552dc69a236bc7/0_748_3235_1940/master/3235.jpg?width=1200&height=1200&quality=85&auto=format&fit=crop&s=172ccbaa7535c9e16d0455138d20a07c"},
				new() {Name = "coucou", Location = "dans ton q", ImageUrl = "https://i.guim.co.uk/img/media/02088fb2247b13df646907d47f552dc69a236bc7/0_748_3235_1940/master/3235.jpg?width=1200&height=1200&quality=85&auto=format&fit=crop&s=172ccbaa7535c9e16d0455138d20a07c"},
			};

			PictureTapped = new Command<string>(OnPictureTapped);
		}

		private async void OnPictureTapped(string imagePath)
		{
			if (string.IsNullOrWhiteSpace(imagePath))
				return;

			await _navigation.PushAsync<PictureFullScreenPage>(nameof(PictureFullScreenViewModel.ImagePath), imagePath);
		}
	}

	public class Monkey
	{
		public string Name { get; set; }
		public string Location { get; set; }
		public string Details { get; set; }
		public string ImageUrl { get; set; }
	}
}