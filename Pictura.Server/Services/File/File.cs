using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Pictura.Server.Models;
using Pictura.Shared.Extensions;

namespace Pictura.Server.Services.File
{
	public class File : IFileService
	{
		private readonly PictureOptions _pictureOptions;

		public File(IOptions<PictureOptions> options)
		{
			_pictureOptions = options.Value;
			
			if (!Directory.Exists(_pictureOptions.SavePath))
				Directory.CreateDirectory(_pictureOptions.SavePath);
		}
		
		public async Task SaveFileAsync(IFormFile file)
		{
			if (file.Length > 0)
			{
				var filePath = _pictureOptions.SavePath;

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