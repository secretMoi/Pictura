using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;
using Pictura.Server.Helpers.Json;

namespace Pictura.Server.Helpers.Pictures
{
	public class PictureHelper : IPictureHelper
	{
		private readonly PictureConfigurationModel _pictureConfiguration;

		public PictureHelper(IConfiguration configuration)
		{
			_pictureConfiguration = configuration
				.GetSection("Picture")
				.Get<PictureConfigurationModel>();
		}

		public string[] GetAllFiles()
		{
			var filters = new [] { "jpg", "jpeg", "png", "gif", "tiff", "bmp", "svg" };
			return GetFilesFrom(_pictureConfiguration.Path, filters, true);
			//return Directory.GetFiles(_pictureConfiguration.Path, extensions, SearchOption.AllDirectories);
		}
		
		private string[] GetFilesFrom(string searchFolder, string[] extensions, bool isRecursive)
		{
			var filesFound = new List<string>();
			var searchOption = isRecursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
			
			foreach (var filter in extensions)
			{
				filesFound.AddRange(Directory.GetFiles(searchFolder, $"*.{filter}", searchOption));
			}
			
			return filesFound.ToArray();
		}
	}
}