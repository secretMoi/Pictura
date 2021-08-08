using System.IO;
using System.Threading.Tasks;
using Kaliko.ImageLibrary;
using Kaliko.ImageLibrary.Scaling;

namespace Pictura.ClientAndroid.Droid
{
	public class ThumbnailService : IThumbnailService
	{
		public async Task<string> GenerateThumbnailAsync(string rootPath, string filePath)
		{
			return await Task.Run(() =>
			{
				var image = new KalikoImage(filePath);

				var thumb = image.Scale(new CropScaling(128, 128));
				var thumbnailPath = Path.Combine(rootPath, "thumbnail-" + filePath);
				thumb.SaveJpg(thumbnailPath, 20);
				
				return thumbnailPath;
			});
		}
	}
}