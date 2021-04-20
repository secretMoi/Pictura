using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Pictura.Server.Models;
using Pictura.Shared.Extensions;

namespace Pictura.Server.Services.File
{
	public class File : IFileService
	{
		private readonly PictureConfigurationModel _pictureConfiguration;

		public File(IConfiguration configuration)
		{
			_pictureConfiguration = configuration.GetModelFromSection<PictureConfigurationModel>("Picture");
			
			if (!Directory.Exists(_pictureConfiguration.SavePath))
				Directory.CreateDirectory(_pictureConfiguration.SavePath);
		}
		
		public async Task SaveFileAsync(IFormFile file)
		{
			if (file.Length > 0)
			{
				var filePath = _pictureConfiguration.SavePath;

				await using var stream = System.IO.File.Create(filePath + file.FileName);
				await file.CopyToAsync(stream);
			}
		}
		
		public async Task SaveFilesAsync(IEnumerable<IFormFile> files)
		{
			foreach (var file in files)
			{
				await SaveFileAsync(file);
			}
		}
	}
}