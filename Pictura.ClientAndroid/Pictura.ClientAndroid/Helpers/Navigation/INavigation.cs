using System.Threading.Tasks;
using Xamarin.Forms;

namespace Pictura.ClientAndroid.Helpers.Navigation
{
	public interface INavigation
	{
		/**
		 * <summary>Définit la page d'accueil</summary>
		 * <param name="page">Chemin de la page</param>
		 */
		Task AsRootPage(Page page);

		/**
		 * <summary>Change de flow</summary>
		 * <param name="page">Nouveau flow</param>
		 */
		void ChangeFlow(Page page);

		/**
		 * <summary>Reviens dans le flow principal</summary>
		 */
		void GoToMainFlow();

		void GoToLogInFlow();

		/**
		 * <summary>Navigue vers une page sans paramètre</summary>
		 */
		Task PushAsync<T>() where T : Page;

		/**
		 * <summary>Navigue vers une page avec paramètres</summary>
		 * <param name="parameterName">Nom du paramètre</param>
		 * <param name="data">Données à transmettre à la nouvelle page</param>
		 */
		Task PushAsync<T>(string parameterName, object data) where T : Page;

		/**
		 * <summary>Permet de retourner une page en arrière</summary>
		 */
		Task GoBackAsync();
	}
}