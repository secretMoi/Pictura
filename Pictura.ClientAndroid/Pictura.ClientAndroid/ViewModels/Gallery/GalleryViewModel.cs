using System;
using System.Collections.ObjectModel;
using System.Linq;
using Pictura.ClientAndroid.Services.Files;
using Pictura.ClientAndroid.Services.ServerConnection.Networks;
using Pictura.ClientAndroid.Views;
using Xamarin.Forms;
using INavigation = Pictura.ClientAndroid.Helpers.Navigation.INavigation;

namespace Pictura.ClientAndroid.ViewModels.Gallery
{
	public class GalleryViewModel
	{
		private readonly INavigation _navigation;
		private readonly IFileService _fileService;
		private readonly IPictureNetwork _pictureNetwork;
		public ObservableCollection<Monkey> Monkeys { get; set; }
		
		public Command<string> PictureTapped { get; }
		
		public GalleryViewModel(INavigation navigation, IFileService fileService, IPictureNetwork pictureNetwork)
		{
			_navigation = navigation;
			_fileService = fileService;
			_pictureNetwork = pictureNetwork;
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
			
			ReadFiles();
		}

		private async void ReadFiles()
		{
			try
			{
				var files = await _fileService.GetFilesFromDirectoryAsync("/storage/emulated/0/Download/");
				var fileStreams = await _fileService.GetStreamsFromFilesAsync(files);

				await _pictureNetwork.PostStreamAsync(fileStreams);

			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
			
		}

		private async void OnPictureTapped(string imagePath)
		{
			if (string.IsNullOrWhiteSpace(imagePath)) return;

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