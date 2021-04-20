using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Pictura.Server.Models;

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

		public async Task<string[]> GetAllFilesAsync()
		{
			return await GetFilesFromAsync(_pictureConfiguration.Path, _pictureConfiguration.FileFormats, true);
		}
		
		private async Task<string[]> GetFilesFromAsync(string searchFolder, string[] extensions, bool isRecursive)
		{
			var filesFound = new List<string>();
			var searchOption = isRecursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;

			return await Task.Run(() =>
			{
				foreach (var filter in extensions)
				{
					filesFound.AddRange(Directory.GetFiles(searchFolder, $"*.{filter}", searchOption));
				}
				
				return filesFound.ToArray();
			});
		}
	}
}