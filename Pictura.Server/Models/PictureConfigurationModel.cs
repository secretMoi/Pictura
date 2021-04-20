namespace Pictura.Server.Models
{
	public class PictureConfigurationModel
	{
		public string Path { get; init; }
		public string[] FileFormats { get; init; }
		public string SavePath { get; init; }
	}
}