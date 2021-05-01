using System.Threading.Tasks;

namespace Pictura.Server.Helpers.Pictures
{
	public interface IPictureHelper
	{
		/**
		 * <summary>Récupère tous les médias du répertoire de sauvegarde par défaut</summary>
		 * <returns>Renvoie le chemin de tous les fichiers trouvés</returns>
		 */
		Task<string[]> GetAllFilesAsync();

		/**
		 * <summary>Récupère tous les médias du répertoire indiqué</summary>
		 * <param name="searchFolder">Dossier dans lequel chercher les médias</param>
		 * <param name="extensions">Extensions des fichiers à chercher</param>
		 * <param name="isRecursive">true cherche dans les sous-dossiers, false ne cherche que dans le dossier parent</param>
		 * <returns>Renvoie le chemin de tous les fichiers trouvés</returns>
		 */
		Task<string[]> GetFilesFromAsync(string searchFolder, string[] extensions, bool isRecursive);
	}
}