using System;
using System.Collections.ObjectModel;
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
		
		public Command<string> PictureTapped { get; }
		
		public GalleryViewModel(INavigation navigation, IFileService fileService, IPictureNetwork pictureNetwork)
		{
			_navigation = navigation;
			_fileService = fileService;
			_pictureNetwork = pictureNetwork;
			Monkeys = new ObservableCollection<PictureModel>();

			PictureTapped = new Command<string>(OnPictureTapped);
			
			LoadMedias();
		}

		private async void LoadMedias()
		{
			try
			{
				var files = await _fileService.GetFilesFromDirectoryAsync("/storage/emulated/0/Download/");

				foreach (var file in files)
				{
					Monkeys.Add(new PictureModel(file));
				}
				
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

	public class PictureModel
	{
		public string Path { get; set; }

		public PictureModel(string path)
		{
			Path = path;
		}
	}
}