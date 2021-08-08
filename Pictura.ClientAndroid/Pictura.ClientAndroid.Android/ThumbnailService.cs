using System;
using System.IO;
using System.Threading.Tasks;
using Android.Graphics;
using Pictura.ClientAndroid.PlatformInterfaces;
using Path = System.IO.Path;

namespace Pictura.ClientAndroid.Droid
{
	public class ThumbnailService : IThumbnailService
	{
		public async Task<string> GenerateThumbnailAsync(string thumbnailsFolderPath, string filePath)
		{
			var thumbnailPath = Path.Combine(thumbnailsFolderPath, "thumbnail-" + Path.GetFileName(filePath));
			if (File.Exists(thumbnailPath)) return thumbnailPath;
			
			try
			{
				// pour les vidéos
				// var retriever = new MediaMetadataRetriever();
				// retriever.SetDataSource(thumbnailPath, new Dictionary<string, string>());
				//var bitmap = retriever.GetFrameAtTime(usecond);

				// if (bitmap != null)
				// {
				// 	// bitmap.Height = 120;
				// 	// bitmap.Width = 120;
				// 	var memoryStream = new MemoryStream();
				// 	await bitmap.CompressAsync(Bitmap.CompressFormat.Png, 100, memoryStream);
				// 	var bitmapData = memoryStream.ToArray();
				
				var imageAsByteArray = await ResizeImage(filePath, 500, 500);
				
				if (!Directory.Exists(Path.GetDirectoryName(thumbnailPath)))
					Directory.CreateDirectory(Path.GetDirectoryName(thumbnailPath) ?? string.Empty);
					
				await File.WriteAllBytesAsync(thumbnailPath, imageAsByteArray);

				return thumbnailPath;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}

		private async Task<byte[]> ResizeImage(string filePath, float width, float height)
		{
			var originalImage = await BitmapFactory.DecodeFileAsync(filePath);

			if (originalImage is null) throw new NullReferenceException($"L'image située à {filePath} n'existe pas");

			float newHeight;
			float newWidth;

			var originalHeight = originalImage.Height;
			var originalWidth = originalImage.Width;

			if (originalHeight > originalWidth)
			{
				newHeight = height;
				var ratio = originalHeight / height;
				newWidth = originalWidth / ratio;
			}
			else
			{
				newWidth = width;
				var ratio = originalWidth / width;
				newHeight = originalHeight / ratio;
			}

			var resizedImage = Bitmap.CreateScaledBitmap(originalImage, (int)newWidth, (int)newHeight, true);
			originalImage.Recycle();
			
			if(resizedImage is null) throw new NullReferenceException($"Impossible de transformer l'image {filePath} en thumbnail");

			await using var memoryStream = new MemoryStream();
			await resizedImage.CompressAsync(Bitmap.CompressFormat.Png, 100, memoryStream);
			resizedImage.Recycle();

			return memoryStream.ToArray();
		}
	}
}