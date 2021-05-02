using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using ExifLib;
using Pictura.ClientAndroid.Services.Files;
using Xamarin.Forms;

namespace Pictura.ClientAndroid.ViewModels.Gallery
{
	[QueryProperty(nameof(ImagePath), nameof(ImagePath))]
	public class MetaDataInfoViewModel : BaseViewModel
	{
		private readonly IFileService _fileService;

		public MetaDataInfoViewModel(IFileService fileService)
		{
			_fileService = fileService;
			MetaDataItems = new Collection<MetaDataItem>();
		}

		public ICollection<MetaDataItem> MetaDataItems { get; set; }
		
		private string _imagePath;
		public string ImagePath
		{
			get => _imagePath;
			set
			{
				SetProperty(ref _imagePath, value);
				GetDate();
			}
		}

		public async void GetDate()
		{
			await using var fileStream = await _fileService.GetFileStreamFromFileAsync(ImagePath);
			var picInfo = ExifReader.ReadJpeg(fileStream);
			var name = picInfo.FileName;
			var lat = picInfo.GpsLatitude;
			var longi = picInfo.GpsLongitude;
			var comment = picInfo.UserComment;
		}
		
		public class MetaDataItem
		{
			public string Name { get; set; }
			public string Description { get; set; }
		}
	}
}