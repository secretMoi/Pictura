using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Pictura.ClientAndroid.Services.Files
{
	public interface IFileService
	{
		Task<string[]> GetFilesFromDirectoryAsync(string directoryPath);
		Task<FileStream> GetStreamFromFileAsync(string path);
		Task<IEnumerable<FileStream>> GetStreamsFromFilesAsync(IEnumerable<string> paths);
	}
}