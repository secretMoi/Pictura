using System.Threading.Tasks;

namespace Pictura.ClientAndroid.Droid
{
	public interface IThumbnailService
	{
		Task<string> GenerateThumbnailAsync(string rootPath, string filePath);
	}
}