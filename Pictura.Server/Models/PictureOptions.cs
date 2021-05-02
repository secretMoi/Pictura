using System;
using System.IO;

namespace Pictura.Server.Models
{
	public class PictureOptions
	{
		public static string MediaPath => Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
		public string SaveFolderName { get; init; }
		public string SaveFolderPath => Path.Combine(MediaPath, SaveFolderName);
	}
}