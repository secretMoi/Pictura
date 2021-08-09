using System;
using System.Threading.Tasks;
using Pictura.ClientAndroid.Services.Routes;
using Xamarin.Forms;

namespace Pictura.ClientAndroid.Services.Navigation
{
	public class ShellNavigation : INavigation
	{
		private readonly IRoute _route;
		private readonly AppShell _appShell;

		public ShellNavigation(IRoute route, AppShell appShell)
		{
			_route = route ?? throw new ArgumentNullException(nameof(route));
			_appShell = appShell;
		}
		
		/**
		 * <summary>Définit la page d'accueil</summary>
		 * <param name="page">Chemin de la page</param>
		 */
		public Task AsRootPage(Page page)
		{
			if (page == null) throw new ArgumentNullException(nameof(page));
			
			return Shell.Current.GoToAsync("//" + page);
		}

		/**
		 * <summary>Change de flow</summary>
		 * <param name="page">Nouveau flow</param>
		 */
		public void ChangeFlow(Page page)
		{
			Application.Current.MainPage = page ?? throw new ArgumentNullException(nameof(page));
		}
		
		/**
		 * <summary>Reviens dans le flow principal</summary>
		 */
		public void GoToMainFlow()
		{
			ChangeFlow(_appShell);
		}
		
		public void GoToLogInFlow()
		{
			//ChangeFlow(new LogInPage());
		}

		/**
		 * <summary>Navigue vers une page sans paramètre</summary>
		 */
		public Task PushAsync<T>() where T : Page
		{
			return Shell.Current.GoToAsync(_route.RouteName(typeof(T).ToString()));
		}
		
		/**
		 * <summary>Navigue vers une page avec paramètres</summary>
		 * <param name="parameterName">Nom du paramètre</param>
		 * <param name="data">Données à transmettre à la nouvelle page</param>
		 */
		public Task PushAsync<T>(string parameterName, object data) where T : Page
		{
			if (parameterName == null) throw new ArgumentNullException(nameof(parameterName));
			if (data == null) throw new ArgumentNullException(nameof(data));

			return Shell.Current.GoToAsync(
				_route.RouteName(typeof(T).ToString()) +
				"?" +
				parameterName +
				"=" +
				data
			);
		}

		/**
		 * <summary>Permet de retourner une page en arrière</summary>
		 */
		public Task GoBackAsync()
		{
			return Shell.Current.GoToAsync("..");
		}
	}
}