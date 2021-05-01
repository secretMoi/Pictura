using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Pictura.Shared.Models;

namespace Pictura.ClientAndroid.Services.Files
{
	public class FileService : IFileService
	{
		public async Task<IEnumerable<string>> GetFilesFromDirectoryAsync(string directoryPath)
		{
			return await Task.Run(() => Directory.GetFiles(directoryPath)
				.Where(item =>
					PictureModel.SupportedFilesFormat.Contains(Path.GetExtension(item).Substring(1)))
			);
		}

		public async Task<FileStream> GetStreamFromFileAsync(string path)
		{
			return await Task.Run(() => new FileStream(path, FileMode.Open, FileAccess.Read));
		}
		
		public async Task<IEnumerable<FileStream>> GetStreamsFromFilesAsync(IEnumerable<string> paths)
		{
			ICollection<FileStream> streams = new List<FileStream>();
			
			foreach (var path in paths)
			{
				streams.Add(await GetStreamFromFileAsync(path));
			}

			return streams;
		}
	}
}