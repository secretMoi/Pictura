using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
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
		public ObservableCollection<PictureModel> Monkeys { get; set; }
		
		public Command<string> PicturePicked { get; }
		public Command<string> UploadMediasToServerCommand { get; }
		
		public GalleryViewModel(INavigation navigation, IFileService fileService, IPictureNetwork pictureNetwork)
		{
			_navigation = navigation;
			_fileService = fileService;
			_pictureNetwork = pictureNetwork;
			Monkeys = new ObservableCollection<PictureModel>();

			PicturePicked = new Command<string>(OnPicturePicked);
			UploadMediasToServerCommand = new Command<string>(async _ => await UploadMediasToServerAsync());
			
			LoadMediasAsync();
		}

		private async Task UploadMediasToServerAsync()
		{
			var files = await _fileService.GetAllFilePathAsync();
			var fileStreams = await _fileService.GetMultipleFileStreamsFromFilesAsync(files);
				
			await _pictureNetwork.PostStreamAsync(fileStreams);
		}

		private async void LoadMediasAsync()
		{
			try
			{
				var files = await _fileService.GetAllFilePathAsync();

				foreach (var file in files)
				{
					Monkeys.Add(new PictureModel(file));
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		private async void OnPicturePicked(string imagePath)
		{
			if (string.IsNullOrWhiteSpace(imagePath)) return;

			await _navigation.PushAsync<PictureFullScreenPage>(nameof(PictureFullScreenViewModel.ImagePath), imagePath);
		}
	}

	public class PictureModel
	{
		public string Path { get; set; }

		public PictureModel(string path)
		{
			Path = path;
		}
	}
}