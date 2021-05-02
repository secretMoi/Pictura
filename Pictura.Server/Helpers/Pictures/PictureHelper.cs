using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Pictura.Server.Models;
using Pictura.Shared.Models;

namespace Pictura.Server.Helpers.Pictures
{
	public class PictureHelper : IPictureHelper
	{
		private readonly PictureOptions _pictureConfiguration;

		public PictureHelper(IConfiguration configuration)
		{
			_pictureConfiguration = configuration
				.GetSection("Picture")
				.Get<PictureOptions>();
		}

		/**
		 * <summary>Récupère tous les médias du répertoire de sauvegarde par défaut</summary>
		 * <returns>Renvoie le chemin de tous les fichiers trouvés</returns>
		 */
		public async Task<string[]> GetAllFilesAsync()
		{
			return await GetFilesFromAsync(PictureOptions.MediaPath, PictureModel.SupportedFilesFormat, true);
		}
		
		/**
		 * <summary>Récupère tous les médias du répertoire indiqué</summary>
		 * <param name="searchFolder">Dossier dans lequel chercher les médias</param>
		 * <param name="extensions">Extensions des fichiers à chercher</param>
		 * <param name="isRecursive">true cherche dans les sous-dossiers, false ne cherche que dans le dossier parent</param>
		 * <returns>Renvoie le chemin de tous les fichiers trouvés</returns>
		 */
		public async Task<string[]> GetFilesFromAsync(string searchFolder, string[] extensions, bool isRecursive)
		{
			var filesFound = new List<string>();
			var searchOption = isRecursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;

			return await Task.Run(() =>
			{
				foreach (var filter in extensions)
				{
					filesFound.AddRange(Directory.GetFiles(searchFolder, $"*.{filter}", searchOption));
				}
				
				return filesFound.ToArray();
			});
		}
	}
}