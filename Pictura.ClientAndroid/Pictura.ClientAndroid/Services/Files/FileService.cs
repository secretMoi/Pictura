using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Pictura.ClientAndroid.Services.Files
{
	public class FileService : IFileService
	{
		public async Task<string[]> GetFilesFromDirectoryAsync(string directoryPath)
		{
			return await Task.Run(() => Directory.GetFiles(directoryPath));
		}

		public async Task<FileStream> GetStreamFromFileAsync(string path)
		{
			return await Task.Run(() =>
			{
				return new FileStream(path, FileMode.Open, FileAccess.Read);

				//return new StreamReader(fileStream).BaseStream;
			});
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