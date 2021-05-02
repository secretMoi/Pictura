using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Pictura.ClientAndroid.Services.Files
{
	public interface IFileService
	{
		/**
		 * <summary>Liste des dossiers de sauvegarde des médias</summary>
		 */
		ICollection<string> MediaDirectoriesStorage { get; }

		/**
		 * <summary>Ajoute un dossier de sauvegarde pour les médias</summary>
		 * <param name="mediaDirectoryStorage">Chemin du dossier à ajouter</param>
		 */
		void AddMediaDirectoryStorage(string mediaDirectoryStorage);

		/**
		 * <summary>Récupère la liste des fichiers situés dans une répertoire</summary>
		 * <param name="directoryPath">Chemin du répertoire</param>
		 * <returns>Une liste des chemins des fichiers trouvés</returns>
		 */
		Task<IEnumerable<string>> GetMediasFromDirectoryAsync(string directoryPath);

		/**
		 * <summary>Récupère un <see cref="FileStream"/> venant d'un fichier</summary>
		 * <param name="path">Chemin du fichier</param>
		 * <returns>Retourne un <see cref="FileStream"/> correspondant au fichier</returns>
		 */
		Task<FileStream> GetFileStreamFromFileAsync(string path);

		/**
		 * <summary>Récupère les <see cref="FileStream"/> venant d'une liste de fichiers</summary>
		 * <param name="paths">Liste des chemins de fichier</param>
		 * <returns>Retourne une liste des <see cref="FileStream"/> correspondant aux fichiers</returns>
		 */
		Task<IEnumerable<FileStream>> GetMultipleFileStreamsFromFilesAsync(IEnumerable<string> paths);

		Task<IEnumerable<string>> GetAllFilePathAsync();
	}
}