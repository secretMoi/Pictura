using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pictura.ClientAndroid.Services.ServerConnection.Networks
{
	public interface IServerNetwork<T>
	{
		/**
		 * <summary>Requête sélectionnant un élément par son ID</summary>
		 * <param name="id">Id de l'enregistrement à récupérer</param>
		 * <returns>Renvoie le model cherché</returns>
		 */
		Task<T> GetByIdAsync(int id);

		/**
		 * <summary>Requête sélectionnant tous les éléments</summary>
		 * <returns>Renvoie une liste des models trouvés</returns>
		 */
		Task<IList<T>> GetAllAsync();

		/**
		 * <summary>Requête modifiant un enregistremen</summary>
		 * <param name="data">Model de l'enregistrement à envoyer</param>
		 */
		Task UpdateAsync(T data);

		/**
		 * <summary>Requête envoyant un nouvel enregistrement</summary>
		 * <param name="input">Model à envoyer et créer</param>
		 * <returns>Renvoie le model créé</returns>
		 */
		Task PostAsync(T input);

		/**
		 * <summary>Requête supprimant un élément par son ID</summary>
		 * <param name="id">Id de l'enregistrement à supprimer</param>
		 * <returns>Renvoie le model supprimé</returns>
		 */
		Task DeleteAsync(int id);
	}
}