﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MetadataExtractor;
using Xamarin.Forms;

namespace Pictura.ClientAndroid.ViewModels.Gallery
{
	[QueryProperty(nameof(ImagePath), nameof(ImagePath))]
	public class MetaDataInfoViewModel : BaseViewModel
	{
		public MetaDataInfoViewModel()
		{
			MetaDataItems = new Collection<MetaDataItem>();
			Title = "Métadonnées";
		}

		public ICollection<MetaDataItem> MetaDataItems { get; set; }
		
		private string _imagePath;
		public string ImagePath
		{
			get => _imagePath;
			set
			{
				SetProperty(ref _imagePath, value);
				ExtractMetaData();
			}
		}

		private void ExtractMetaData()
		{
			var directories = ImageMetadataReader.ReadMetadata(ImagePath);
			
			ReadDirectory(directories, "Exif SubIFD");
			ReadDirectory(directories, "File");
			ReadDirectory(directories, "File Type");
		}

		private void ReadDirectory(IReadOnlyCollection<Directory> directories, string directoryName)
		{
			var directory = directories.FirstOrDefault(item => item.Name.Equals(directoryName));
			if (directory == null) return;
			foreach (var tag in directory.Tags)
				MetaDataItems.Add(new MetaDataItem(tag.Name, tag.Description));
		}
	}
	
	public class MetaDataItem
	{
		public MetaDataItem(string name, string description)
		{
			Name = name;
			Description = description;
		}

		public string Name { get; set; }
		public string Description { get; set; }
	}
}