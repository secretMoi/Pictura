using System.Threading.Tasks;

namespace Pictura.ClientAndroid.PlatformInterfaces
{
	public interface IThumbnailService
	{
		Task<string> GenerateThumbnailAsync(string thumbnailsFolderPath, string filePath);
	}
}