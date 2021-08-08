using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Kaliko.ImageLibrary;
using Kaliko.ImageLibrary.Scaling;
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
		public ObservableCollection<PictureModel> Monkeys { get; set; }
		
		public ICommand PicturePicked { get; }
		public ICommand UploadMediasToServerCommand { get; }
		
		public GalleryViewModel(INavigation navigation, IFileService fileService, IPictureNetwork pictureNetwork)
		{
			Title = "Gallerie";
			
			_navigation = navigation;
			_fileService = fileService;
			_pictureNetwork = pictureNetwork;
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
					var thumbNailPath = await GenerateThumbnailAsync(string.Empty, fileName);
					Monkeys.Add(new PictureModel(thumbNailPath));
					//Monkeys.Add(new PictureModel(file));
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		private async Task<string> GenerateThumbnailAsync(string rootPath, string filePath)
		{
			return await Task.Run(() =>
			{
				var image = new KalikoImage(filePath);

				var thumb = image.Scale(new CropScaling(128, 128));
				var thumbnailPath = Path.Combine(rootPath, "thumbnail-" + filePath);
				thumb.SaveJpg(thumbnailPath, 20);
				
				return thumbnailPath;
			});
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