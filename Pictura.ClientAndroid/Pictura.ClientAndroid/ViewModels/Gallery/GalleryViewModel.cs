using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Extensions.Options;
using Pictura.ClientAndroid.Models;
using Pictura.ClientAndroid.PlatformInterfaces;
using Pictura.ClientAndroid.Services.Files;
using Pictura.ClientAndroid.Services.ServerConnection.Networks;
using Pictura.ClientAndroid.Views;
using Xamarin.Forms;
using INavigation = Pictura.ClientAndroid.Services.Navigation.INavigation;

namespace Pictura.ClientAndroid.ViewModels.Gallery
{
	public class GalleryViewModel : BaseViewModel
	{
		private readonly INavigation _navigation;
		private readonly IFileService _fileService;
		private readonly IPictureNetwork _pictureNetwork;
		private readonly IThumbnailService _thumbnailService;
		private readonly PathOptions _pathOptions;
		public ObservableCollection<PictureModel> Monkeys { get; set; }
		
		public ICommand PicturePicked { get; }
		public ICommand UploadMediasToServerCommand { get; }
		
		public GalleryViewModel(INavigation navigation, IFileService fileService, IPictureNetwork pictureNetwork,
			IThumbnailService thumbnailService, IOptions<PathOptions> pathOptions)
		{
			Title = "Gallerie";
			
			_navigation = navigation;
			_fileService = fileService;
			_pictureNetwork = pictureNetwork;
			_thumbnailService = thumbnailService;
			_pathOptions = pathOptions.Value;
			Monkeys = new ObservableCollection<PictureModel>();

			PicturePicked = new Command<PictureModel>(OnPicturePicked);
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
				var filePaths = await _fileService.GetAllFilePathAsync();

				foreach (var filePath in filePaths)
				{
					var thumbnailPath = 
						await _thumbnailService.GenerateThumbnailAsync(_pathOptions.ThumbnailsPath, filePath);
					Monkeys.Add(new PictureModel(filePath, thumbnailPath));
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		private async void OnPicturePicked(PictureModel picture)
		{
			if (string.IsNullOrWhiteSpace(picture.ImagePath)) return;

			await _navigation.PushAsync<PictureFullScreenPage>(nameof(PictureFullScreenViewModel.ImagePath), picture.ImagePath);
		}
	}

	public class PictureModel
	{
		public string ImagePath { get; }
		public string ThumbnailPath { get; set; }

		public PictureModel(string imagePath, string thumbnailPath)
		{
			ImagePath = imagePath;
			ThumbnailPath = thumbnailPath;
		}
	}
}