using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Pictura.Shared.Models;

namespace Pictura.ClientAndroid.Services.Files
{
	public class FileService : IFileService
	{
		/**
		 * <summary>Liste des dossiers de sauvegarde des médias</summary>
		 */
		public ICollection<string> MediaDirectoriesStorage { get; }

		public FileService()
		{
			MediaDirectoriesStorage = new Collection<string>
			{
				"/storage/emulated/0/Download/"
			};
		}

		/**
		 * <summary>Ajoute un dossier de sauvegarde pour les médias</summary>
		 * <param name="mediaDirectoryStorage">Chemin du dossier à ajouter</param>
		 */
		public void AddMediaDirectoryStorage(string mediaDirectoryStorage)
		{
			MediaDirectoriesStorage.Add(mediaDirectoryStorage);
		}
		
		/**
		 * <summary>Récupère la liste des fichiers situés dans une répertoire</summary>
		 * <param name="directoryPath">Chemin du répertoire</param>
		 * <returns>Une liste des chemins des fichiers trouvés</returns>
		 */
		public async Task<IEnumerable<string>> GetMediasFromDirectoryAsync(string directoryPath)
		{
			return await Task.Run(() => Directory.GetFiles(directoryPath)
				.Where(item =>
					PictureModel.SupportedFilesFormat.Contains(Path.GetExtension(item).Substring(1)))
			);
		}

		/**
		 * <summary>Récupère un <see cref="FileStream"/> venant d'un fichier</summary>
		 * <param name="path">Chemin du fichier</param>
		 * <returns>Retourne un <see cref="FileStream"/> correspondant au fichier</returns>
		 */
		public async Task<FileStream> GetFileStreamFromFileAsync(string path)
		{
			return await Task.Run(() => new FileStream(path, FileMode.Open, FileAccess.Read));
		}
		
		/**
		 * <summary>Récupère les <see cref="FileStream"/> venant d'une liste de fichiers</summary>
		 * <param name="paths">Liste des chemins de fichier</param>
		 * <returns>Retourne une liste des <see cref="FileStream"/> correspondant aux fichiers</returns>
		 */
		public async Task<IEnumerable<FileStream>> GetFileStreamsFromFilesAsync(IEnumerable<string> paths)
		{
			ICollection<FileStream> streams = new List<FileStream>();
			
			foreach (var path in paths)
			{
				streams.Add(await GetFileStreamFromFileAsync(path));
			}

			return streams;
		}
	}
}