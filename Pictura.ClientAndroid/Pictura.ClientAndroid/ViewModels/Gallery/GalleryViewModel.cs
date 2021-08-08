using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;
using Pictura.ClientAndroid.PlatformInterfaces;
using Pictura.ClientAndroid.Services.Files;
using Pictura.ClientAndroid.Services.ServerConnection.Networks;
using Pictura.ClientAndroid.Views;
using Xamarin.Forms;
using INavigation = Pictura.ClientAndroid.Helpers.Navigation.INavigation;

namespace Pictura.ClientAndroid.ViewModels.Gallery
{
	public class GalleryViewModel : BaseViewModel
	{
		private readonly INavigation _navigation;
		private readonly IFileService _fileService;
		private readonly IPictureNetwork _pictureNetwork;
		private readonly IThumbnailService _thumbnailService;
		public ObservableCollection<PictureModel> Monkeys { get; set; }
		
		public ICommand PicturePicked { get; }
		public ICommand UploadMediasToServerCommand { get; }
		
		public GalleryViewModel(INavigation navigation, IFileService fileService, IPictureNetwork pictureNetwork,
			IThumbnailService thumbnailService)
		{
			Title = "Gallerie";
			
			_navigation = navigation;
			_fileService = fileService;
			_pictureNetwork = pictureNetwork;
			_thumbnailService = thumbnailService;
			Monkeys = new ObservableCollection<PictureModel>();

			PicturePicked = new Command<string>(OnPicturePicked);
			UploadMediasToServerCommand = new Command(UploadMediasToServerAsync);
			
			LoadMediasAsync();
		}

		private async void UploadMediasToServerAsync()
		{
			var files = await _fileService.GetAllFilePathAsync();
			var fileStreams = await _fileService.GetMultipleFileStreamsFromFilesAsync(files);
				
			await _pictureNetwork.PostStreamAsync(fileStreams);
		}

		private async void LoadMediasAsync()
		{
			try
			{
				var fileNames = await _fileService.GetAllFilePathAsync();

				foreach (var fileName in fileNames)
				{
					var thumbNailPath = await _thumbnailService.GenerateThumbnailAsync("/storage/emulated/0/thumbs/", fileName);
					Monkeys.Add(new PictureModel(thumbNailPath));
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